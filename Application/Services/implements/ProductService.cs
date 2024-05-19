using Application.Dtos.ProductDTO;
using Application.Extensions.Generators.NameGenerator;
using Application.Services.Interfaces;
using Domain.Entities.Product;
using Domain.Entities.Product.SelectedCategory;
using Domain.Entities.User;
using Domain.IRepository;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

public class ProductService : IProductService
{

    #region Ctor

    private readonly IProductRepository _IProductRepository;
    private readonly ICategoryRepository _ICategoryRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _IProductRepository = productRepository;
        _ICategoryRepository = categoryRepository;

    }
    #endregion

    #region GetAllProducts

    public async Task<List<ProductDTO>> GetAllProducts()
    {

        List<Product> products = await _IProductRepository.GetAllProducts();

        List<ProductDTO> productDtoList = new List<ProductDTO>();

        foreach (var item in products)
        {

            ProductDTO productDTO = new ProductDTO()
            {

                Id = item.Id,
                ProductName = item.ProductName,
                Image = item.Image,
                Price = item.Price,

            };

            productDtoList.Add(productDTO);

        }

        return productDtoList;

    }

    #endregion

    #region GetProductById

    public async Task<ProductDTO> GetProductById(int Id)
    {
        //Get product By Id
        Product product = await _IProductRepository.GetProductById(Id);

        //object mapping
        ProductDTO productDTO = new ProductDTO()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Image = product.Image,
            Price = product.Price,
            Description = product.Description
        };

        //Return

        return productDTO;


    }
    #endregion

    #region GetproductByCategoryId

    public async Task<List<ProductDTO>> GetProductsByCategoryId(int CategoryId)
    {

        List<Product> products = await _IProductRepository.GetProductsByCategoryId(CategoryId);

        List<ProductDTO> productIndexDTOs = new List<ProductDTO>();

        foreach (var item in products)
        {
            ProductDTO dTO = new ProductDTO
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




    #region AddProduct

    public async Task AddProduct(ProductDTO productDTO, List<int> SelectedCategories)
    {

        //object mapping
        Product product = new Product()
        {

            ProductName = productDTO.ProductName,
            Description = productDTO.Description,
            Price = productDTO.Price,


        };

        #region AddImage 

        if (productDTO.ImageIformFile != null)
        {
            //Save New Image
            product.Image = NameGenerator.GenerateUniqCode() + Path.GetExtension(productDTO.ImageIformFile.FileName);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", product.Image);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                productDTO.ImageIformFile.CopyTo(stream);
            }
        }

        #endregion

        await _IProductRepository.AddProduct(product);


        //AddSelected Categories

        if (SelectedCategories.Count != 0)
        {
            var tempproduct = await _IProductRepository.GetLastProduct();
            if (tempproduct != null)
            {
                foreach (var item in SelectedCategories)
                {
                    SelectedCategory selectedCategory = new SelectedCategory()
                    {
                        CategoryId = item,
                        ProductId = tempproduct.Id,
                    };

                    await _ICategoryRepository.AddSelectedCategory(selectedCategory);

                }

                await _ICategoryRepository.SaveChangeAsync();


            }

        }

    }

    #endregion

    #region EditProduct

    public async Task EditProduct(ProductDTO productDTO)
    {
        Product product = new Product()
        {
            Id = productDTO.Id,
            ProductName = productDTO.ProductName,
            Description = productDTO.Description,
            Price = productDTO.Price,

        };


        #region AddImage 

        if (productDTO.ImageIformFile != null)
        {
            //Save New Image
            product.Image = NameGenerator.GenerateUniqCode() + Path.GetExtension(productDTO.ImageIformFile.FileName);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", product.Image);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                productDTO.ImageIformFile.CopyTo(stream);
            }
        }

        #endregion

        await _IProductRepository.EditProduct(product);
    }

    #endregion

    #region Remove Product

    public async Task RemoveProduct(int ProductId)
    {
        await _IProductRepository.RemoveProduct(ProductId);
    }

    #endregion


}
