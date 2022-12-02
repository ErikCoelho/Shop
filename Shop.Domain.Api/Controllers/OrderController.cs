using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Entities;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class OrderController: ControllerBase
    {
        [HttpGet("v1/orders")]
        public IEnumerable<Order> GetAll(
            [FromServices]IOrderRepository repository)
        {
            return repository.GetAll("28791322820");
        }

        [HttpPost("v1/orders")]
        public  GenericCommandResult Create(
            [FromBody]CreateOrderCommand command,
            [FromServices]OrderHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}
