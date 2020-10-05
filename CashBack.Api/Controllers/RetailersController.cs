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

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailerResource>> GetRetailerByDocumentId(string id)
        {
            try
            {
                var retailer = await _retailerService.GetRetailerByDocumentId(id);
                var retailerService = _mapper.Map<Retailer, RetailerResource>(retailer);

                return Ok(retailerService);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

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

                var createdRetailer = await _retailerService.GetRetailerByDocumentId(newRetailer.DocumentId);

                var retailerResource = _mapper.Map<Retailer, RetailerResource>(createdRetailer);

                return Ok(retailerResource);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
