using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserAdminPanelDTO>> GetAllUser();
        public Task<UserAdminPanelDTO> GetUserById(int UserId);

        public Task<bool> Register(UserRegisterDTO userRegisterDTO);

        public Task<bool> LogIn(UserLogInDTO userLogInDTO);
        public Task<bool> LogInWithSms(string PhoneNumber);

        public Task EditUser(UserAdminPanelDTO UserDTO);

        public Task RemoveProduct(int UserId);

        public void SaveChange();


    }
}
