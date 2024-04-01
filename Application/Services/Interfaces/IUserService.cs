﻿using Application.Dtos.UserLogInDTO;
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
        public Task<bool> Register(UserRegisterDTO userRegisterDTO);

        public Task<bool> LogIn(UserLogInDTO userLogInDTO);


        public void SaveChange();


    }
}