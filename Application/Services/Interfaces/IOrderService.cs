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

        //Get All Orders

        public Task<List<OrderDTO>> GetAllUnFinaledOrders();

        public Task<List<OrderDTO>> GetAllFinaledOrders();

        public Task<List<OrderDetailDTO>> GetAllOrderDetailsByOrderId(int OrderId);

        //Get Order

        public Task<OrderDTO?> GetOrderByUserId(int UserId);
        public Task<OrderDTO?> GetUnFinaledOrderByUserId(int UserId);
        public Task<OrderDTO?> GetFinalyOrderByUserId(int UserId);

        public Task<OrderDTO?> GetUnFinaledOrderByOrderId(int OrderId);
        public Task<OrderDTO?> GetFinalyOrderByOrderId(int OrderId);



        //Add Product To Cart
        public Task AddProductToCart(int UserId, int ProductId, int Weight);


        //Remove
        public Task RemoveOrder(int OrderId);

        public Task RemoveOrderDetail(int OrderDetailId);

        //Finalize Order

        public Task FinalizeOrder(int OrderId);



        //Add Order Location
        public Task AddOrderLocation(LocationDTO locationDTO, int UserId);
        public Task<LocationDTO?> GetLocationByOrderId(int OrderId);


    }

}
