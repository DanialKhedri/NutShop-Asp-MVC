using Domain.Entities.User.Role;
using Domain.Entities.User.SelectedRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository;

public interface IRoleRepository
{

    public Task<List<Role>> GetAllRoles();

    public Task AddSelectedRole(SelectedRole SelectedRole); 





}


