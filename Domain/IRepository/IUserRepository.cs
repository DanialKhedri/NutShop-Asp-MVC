using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IUserRepository
    {
        public Task<bool> Register(User user);

        public Task<bool> LogIn(User user);


        public void SaveChange();

    }
}
