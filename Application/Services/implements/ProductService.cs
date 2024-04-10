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

        #region GetProductById

        public async Task<ProductIndexDTO> GetProductById(int Id) 
        {
            //Get product By Id
            Product product = await _IProductRepository.GetProductById(Id);

            //object mapping
            ProductIndexDTO productIndexDTO = new ProductIndexDTO()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Image = product.Image,
                Price = product.Price,
                Description =product.Description
            };

            //Return

            return productIndexDTO;


        }
        #endregion

        #region GetproductByCategoryId

        public async Task<List<ProductIndexDTO>> GetProductsByCategoryId(int CategoryId)
        {

            List<Product> products = await _IProductRepository.GetProductsByCategoryId(CategoryId);

            List<ProductIndexDTO> productIndexDTOs = new List<ProductIndexDTO>();

            foreach (var item in products)
            {
                ProductIndexDTO dTO = new ProductIndexDTO
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Image = item.Image,
                    Description = item.Description,

                };


                productIndexDTOs.Add(dTO);


            }

            return productIndexDTOs;

        }

        #endregion
    }
}
