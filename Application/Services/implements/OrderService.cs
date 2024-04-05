using Application.Services.Interfaces;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements
{
    public class OrderService : IOrderService
    {
        #region Ctor 

        private readonly IOrderRepository _IOrderRepository;

        public OrderService(IOrderRepository IOrderRepository)
        {
            _IOrderRepository = IOrderRepository;
        }

        #endregion


        public async void AddProductToCart(int UserId, int ProductId)
        {
            _IOrderRepository.AddProductToCart(UserId, ProductId);

        }

        public void SaveChange() 
        {
            _IOrderRepository.SaveChange();
        }

    }
}
