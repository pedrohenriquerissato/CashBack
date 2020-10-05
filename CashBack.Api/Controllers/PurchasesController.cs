using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Api.Resources;
using CashBack.Domain;
using CashBack.Domain.Models;
using CashBack.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IRetailerService _retailerService;
        private readonly IMapper _mapper;

        public PurchasesController(IPurchaseService purchaseService, IMapper mapper, IRetailerService retailerService)
        {
            this._mapper = mapper;
            this._purchaseService = purchaseService;
            this._retailerService = retailerService;
        }

        /// <summary>
        /// Retorna todas as compras cadastradas para um(a) determinado(a) revendedor(a)
        /// </summary>
        /// <param name="cpf">CPF do(a) revendedor(a)</param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("retailer")]
        public async Task<ActionResult<IEnumerable<CashbachPurchaseResource>>> GetPurchaseByRetailerDocumentId(string cpf)
        {
            try
            {
                var purchases = await _purchaseService.GetPurchaseByRetailerDocumentId(cpf);
                var purchaseResource = _mapper.Map<IEnumerable<CashbackPurchaseDTO>, IEnumerable<CashbachPurchaseResource>>(purchases);

                return Ok(purchaseResource);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        /// <summary>
        /// Registra uma compra realizada por um(a) revendedor(a)
        /// </summary>
        /// <param name="purchase">Objeto da compra</param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Purchase>> CreatePurchase([FromBody] SavePurchaseResource purchase)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var retailer = await _retailerService.GetRetailerByDocumentId(purchase.RetailerDocumentId);

                if (retailer == null)
                    return NotFound("O(a) revendedor(a) não foi encontrado(a) pelo CPF informado. Favor checar");

                purchase.Retailer = retailer;

                if (!DateTime.TryParseExact(purchase.Date, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var result))
                {
                    DateTime.TryParse(purchase.Date, out result);
                }

                purchase.Date = result.ToShortDateString();

                var purchaseToCreate = _mapper.Map<SavePurchaseResource, Purchase>(purchase);

                var newPurchase = await _purchaseService.CreatePurchase(purchaseToCreate);

                var createdPurchase = await _purchaseService.GetPurchaseById(newPurchase.Id);

                var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(createdPurchase);

                return Ok(new { retorno = "Compra cadastrada com sucesso!", purchaseResource});
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retorna o total de cashback de um(a) revendedor(a)
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("cashback")]
        public async Task<ActionResult<object>> GetPurchasesCashBack(string cpf)
        {
            try
            {
                //TODO validates retailer's documentId

                var result = await _purchaseService.GetCashBackTotal(cpf);

                return result;
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
