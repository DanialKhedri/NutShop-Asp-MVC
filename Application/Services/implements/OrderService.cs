using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Services.Interfaces;
using Domain.Entities.Order;
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
        public async Task AddProductToCart(int UserId, int ProductId)
        {
           await _IOrderRepository.AddProductToCart(UserId, ProductId);

        }
        #endregion



        #region GetAllOrderDetails
        public async Task<List<OrderDetailDTO>> GetAllOrderDetails(int UserId)
        {
            List<OrderDetail>? Orderdetails = await _IOrderRepository.GetAllOrderDetails(UserId);

            List<OrderDetailDTO>? orderDetailDTOs = new List<OrderDetailDTO>();

            if (Orderdetails != null)
            {
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

            }

            return orderDetailDTOs;
        }
        #endregion

        #region RemoveOrderDetail

        public async Task RemoveOrderDetail(int Id)
        {
           await _IOrderRepository.RemoveOrderDetail(Id);
        }
        #endregion


        #region GetOrderByUserId

        public async Task<OrderDTO?> GetOrderByUserID(int UserId)
        {
            Order? order = await _IOrderRepository.GetOrderByUserID(UserId);

            if (order != null)
            {
                //Object Mapping
                OrderDTO? tempOrderDTO = new OrderDTO()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    CreateTime = order.CreateTime,
                    Sum = order.Sum,
                    IsFinaly = order.IsFinaly,

                };

                return tempOrderDTO;
            }

            return null;
        }

        #endregion 

     
    }
}
