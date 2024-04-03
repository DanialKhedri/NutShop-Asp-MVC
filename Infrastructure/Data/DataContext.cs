using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
using Domain.Entities.Product;
using Domain.Entities.Product.Category;
using Domain.Entities.Product.SelectedCategory;
using Domain.Entities.User;
using Domain.Entities.User.Role;
using Domain.Entities.User.SelectedRole;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        #region Ctor

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        #endregion


        #region DBSets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        public DbSet<SelectedRole> SelectedRoles { get; set; }



        public DbSet<Product> Products { get; set; }


        public DbSet<Category> Categories { get; set; }


        public DbSet<SelectedCategory> SelectedCategories { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

       



        #endregion
    }
}
