using Application.Dtos.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> GetAllProducts();

        public Task<ProductDTO> GetProductById(int Id);

        public Task<List<ProductDTO>> GetProductsByCategoryId(int CategoryId);

        public Task AddProduct(ProductDTO productDTO);
        public Task EditProduct(ProductDTO productDTO);
        public Task RemoveProduct(int ProductId);


    }
}
