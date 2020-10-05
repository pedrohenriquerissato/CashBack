using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Api.Resources;
using CashBack.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public LoginsController(ILoginService loginService, IMapper mapper)
        {
            this._mapper = mapper;
            this._loginService = loginService;
        }

        /// <summary>
        /// Realiza o login de um(a) revendedor(a) na API
        /// </summary>
        /// <param name="login">Objeto de login</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<RetailerResource>> Login([FromBody] LoginResource login)
        {
            try
            {
                var retailer = await _loginService.Login(login.Email, login.Password);

                return Ok(retailer);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
