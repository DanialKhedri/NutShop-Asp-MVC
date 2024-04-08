using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IOrderRepository
    {

        public Task AddProductToCart(int UserId, int ProductId);

        public Task<List<OrderDetail>> GetAllOrderDetails(int UserId);


        public Task RemoveOrderDetail(int OrderDetailId);

        public Task<Order?> GetOrderByUserID(int UserId);

        public Task SaveChange();


    }
}
