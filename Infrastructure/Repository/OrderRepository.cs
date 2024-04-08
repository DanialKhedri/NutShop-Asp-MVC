using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
using Domain.Entities.Product;
using Domain.Entities.User;
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


        #region Main Methods

        #region AddProductToCart

        public async Task AddProductToCart(int UserId, int ProductId)
        {

            Order? order = await _datacontext.Orders
                                 .FirstOrDefaultAsync(o => o.UserId == UserId);

            if (order == null)
            {


                //Add Order
                await AddOrder(UserId);


                //Add OrderDetail
                await AddOrderDetail(UserId, ProductId);

                //Update Sum
                await UpdateSum(UserId);


            }


            else
            {

                //Add OrderDetail
                await AddOrderDetail(UserId, ProductId);

                //Update Sum And SaveChanges
                await UpdateSum(UserId);

            }

        }
        #endregion


        #region GetAllOrderDetails

        public async Task<List<OrderDetail>> GetAllOrderDetails(int UserId)
        {

            Order? order = _datacontext.Orders
                          .FirstOrDefault(o => o.UserId == UserId && o.IsFinaly == false);


            if (order != null)
            {
                List<OrderDetail>? orderdetails = await _datacontext.OrderDetails                    
                                                                   .Where(o => o.OrderId == order.Id)
                                                                   .ToListAsync();

                return orderdetails;

            }

            else
                return null;



        }

        #endregion


        #region RemoveOrderDetail

        public async Task RemoveOrderDetail(int Id)
        {
            OrderDetail orderDetail = await _datacontext.OrderDetails.FindAsync(Id);

            _datacontext.OrderDetails.Remove(orderDetail);

            await SaveChange();

            int orderId = orderDetail.OrderId;

            bool Empty = _datacontext.OrderDetails.Any(o => o.OrderId == orderId);

            if (!Empty)
            {
                var order = _datacontext.Orders.Find(orderId);
                if (order != null)
                {

                    _datacontext.Orders.Remove(order);
                    await SaveChange();

                }

            }

            await UpdateSum(orderId);
        }

        #endregion


        #region GetOrderByUserID

        public async Task<Order?> GetOrderByUserID(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId && o.IsFinaly == false);
        }
        #endregion



        #endregion




        #region Micro Methods


        #region AddOrder

        public async Task AddOrder(int UserId)
        {
            //Object Mapping
            Order neworder = new Order()
            {
                UserId = UserId,
                CreateTime = DateTime.Now,
                IsFinaly = false,
                Sum = 0,
            };


            //Add New Order
            await _datacontext.Orders.AddAsync(neworder);
            await SaveChange();

        }

        #endregion

        #region AddOrderDetal

        public async Task AddOrderDetail(int UserId, int ProductId)
        {
            Product? product = await _datacontext.Products.FindAsync(ProductId);

            Order? order = await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId);

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

                };

                //add orderdetail to database
                await _datacontext.OrderDetails.AddAsync(neworderdetail);
                await SaveChange();

            }
          

        }

        #endregion

        #region UpdateSum
        public async Task UpdateSum(int UserId)
        {
            Order? Order = await _datacontext.Orders.FirstOrDefaultAsync(O => O.UserId == UserId);

            if (Order != null)
            {

                Order.Sum = _datacontext.OrderDetails.Where(o => o.OrderId == Order.Id)
                                               .Select(o => o.Price)
                                               .Sum();


                _datacontext.Update(Order);
                await _datacontext.SaveChangesAsync();

            }

        }
        #endregion

        #region SaveChange
        public async Task SaveChange()
        {
            await _datacontext.SaveChangesAsync();
        }
        #endregion


        #endregion

    }
}
