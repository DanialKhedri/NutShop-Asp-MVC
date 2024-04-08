using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;

namespace NutsShop_Presentation.Areas.SitePanel.ViewModels
{
    public class CartViewModel
    {
        public OrderDTO? OrderDTO { get; set; }
        public List<OrderDetailDTO> OrderDetailDTOs { get; set; }

    }
}
