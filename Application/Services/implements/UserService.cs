﻿using Application.Dtos.UserRegisterDTO;
using Application.Services.Interfaces;
using Domain.Entities.User;
using Domain.IRepository;
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
                Password = userRegisterDTO.Password,
            };


            return await _IUserRepository.Register(user);


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
