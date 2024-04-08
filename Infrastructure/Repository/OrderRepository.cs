using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
using Domain.Entities.Product;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        #region Ctor 
        private readonly DataContext _datacontext;
        public OrderRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        #endregion


        #region AddProductToCart
        public async void AddProductToCart(int UserId, int ProductId)
        {

            Order? order = _datacontext.Orders
                          .FirstOrDefault(o => o.UserId == UserId);

            if (order == null)
            {

                //Add Order For User

                Order neworder = new Order()
                {

                    UserId = UserId,
                    CreateTime = DateTime.Now,
                    IsFinaly = false,
                    Sum = 0,

                };

                _datacontext.Orders.Add(neworder);
                await _datacontext.SaveChangesAsync();



                Product? product = _datacontext.Products.Find(ProductId);

                //Add OrderDetail

                OrderDetail neworderdetail = new OrderDetail()
                {
                    OrderId = _datacontext.Orders.FirstOrDefault(o => o.UserId == UserId).Id,
                    ProductImage = product.Image,
                    Price = product.Price,
                    Producttitle = product.ProductName,
                    ProductId = ProductId,

                };

                _datacontext.OrderDetails.Add(neworderdetail);

                SaveChange();

            }


            else
            {
                //Add OrderDetail

                Product? product = _datacontext.Products.Find(ProductId);


                OrderDetail orderDetail = new OrderDetail()
                {

                    OrderId = order.Id,
                    ProductImage = product.Image,
                    Price = product.Price,
                    Producttitle = product.ProductName,
                    ProductId = ProductId,

                };

                _datacontext.OrderDetails.Add(orderDetail);
                SaveChange();

            }

        }
        #endregion


        #region GetAllOrderDetails

        public async Task<List<OrderDetail>> GetAllOrderDetails(int UserId)
        {




            Order? order = _datacontext.Orders.FirstOrDefault(o => o.UserId == UserId && o.IsFinaly == false);


            if (order != null)
            {
                List<OrderDetail> orderdetails = await _datacontext.OrderDetails.
                                                   Where(o => o.OrderId == order.Id).ToListAsync();



                return orderdetails;


            }

            else return null;



        }

        #endregion

        #region RemoveOrderDetail

        public void RemoveOrderDetail(int Id)
        {
            OrderDetail orderDetail = _datacontext.OrderDetails.Find(Id);

            _datacontext.OrderDetails.Remove(orderDetail);

            _datacontext.SaveChanges();

            int orderId = orderDetail.OrderId;

            bool Empty = _datacontext.OrderDetails.Any(o => o.OrderId == orderId);

            if (!Empty)
            {
                var order = _datacontext.Orders.Find(orderId);
                if (order != null)
                {

                    _datacontext.Orders.Remove(order);
                    _datacontext.SaveChanges();

                }

            }

        }

        #endregion

        #region SaveChange

        public async void SaveChange()
        {
            _datacontext.SaveChangesAsync();
        }
        #endregion
    }
}
