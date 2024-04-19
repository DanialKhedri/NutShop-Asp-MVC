using Application.Dtos.CategoryDTO;
using Application.Dtos.ProductDTO;
using Domain.Entities.Product;

namespace NutsShop_Presentation.Areas.AdminPanel.ViewModels
{
    public class EditProductViewModel
    {
        public ProductDTO? productDTO { get; set; }

        public List<CategoryDTO>? categories{ get; set; }


    }
}
