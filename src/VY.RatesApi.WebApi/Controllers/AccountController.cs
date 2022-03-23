using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RatesApi.Business.Contracts.Services;
using VY.RatesApi.Dtos;
using VY.RatesApi.XCutting.Domain.OperationResult;

namespace VY.RatesApi.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ErrorObject>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AccounteeDto accounteeDto)
        {
            var res = await _accountService.Create(accounteeDto);
            if (res.HasErrors())
            {
                return BadRequest(res.Errors);
            }
            return Ok(res);
        }
    }
}
