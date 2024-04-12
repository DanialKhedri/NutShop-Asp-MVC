﻿using Application.Dtos.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<List<RoleDTO>> GetAllRoles();


    }
}
