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
        public Task<List<User>> GetAllUser();
        public Task<User> GetUserById(int UserId);

        public Task<User?> GetUserByPhone(string PhoneNumber);


        public Task<bool> Register(User user);

        public Task<bool> LogIn(User user);
        public Task<bool> LogInWithSms(string PhoneNumber);



        public Task EditUser(User user);
        public Task RemoveUser(int UserId);




        public void SaveChange();

    }
}
