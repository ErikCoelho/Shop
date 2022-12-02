using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public GenericCommandResult GetAll(
            [FromServices]IProductRepository repository)
        {
            return (GenericCommandResult)repository.GetActiveProducts();
        }

        [HttpPost("v1/products")]
        public GenericCommandResult Create(
            [FromBody] CreateProductCommand command,
            [FromServices] ProductHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}
