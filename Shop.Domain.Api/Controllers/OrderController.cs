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
        public async Task<IEnumerable<Order>> GetAllAsync(
            [FromServices] IOrderRepository repository)
        {
            var customer = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return await repository.GetAllAsync(customer);
        }

        [HttpPost("v1/orders")]
        [Authorize]
        public async Task<GenericCommandResult> CreateAsync(
            [FromBody] CreateOrderCommand command,
            [FromServices] OrderHandler handler)
        {
            var customer = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            var result = await handler.HandleOrder(command, customer);
            return (GenericCommandResult)result;
        }
    }
}
