using Application.Dtos.OrderDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {

        public void AddProductToCart(int UserId, int ProductId);

        public Task<List<OrderDetailDTO>> GetAllOrderDetails(int UserId);


        public void SaveChange();







    }

}
