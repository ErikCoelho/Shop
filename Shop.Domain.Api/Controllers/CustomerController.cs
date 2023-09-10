using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Customer;
using Shop.Domain.Entities;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class CustomerController: ControllerBase
    {

        [HttpGet("v1/account/info")]
        [Authorize]
        public async Task<Customer> GetInfoCustomerAsync(
            [FromServices] ICustomerRepository repository)
        {
            var customer = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return await repository.GetAsync(customer);
        }

        [HttpPost("v1/account/create")]
        public async Task<GenericCommandResult> CreateAsync(
            [FromBody] CreateCustomerCommand command,
            [FromServices] CustomerHandler handler)
        {
            var result = await handler.HandleAsync(command);
            return (GenericCommandResult)result;
        }

        [HttpPost("v1/account/login")]
        public async Task<GenericCommandResult> LoginAsync(
            [FromBody] LoginCustomerCommand command,
            [FromServices] CustomerHandler handler)
        {
            var result = await handler.HandleLoginAsync(command);
            return (GenericCommandResult)result;
        }
    }
}
