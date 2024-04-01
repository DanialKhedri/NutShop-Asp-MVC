using Domain.Entities.User;
using Domain.IRepository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Ctor 

        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Register

        public async Task<bool> Register(User user)
        {
            if (!_dataContext.Users.Any(p => p.UserName == user.UserName
                                             || p.Phone == user.Phone))
            {
                //add user to database
                await _dataContext.Users.AddAsync(user);

                

                return true;

            }
            else
                return false;

             
        }
        #endregion

        #region SaveChange

        public void SaveChange() 
        {
            _dataContext.SaveChanges();
        }

        #endregion

    }
}
