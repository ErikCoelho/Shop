﻿using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Domain.Queries
{
    public class OrderQueries
    {
        public static Expression<Func<Order, bool>> GetAll(string customer)
        {
            return x => x.CustomerDoc == customer;
        }
    }
}
