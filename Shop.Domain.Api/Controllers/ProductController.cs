using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Product;
using Shop.Domain.Entities;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public IEnumerable<Product> GetAll(
            [FromServices]IProductRepository repository)
        {
            return repository.GetActiveProducts();
        }

        [HttpGet("v1/products/{id}")]
        public Product GetById(
            [FromRoute] Guid id,
            [FromServices] IProductRepository repository)
        {
            return repository.GetById(id);
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
