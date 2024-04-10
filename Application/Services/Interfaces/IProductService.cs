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
        public Task<List<ProductIndexDTO>> GetAllProducts();

        public Task<ProductIndexDTO> GetProductById(int Id);

        public Task<List<ProductIndexDTO>> GetProductsByCategoryId(int CategoryId);

    }
}
