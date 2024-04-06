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

        public void AddProductToCart(int UserId, int ProductId);

        public Task<List<OrderDetail>> GetAllOrderDetails(int UserId);


        public void RemoveOrderDetail(int Id);


        public void SaveChange();


    }
}
