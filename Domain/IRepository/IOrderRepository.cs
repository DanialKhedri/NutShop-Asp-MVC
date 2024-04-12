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

        public Task<List<OrderDetail>> GetAllOrderDetails(int UserId);


        public  Task<List<Order>> GetAllOrdersForAdminPanel();


        public Task RemoveOrderDetail(int OrderDetailId);


        public Task AddOrder(Order order);


        public Task<bool> OrderIsExist(int UserId);


        public Task AddOrderDetail(OrderDetail orderDetail);


        public Task AddOrderLocation(Location location,int UserId);


        public Task UpdateSum(int UserId);


        public Task<Order?> GetOrderByUserID(int UserId);


        public Task<Product?> GetProductById(int ProductId);


        public Task SaveChange();


    }
}
