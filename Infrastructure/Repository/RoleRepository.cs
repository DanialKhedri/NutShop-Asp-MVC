using Domain.Entities.User.Role;
using Domain.Entities.User.SelectedRole;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {

        #region Ctor 

        private readonly DataContext _datacontext;

        public RoleRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }



        #endregion


        #region Get All Roles

        public async Task<List<Role>> GetAllRoles()
        {


            return await _datacontext.Roles.Where(r => r.Isdelete == false)
                                           .ToListAsync();



        }

        #endregion


        #region AddSelectedRole

        public async Task AddSelectedRole(SelectedRole SelectedRole)
        {
            if (SelectedRole != null)
            {


                await _datacontext.SelectedRoles.AddAsync(SelectedRole);

            }

        }


        #endregion

        #region RemoveAllSelectedRolesByUserId
        public async Task RemoveAllSelectedRolesByUserId(int  UserId)
        {
            var Lastselectedroles = await _datacontext.SelectedRoles.Where(s => s.UserId == UserId)
                                                                    .ToListAsync();
            _datacontext.SelectedRoles.RemoveRange(Lastselectedroles);
            await _datacontext.SaveChangesAsync();
        }
        #endregion

        #region SaveChangeAsync
        public async Task SaveChangeAsync()
        {
            await _datacontext.SaveChangesAsync();
        }
        #endregion

    }

}
