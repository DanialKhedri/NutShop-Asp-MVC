using Application.Dtos.ProductDTO;
using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
using Application.Extensions.NameGenerator;
using Application.Security;
using Application.Services.Interfaces;
using Domain.Entities.Product;
using Domain.Entities.User;
using Domain.IRepository;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements
{
    public class UserService : IUserService
    {
        #region Ctor 

        private readonly IUserRepository _IUserRepository;

        public UserService(IUserRepository IuserRepository)
        {
            _IUserRepository = IuserRepository;
        }
        #endregion


        #region GetAllUsers

        public async Task<List<UserAdminPanelDTO>> GetAllUser()
        {

            List<User> users = await _IUserRepository.GetAllUser();

            List<UserAdminPanelDTO> userAdminPanelDTOs = new List<UserAdminPanelDTO>();

            foreach (var item in users)
            {

                UserAdminPanelDTO userAdminPanelDTO = new UserAdminPanelDTO()
                {

                    Id = item.Id,
                    UserName = item.UserName,
                    Phone = item.Phone,
                    CreateDate = item.CreateDate,
                    Password = item.Password,

                };


                userAdminPanelDTOs.Add(userAdminPanelDTO);
            }

            return userAdminPanelDTOs;
        }

        #endregion



        #region GetUserById

        public async Task<UserAdminPanelDTO> GetUserById(int UserId)
        {
            //Get User By Id
            User User = await _IUserRepository.GetUserById(UserId);

            //object mapping
            UserAdminPanelDTO userDTO = new UserAdminPanelDTO()
            {
                Id = User.Id,
                UserName = User.UserName,
                Phone = User.Phone,
                Password = User.Password,
                CreateDate = User.CreateDate

            };

            //Return

            return userDTO;


        }
        #endregion

        #region EditUser

        public async Task EditUser(UserAdminPanelDTO UserDTO)
        {
            User user = new User()
            {

                Phone = UserDTO.Phone,
                UserName = UserDTO.UserName,


            };

            await _IUserRepository.EditUser(user);

        }

        #endregion

        #region Remove User

        public async Task RemoveProduct(int UserId)
        {
            await _IUserRepository.RemoveUser(UserId);
        }

        #endregion


        #region Register 
        public async Task<bool> Register(UserRegisterDTO userRegisterDTO)
        {

            //Object Mapping
            User user = new User()
            {
                UserName = userRegisterDTO.UserName,
                Phone = userRegisterDTO.Phone,
                Password = PasswordHasher.EncodePasswordMd5(userRegisterDTO.Password),

            };


            return await _IUserRepository.Register(user);


        }


        #endregion


        #region LogIn

        public async Task<bool> LogIn(UserLogInDTO userLogInDTO)
        {

            User user = new User()
            {
                UserName = userLogInDTO.UserName,
                Phone = userLogInDTO.UserName,
                Password = PasswordHasher.EncodePasswordMd5(userLogInDTO.Password),
            };

            return await _IUserRepository.LogIn(user);

        }

        #endregion

        #region SavChanges

        public void SaveChange()
        {
            _IUserRepository.SaveChange();
        }
        #endregion

    }
}
