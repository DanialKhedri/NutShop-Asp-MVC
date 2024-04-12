using Application.Dtos.LocationDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {

        public Task AddProductToCart(int UserId, int ProductId, int Weight);

        public Task<List<OrderDetailDTO>> GetAllOrderDetails(int UserId);

        public Task RemoveOrderDetail(int OrderDetailId);

        public Task<OrderDTO?> GetOrderByUserID(int UserId);


        public Task<List<OrderDTO>> GetAllOrdersForAdminPanel();


        public Task AddOrderLocation(LocationDTO locationDTO, int UserId);


    }

}
