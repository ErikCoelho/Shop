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
        public async Task<IEnumerable<Product>> GetAllAsync(
            [FromServices] IProductRepository repository,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            return await repository.GetActiveProductsAsync(page, pageSize);
        }

        [HttpGet("v1/products/inactive")]
        [Authorize(Roles = "admin")]
        public async Task<IEnumerable<Product>> GetAllAdminAsync(
            [FromServices] IProductRepository repository)
        {
            return await repository.GetInactiveProductsAsync();
        }

        [HttpGet("v1/products/{id}")]
        public async Task<Product> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] IProductRepository repository)
        {
            return await repository.GetByIdAsync(id);
        }

        [HttpGet("v1/products/search/{term}")]
        public async Task<IEnumerable<Product>> SearchProductsAsync(
            [FromRoute] string term,
            [FromServices] IProductRepository repository)
        {
            return await repository.SearchProductsAsync(term);
        }

        [HttpPost("v1/products")]
        //[Authorize(Roles = "admin")]
        public async Task<GenericCommandResult> CreateAsync(
            [FromBody] EditProductCommand command,
            [FromServices] ProductHandler handler)
        {
            var result = await handler.HandleAsync(command);
            return (GenericCommandResult)result;
        }

        [HttpPut("v1/products/{id:Guid}")]
        [Authorize(Roles = "admin")]
        public async Task<GenericCommandResult> EditAsync(
            [FromRoute] Guid id,
            [FromBody] EditProductCommand command,
            [FromServices] ProductHandler handler)
        {
            var result = await handler.HandleEditAsync(command, id);
            return (GenericCommandResult)result;
        }

        [HttpDelete("v1/products/{id:Guid}")]
        [Authorize(Roles = "admin")]
        public async Task<GenericCommandResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] ProductHandler handler)
        {
            var result = await handler.HandleDeleteAsync(id);
            return (GenericCommandResult)result;
        }
    }
}
