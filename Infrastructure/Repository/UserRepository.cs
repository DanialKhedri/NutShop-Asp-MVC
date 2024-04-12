using Domain.Entities.User;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Ctor 

        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _IhttpContextAccessor;


        public UserRepository(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _IhttpContextAccessor = httpContextAccessor;
        }

        #endregion


        #region GetAllUsers

        public async Task<List<User>> GetAllUser()
        {



            return await _dataContext.Users.ToListAsync();

        }

        #endregion


        #region Register

        public async Task<bool> Register(User user)
        {
            if (!_dataContext.Users.Any(p => p.UserName == user.UserName
                                             || p.Phone == user.Phone))
            {
                //add user to database
                await _dataContext.Users.AddAsync(user);



                return true;

            }
            else
                return false;


        }
        #endregion


        #region LogIn
        public async Task<bool> LogIn(User user)
        {

            User? Tempuser = await _dataContext.Users.FirstOrDefaultAsync(p => p.UserName == user.UserName ||
                                                                          p.Phone == user.Phone &&
                                                                          p.Password == user.Password);



            if (Tempuser != null)
            {

                //Set Cookies

                #region SetCoockie
                var claims = new List<Claim>

            {

            new (ClaimTypes.NameIdentifier, Tempuser.Id.ToString()),
            new (ClaimTypes.Name, Tempuser.UserName),

              };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimIdentity);

                var authProps = new AuthenticationProperties();

                //authProps.IsPersistent = model.RememberMe;

                _IhttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);

                #endregion


                return true;

            }
            else
            {
                return false;
            }


        }
        #endregion


        #region SaveChange

        public void SaveChange()
        {
            _dataContext.SaveChanges();
        }

        #endregion

    }
}
