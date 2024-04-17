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
        private readonly IProductRepository _IProductRepository;
        public OrderService(IOrderRepository OrderRepository, IProductRepository productRepository)
        {
            _IOrderRepository = OrderRepository;
            _IProductRepository = productRepository;
        }

        #endregion


        //Get All Orders

        #region GetAllUnFinalOrders

        public async Task<List<OrderDTO>> GetAllUnFinaledOrders()
        {

            List<Order> Orders = await _IOrderRepository.GetAllUnFinaledOrders();

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

        #region GetAllFinalOrders

        public async Task<List<OrderDTO>> GetAllFinaledOrders()
        {

            List<Order> Orders = await _IOrderRepository.GetAllFinaledOrders();

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

        #region GetAllOrderDetails

        public async Task<List<OrderDetailDTO>> GetAllOrderDetails(int UserId)
        {
            var order = _IOrderRepository.GetOrderByUserId(UserId);

            List<OrderDetail>? Orderdetails = await _IOrderRepository.GetAllOrderDetailsByOrderId(order.Id);

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

        #region GetAllOrderDetailsByOrderId
        public async Task<List<OrderDetailDTO>> GetAllOrderDetailsByOrderId(int OrderId)
        {
            List<OrderDetail>? Orderdetails = await _IOrderRepository.GetAllOrderDetailsByOrderId(OrderId);

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


        //Get Order

        #region GetOrderByUserId
        public async Task<OrderDTO?> GetOrderByUserId(int UserId)
        {
            var Order = await _IOrderRepository.GetOrderByUserId(UserId);

            OrderDTO? orderDTO = new OrderDTO()
            {
                Id = Order.Id,
                Sum = Order.Sum,
                CreateTime = Order.CreateTime

            };

            return orderDTO;
        }
        #endregion

        #region GetUnFinaledOrderByUserId
        public async Task<OrderDTO?> GetUnFinaledOrderByUserId(int UserId)
        {

            var Order = await _IOrderRepository.GetUnFinaledOrderByUserID(UserId);

            if (Order != null)
            {

                OrderDTO? orderDTO = new OrderDTO()
                {
                    Id = Order.Id,
                    Sum = Order.Sum,
                    CreateTime = Order.CreateTime

                };
                return orderDTO;
            }


            return null;

        }
        #endregion

        #region GetFinalyOrderByUserID
        public async Task<OrderDTO?> GetFinalyOrderByUserId(int UserId)
        {

            var Order = await _IOrderRepository.GetFinalyOrderByUserID(UserId);

            if (Order != null)
            {

                OrderDTO? orderDTO = new OrderDTO()
                {
                    Id = Order.Id,
                    Sum = Order.Sum,
                    CreateTime = Order.CreateTime

                };
                return orderDTO;
            }


            return null;
        }

        #endregion

        #region Get UnFinaledOrderByOrderId
        public async Task<OrderDTO?> GetUnFinaledOrderByOrderId(int OrderId)
        {
            var Order = await _IOrderRepository.GetUnFinaledOrderByOrderId(OrderId);

            if (Order != null)
            {

                OrderDTO? orderDTO = new OrderDTO()
                {
                    Id = Order.Id,
                    Sum = Order.Sum,
                    CreateTime = Order.CreateTime

                };
                return orderDTO;
            }


            return null;

        }
        #endregion

        #region GetFinaledOrderByOrderId
        public async Task<OrderDTO?> GetFinalyOrderByOrderId(int OrderId)
        {
            var Order = await _IOrderRepository.GetFinalyOrderByOrderId(OrderId);

            if (Order != null)
            {

                OrderDTO? orderDTO = new OrderDTO()
                {
                    Id = Order.Id,
                    Sum = Order.Sum,
                    CreateTime = Order.CreateTime

                };

                return orderDTO;
            }


            return null;
        }
        #endregion


        //Add Product

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

        //Remove

        #region Remove Order

        public async Task RemoveOrder(int OrderId)
        {

            await _IOrderRepository.RemoveOrder(OrderId);



        }

        #endregion


        //Remove OrderDetail

        #region RemoveOrderDetail

        public async Task RemoveOrderDetail(int OrderDetailId)
        {
            await _IOrderRepository.RemoveOrderDetail(OrderDetailId);
        }

        #endregion

        //Add Order Location

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


        //add to cart methods

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

            Product? product = await _IProductRepository.GetProductById(ProductId);
            Order? order = await _IOrderRepository.GetUnFinaledOrderByUserID(UserId);

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
