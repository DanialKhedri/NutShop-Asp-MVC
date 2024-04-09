using Domain.Entities.Order;
using Domain.Entities.Order.Location;
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

        public async Task RemoveOrderDetail(int OrderDetailId)
        {

            #region Remove OrderDetail
            OrderDetail orderDetail = await _datacontext.OrderDetails.FindAsync(OrderDetailId);
            _datacontext.OrderDetails.Remove(orderDetail);
            await SaveChange();
            #endregion



            #region Remove Order if Empty

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

            #endregion



            #region UpdateSum By User Id
            Order? temporder = await _datacontext.Orders
                                   .FirstOrDefaultAsync(o => o.Id == orderDetail.OrderId);
            if (temporder != null)
                await UpdateSum(temporder.UserId);
            #endregion

        }

        #endregion


        #region GetOrderByUserID

        public async Task<Order?> GetOrderByUserID(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId && o.IsFinaly == false);
        }
        #endregion


        #region OrderIsExist

        public async Task<bool> OrderIsExist(int UserId)
        {
            return await _datacontext.Orders
                              .AnyAsync(o => o.UserId == UserId);
        }
        #endregion


        #region Get Product By Id

        public async Task<Product?> GetProductById(int ProductId)
        {
            return await _datacontext.Products.FindAsync(ProductId);
        }

        #endregion

        #region Get Order By UserId

        public async Task<Order?> GetOrderById(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId);
        }

        #endregion


        #region AddOrderLocation

        public async Task AddOrderLocation(Location location,int UserId)
        {

            int OrderId = GetOrderById(UserId).Id;

            location.OrderId = OrderId;

            await _datacontext.Locations.AddAsync(location);
            await _datacontext.SaveChangesAsync();

        }

        #endregion


        #region Cart Methods-Add Product


        #region AddOrder

        public async Task AddOrder(Order order)
        {

            //Add New Order
            await _datacontext.Orders.AddAsync(order);
            await SaveChange();

        }

        #endregion


        #region AddOrderDetal

        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            //add orderdetail to database

            await _datacontext.OrderDetails.AddAsync(orderDetail);
            await SaveChange();

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
                await SaveChange();

            }

        }
        #endregion


        #endregion

        #region SaveChange
        public async Task SaveChange()
        {
            await _datacontext.SaveChangesAsync();
        }
        #endregion

    }
}
