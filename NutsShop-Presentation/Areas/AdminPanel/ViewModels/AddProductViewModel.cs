using Application.Dtos.CategoryDTO;
using Application.Dtos.ProductDTO;

namespace NutsShop_Presentation.Areas.AdminPanel.ViewModels;

public class AddProductViewModel
{
    public ProductDTO? productDTO { get; set; }

    public List<CategoryDTO>? categories { get; set; }

}
