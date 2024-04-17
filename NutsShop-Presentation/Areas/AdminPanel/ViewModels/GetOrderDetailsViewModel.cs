using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Domain.Entities.Order.OrderDetail;

namespace NutsShop_Presentation.Areas.AdminPanel.ViewModels
{
    public class GetOrderDetailsViewModel
    {

        public OrderDTO? OrderDTO { get; set; }
            
        public List<OrderDetailDTO>? orderDetails { get; set; }





    }
}
