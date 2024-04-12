using Domain.Entities.User.Role;
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




    }

}
