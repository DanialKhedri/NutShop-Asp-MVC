using Domain.Entities.Product;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
           return await _dataContext.Products.ToListAsync();
        }
        #endregion

        #region GetProductById

        public async Task<Product> GetProductById(int Id) 
        {
           var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == Id);

            return product;
        }
        #endregion

    }
}
