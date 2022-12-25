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
        public Customer GetInfoCustomer(
            [FromServices] ICustomerRepository repository)
        {
            var customer = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return repository.Get(customer);
        }

        [HttpPost("v1/account/create")]
        public GenericCommandResult Create(
            [FromBody] CreateCustomerCommand command,
            [FromServices] CustomerHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPost("v1/account/login")]
        public GenericCommandResult Login(
            [FromBody] LoginCustomerCommand command,
            [FromServices] CustomerHandler handler)
        {
            return (GenericCommandResult)handler.HandleLogin(command);
        }
    }
}
