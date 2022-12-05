using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Order;
using Shop.Domain.Entities;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("v1/orders")]
        [Authorize]
        public IEnumerable<Order> GetAll(
            [FromServices] IOrderRepository repository)
        {
            var customer = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return repository.GetAll(customer);
        }

        [HttpPost("v1/orders")]
        [Authorize]
        public GenericCommandResult Create(
            [FromBody] CreateOrderCommand command,
            [FromServices] OrderHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}
