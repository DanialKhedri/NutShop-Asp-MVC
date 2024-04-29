using Domain.Entities.Order;
using Domain.Entities.Order.Location;
using Domain.Entities.Order.OrderDetail;
using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IOrderRepository
    {
        //Get All
        public Task<List<Order>> GetAllUnFinaledOrders();
        public Task<List<Order>> GetAllFinaledOrders();
        public Task<List<OrderDetail>> GetAllOrderDetailsByOrderId(int OrderId);

        //Get Order
        public Task<Order?> GetOrderByUserId(int UserId);

        public Task<Order?> GetUnFinaledOrderByUserID(int UserId);
        public Task<Order?> GetFinalyOrderByUserID(int UserId);

        public Task<Order?> GetUnFinaledOrderByOrderId(int OrderId);
        public Task<Order?> GetFinalyOrderByOrderId(int OrderId);




        //Remove
        public Task RemoveOrder(int OrderId);

        public Task RemoveOrderDetail(int OrderDetailId);


        //Add Order
        public Task AddOrder(Order order);

        //IsExist
        public Task<bool> OrderIsExist(int UserId);

        //Finalize Order
        public Task FinalizeOrder(int OrderId);


        //Add orderdetail
        public Task AddOrderDetail(OrderDetail orderDetail);

        //Update Sum
        public Task UpdateSum(int UserId);


        public Task SaveChange();



        //Add orderLocation
        public Task AddOrderLocation(Location location, int UserId);
        //GetLocation By Order Id
        public Task<Location?> GetLocationByOrderId(int OrderId);
    }
}
