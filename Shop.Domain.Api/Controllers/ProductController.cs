using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("v1/products/inactive")]
        [Authorize(Roles = "admin")]
        public IEnumerable<Product> GetAllAdmin(
            [FromServices] IProductRepository repository)
        {
            return repository.GetInactiveProducts();
        }

        [HttpGet("v1/products/{id}")]
        public Product GetById(
            [FromRoute] Guid id,
            [FromServices] IProductRepository repository)
        {
            return repository.GetById(id);
        }

        [HttpPost("v1/products")]
        [Authorize(Roles = "admin")]
        public GenericCommandResult Create(
            [FromBody] EditProductCommand command,
            [FromServices] ProductHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPut("v1/products/{id:Guid}")]
        [Authorize(Roles = "admin")]
        public GenericCommandResult Edit(
            [FromRoute] Guid id,
            [FromBody] EditProductCommand command,
            [FromServices] ProductHandler handler)
        {
            return (GenericCommandResult)handler.HandleEdit(command, id);
        }

        [HttpDelete("v1/products/{id:Guid}")]
        [Authorize(Roles = "admin")]
        public GenericCommandResult Delete(
            [FromRoute] Guid id,
            [FromServices] ProductHandler handler)
        {
            return (GenericCommandResult)handler.HandleDelete(id);
        }
    }
}
