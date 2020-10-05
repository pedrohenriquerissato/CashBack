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
    public class RetailersController : ControllerBase
    {
        private readonly IRetailerService _retailerService;
        private readonly IMapper _mapper;

        public RetailersController(IRetailerService retailerService, IMapper mapper)
        {
            this._mapper = mapper;
            this._retailerService = retailerService;
        }

        /// <summary>
        /// Retorna um(a) revendedor(a) pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("{cpf}", Name = "GetRetailerByDocumentId")]
        public async Task<ActionResult<RetailerResource>> GetRetailerByDocumentId(string cpf)
        {
            try
            {
                var retailer = await _retailerService.GetRetailerByDocumentId(cpf);
                var retailerService = _mapper.Map<Retailer, RetailerResource>(retailer);

                return Ok(retailerService);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Cadastra um(a) novo(a) revendedor(a)
        /// </summary>
        /// <param name="retailer">Objeto do(a) revendedor(a)</param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<RetailerResource>> CreateRetailer([FromBody] SaveRetailerResource retailer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var retailerToCreate = _mapper.Map<SaveRetailerResource, Retailer>(retailer);

                var newRetailer = await _retailerService.CreateRetailer(retailerToCreate);

                var retailerService = _mapper.Map<Retailer, RetailerResource>(newRetailer);

                return CreatedAtAction(nameof(GetRetailerByDocumentId), new { cpf = newRetailer.DocumentId }, retailerService);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
