using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Api.Resources;
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
        private readonly IMapper _mapper;

        public PurchasesController(IPurchaseService purchaseService, IMapper mapper)
        {
            this._mapper = mapper;
            this._purchaseService = purchaseService;
        }

        [Authorize("Bearer")]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAllPurchases()
        {
            try
            {
                var purchases = await _purchaseService.GetAllWithRetailer();
                var purchaseResources = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

                return Ok(purchaseResources);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("retailer/{id}")]
        public async Task<ActionResult<IEnumerable<PurchaseResource>>> GetPurchaseByRetailerDocumentId(string id)
        {
            try
            {
                var purchases = await _purchaseService.GetPurchaseByRetailerDocumentId(id);
                var purchaseResource = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

                return Ok(purchaseResource);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
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

                var purchaseToCreate = _mapper.Map<SavePurchaseResource, Purchase>(purchase);

                var newPurchase = await _purchaseService.CreatePurchase(purchaseToCreate);

                var createdPurchase = await _purchaseService.GetPurchaseById(newPurchase.Id);

                var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(createdPurchase);

                return Ok(purchaseResource);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
