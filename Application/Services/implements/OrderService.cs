using Application.Dtos.OrderDetailDTO;
using Application.Services.Interfaces;
using Domain.Entities.Order.OrderDetail;
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

        #region AddProductToCart
        public async void AddProductToCart(int UserId, int ProductId)
        {
            _IOrderRepository.AddProductToCart(UserId, ProductId);

        }
        #endregion

        #region GetAllOrderDetails
        public async Task<List<OrderDetailDTO>> GetAllOrderDetails(int UserId) 
        {
            List<OrderDetail> Orderdetails = await _IOrderRepository.GetAllOrderDetails(UserId);

            List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();

            foreach (var item in Orderdetails)
            {

                OrderDetailDTO orderDetailDTO = new OrderDetailDTO()
                {
                    Id = item.Id,
                    Price = item.Price,
                    ProductImage = item.ProductImage,
                    Producttitle = item.Producttitle
                };

                orderDetailDTOs.Add(orderDetailDTO);

            }

            return orderDetailDTOs;
        }
        #endregion
        
        #region SaveChange
        public void SaveChange() 
        {
            _IOrderRepository.SaveChange();
        }
        #endregion
    }
}
