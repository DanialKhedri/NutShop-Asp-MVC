using Domain.Entities.Product;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Ctor

        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region GetAllProducts
        public async Task<List<Product>> GetAllProducts()
        {
            return await _dataContext.Products.Where(p => p.Isdelete == false).ToListAsync();
        }
        #endregion

        #region GetProductById

        public async Task<Product> GetProductById(int Id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == Id);

            return product;
        }
        #endregion

        #region GetProductsByCategoryId

        public async Task<List<Product>> GetProductsByCategoryId(int CategoryId)
        {
            List<Product> Products = await _dataContext.SelectedCategories.Where(s => s.CategoryId == CategoryId)
                                                                          .Select(s => s.Product)
                                                                          .ToListAsync();



            return Products;
        }
        #endregion


        #region AddProduct

        public async Task AddProduct(Product product)
        {

            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();

        }


        #endregion


        #region EditProduct

        public async Task EditProduct(Product product)
        {

             Product originproduct = await  GetProductById(product.Id);

            originproduct.ProductName = product.ProductName;
            originproduct.Description = product.Description;
            originproduct.Price = product.Price;

            if (product.Image != null)
                originproduct.Image = product.Image;

               
          

            _dataContext.Update(originproduct);
            await _dataContext.SaveChangesAsync();
        }


        #endregion

        #region RemoveProduct

        public async Task RemoveProduct(int ProductId) 
        {
            var Product = await _dataContext.Products.FirstOrDefaultAsync( p => p.Id == ProductId);

            Product.Isdelete = true;

            _dataContext.Update(Product);
           await _dataContext.SaveChangesAsync();

        }

        #endregion

    }
}
