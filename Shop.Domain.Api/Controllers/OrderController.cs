﻿using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;

namespace Shop.Domain.Api.Controllers
{
    [ApiController]
    public class OrderController: ControllerBase
    {
        [HttpGet("v1/orders/")]
        public GenericCommandResult GetAll(
            [FromServices]IOrderRepository repository)
        {
            return (GenericCommandResult)repository.GetAll("28791322820");
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