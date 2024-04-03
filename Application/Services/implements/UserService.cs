using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
using Application.Security;
using Application.Services.Interfaces;
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
