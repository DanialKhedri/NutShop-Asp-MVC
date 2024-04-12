using Application.Dtos.RoleDTO;
using Application.Services.Interfaces;
using Domain.Entities.User.Role;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements
{
    public class RoleService : IRoleService
    {
        #region Ctor


        private readonly IRoleRepository _IRoleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _IRoleRepository = roleRepository;
        }

        #endregion

        public async Task<List<RoleDTO>> GetAllRoles()
        {
            List<Role>? Roles = await _IRoleRepository.GetAllRoles();

            List<RoleDTO> RoleDTOs = new List<RoleDTO>();

            if (Roles != null)
            {

                foreach (var item in Roles)
                {

                    RoleDTO roleDTO = new RoleDTO()
                    {
                        Id = item.Id,
                        RoleTitle = item.RoleTitle,
                        RoleUniqueName = item.RoleUniqueName

                    };

                    RoleDTOs.Add(roleDTO);


                }

              

            }

            return RoleDTOs;

        }






    }

}
