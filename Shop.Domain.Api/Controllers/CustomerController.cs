﻿using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Handlers;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class CustomerController
    {
        [HttpPost("v1/account/create")]
        public GenericCommandResult Create(
            [FromBody] CreateCustomerCommand command,
            [FromServices] CustomerHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}