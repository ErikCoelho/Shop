using Shop.Domain.Entities;
using System.Security.Claims;

namespace Shop.Domain.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Customer customer)
        {
            var result = new List<Claim>
        {
            new(ClaimTypes.Name, customer.Document.Number)
        };
            result.AddRange(
                customer.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );
            return result;
        }
    }
}
