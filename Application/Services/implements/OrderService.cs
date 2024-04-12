using Application.Dtos.LocationDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using Application.Services.Interfaces;
using Domain.Entities.Order;
using Domain.Entities.Order.Location;
using Domain.Entities.Order.OrderDetail;
using Domain.Entities.Product;
using Domain.Entities.User;
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



        #region Get All Orders

        public async Task<List<OrderDTO>> GetAllOrdersForAdminPanel()
        {

            List<Order> Orders = await _IOrderRepository.GetAllOrdersForAdminPanel();

            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            foreach (var item in Orders)
            {

                OrderDTO orderDTO = new OrderDTO()
                {

                    Id = item.Id,
                    UserId = item.UserId,
                    CreateTime = item.CreateTime,
                    Sum = item.Sum,

                };

                orderDTOs.Add(orderDTO);

            }


            return orderDTOs;

        }

        #endregion


        #region AddProductToCart
        public async Task AddProductToCart(int UserId, int ProductId, int Weight)
        {

            bool OrderIsExist = await _IOrderRepository.OrderIsExist(UserId);

            if (OrderIsExist == false)
            {

                await AddUser(UserId);


                await AddOrderDetail(UserId, ProductId, Weight);


                await _IOrderRepository.UpdateSum(UserId);



            }


            else
            {

                await AddOrderDetail(UserId, ProductId, Weight);


                await _IOrderRepository.UpdateSum(UserId);


            }





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
                        Producttitle = item.Producttitle,
                        Weight = item.Weight

                    };

                    orderDetailDTOs.Add(orderDetailDTO);

                }

            }

            return orderDetailDTOs;
        }
        #endregion


        #region RemoveOrderDetail

        public async Task RemoveOrderDetail(int OrderDetailId)
        {
            await _IOrderRepository.RemoveOrderDetail(OrderDetailId);
        }

        #endregion


        #region AddOrderLocation
        public async Task AddOrderLocation(LocationDTO locationDTO, int UserId)
        {

            Location location = new Location()
            {
                FirstName = locationDTO.FirstName,
                LastName = locationDTO.LastName,
                State = locationDTO.State,
                City = locationDTO.City,
                Address = locationDTO.Address,
                PhoneNumber = locationDTO.PhoneNumber,
                PostCode = locationDTO.PostCode,

            };

            await _IOrderRepository.AddOrderLocation(location, UserId);

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
                  
                };

                return tempOrderDTO;
            }

            return null;
        }

        #endregion


        #region Add To cart Methods

        private async Task AddUser(int UserId)
        {


            #region AddOrder
            //Object Mapping
            Order Neworder = new Order()
            {
                UserId = UserId,
                IsFinaly = false,
                Sum = 0,
            };

            await _IOrderRepository.AddOrder(Neworder);

            #endregion

        }

        private async Task AddOrderDetail(int UserId, int ProductId, int weight)
        {


            #region AddOrderDetail
            Product? product = await _IOrderRepository.GetProductById(ProductId);
            Order? order = await _IOrderRepository.GetOrderByUserID(UserId);

            if (product != null && order != null)
            {

                //object mapping
                OrderDetail neworderdetail = new OrderDetail()
                {

                    OrderId = order.Id,
                    ProductImage = product.Image,
                    Price = product.Price,
                    Producttitle = product.ProductName,
                    ProductId = ProductId,
                    Weight = weight


                };

                await _IOrderRepository.AddOrderDetail(neworderdetail);

            }

            #endregion


        }

        #endregion

    }
}
