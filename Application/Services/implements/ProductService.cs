using Application.Dtos.ProductDTO;
using Application.Services.Interfaces;
using Domain.Entities.Product;
using Domain.IRepository;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements
{
    public class ProductService : IProductService
    {

        #region Ctor

        private readonly IProductRepository _IProductRepository;

        public ProductService(IProductRepository productRepository) 
        {
            _IProductRepository = productRepository;
        }
        #endregion

        #region GetAllProducts

        public async Task<List<ProductIndexDTO>> GetAllProducts() 
        {

            List<Product> products = await _IProductRepository.GetAllProducts();

            List<ProductIndexDTO> productDtoList = new List<ProductIndexDTO>();

            foreach (var item in products)
            {

                ProductIndexDTO productIndexDTO = new ProductIndexDTO()
                {

                    Id = item.Id,
                    ProductName = item.ProductName,
                    Image = item.Image,
                    Price = item.Price,

                };

                productDtoList.Add(productIndexDTO);

            }

            return productDtoList;

        }

        #endregion


    }
}
