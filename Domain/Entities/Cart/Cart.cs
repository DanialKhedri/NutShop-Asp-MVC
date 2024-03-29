﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cart
{
    public class Cart
    {

        public int Id { get; set; }

        public int UserId { get; set; }


        public List<Product.Product> Products { get; set; }


        public int? SumPrice { get; set; }


        public bool Isdelete { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public CartStatus.cartstatus cartstatus { get; set; } = CartStatus.cartstatus.Waiting;

    }
}
