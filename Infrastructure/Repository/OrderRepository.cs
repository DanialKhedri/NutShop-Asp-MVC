using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
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
        public void AddProductToCart(int UserId, int ProductId)
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
                SaveChange();



                //Add OrderDetail

                OrderDetail neworderdetail = new OrderDetail()
                {
                    OrderId = _datacontext.Orders.FirstOrDefault(o => o.UserId == UserId).Id,
                    Count = 1,
                    Price = _datacontext.Products.Find(ProductId).Price,
                    ProductId = ProductId,

                };

                _datacontext.OrderDetails.Add(neworderdetail);

                SaveChange();

            }


            else
            {
                //Add OrderDetail

                OrderDetail orderDetail = new OrderDetail()
                {

                    OrderId = order.Id,
                    Count = 1,
                    Price = _datacontext.Products.Find(ProductId).Price,
                    ProductId = ProductId,

                };

                _datacontext.OrderDetails.Add(orderDetail);







            }

        }
        #endregion


        #region GetAllOrderDetails

        public async Task<List<OrderDetail>> GetAllOrderDetails(int UserId)
        {

            Order? Order = await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId);

            List<OrderDetail> orderdetails = await _datacontext.OrderDetails.Where(o => o.OrderId == Order.Id).ToListAsync();

            return orderdetails;


        }

        #endregion

        #region SaveChange

        public void SaveChange()
        {
            _datacontext.SaveChanges();
        }
        #endregion
    }
}
