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

        //Get All Orders

        #region GetAllUnFinaledOrders
        public async Task<List<Order>> GetAllUnFinaledOrders()
        {

            return await _datacontext.Orders.Where(o => o.IsFinaly == false)
                                            .ToListAsync();


        }

        #endregion

        #region GetAllFinaledOrders
        public async Task<List<Order>> GetAllFinaledOrders()
        {

            return await _datacontext.Orders.Where(o => o.IsFinaly == true)
                                            .ToListAsync();


        }

        #endregion


        //Get Order

        #region GetOrderByUserID
        public async Task<Order?> GetOrderByUserId(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId);
        }


        #endregion

        #region GetUnFinaledOrderByUserID

        public async Task<Order?> GetUnFinaledOrderByUserID(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId && o.IsFinaly == false);
        }

        #endregion

        #region GetFinalOrderByUserID

        public async Task<Order?> GetFinalyOrderByUserID(int UserId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.UserId == UserId && o.IsFinaly == true);
        }

        #endregion

        #region GetUnFinaledOrderByUserID

        public async Task<Order?> GetUnFinaledOrderByOrderId(int OrderId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId && o.IsFinaly == false);
        }

        #endregion

        #region GetFinalOrderByOrderId

        public async Task<Order?> GetFinalyOrderByOrderId(int OrderId)
        {
            return await _datacontext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId && o.IsFinaly == true);
        }

        #endregion


        //Get all Order Details By Order Id

        #region GetAllOrderDetailsByOrderId

        public async Task<List<OrderDetail>> GetAllOrderDetailsByOrderId(int OrderId)
        {

            Order? order = _datacontext.Orders
                          .FirstOrDefault(o => o.Id == OrderId);


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


        //Remove

        #region Remove Order

        public async Task RemoveOrder(int OrderId)
        {
            var order = await _datacontext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);

            if (order != null)
            {
                order.IsDelete = true;
                _datacontext.Update(order);
                await _datacontext.SaveChangesAsync();
            }

           
        }

        #endregion

        #region RemoveOrderDetail

        public async Task RemoveOrderDetail(int OrderDetailId)
        {

            #region Remove OrderDetail
            OrderDetail? orderDetail = await _datacontext.OrderDetails.FindAsync(OrderDetailId);
            if (orderDetail != null)
            {
                _datacontext.OrderDetails.Remove(orderDetail);
                await SaveChange();
            }

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



        #region OrderIsExist

        public async Task<bool> OrderIsExist(int UserId)
        {
            return await _datacontext.Orders
                              .AnyAsync(o => o.UserId == UserId);
        }
        #endregion


        //Add Order Location

        #region AddOrderLocation

        public async Task AddOrderLocation(Location location, int UserId)
        {

            Order? order = await GetUnFinaledOrderByUserID(UserId);


            Location? templocation = await _datacontext.Locations
                                    .FirstOrDefaultAsync(l => l.OrderId == order.Id);



            if (templocation != null)
            {

                templocation.FirstName = location.FirstName;
                templocation.LastName = location.LastName;
                templocation.State = location.State;
                templocation.City = location.City;
                templocation.Address = location.Address;
                templocation.PhoneNumber = location.PhoneNumber;
                templocation.PostCode = location.PostCode;


                _datacontext.Update(templocation);
                await _datacontext.SaveChangesAsync();

            }


            else
            {

                location.OrderId = order.Id;
                await _datacontext.Locations.AddAsync(location);
                await _datacontext.SaveChangesAsync();

            }

        }

        #endregion


        //Add Product methods

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
