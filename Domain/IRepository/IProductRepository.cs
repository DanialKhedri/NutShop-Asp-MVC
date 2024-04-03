﻿using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();

       


    }
}