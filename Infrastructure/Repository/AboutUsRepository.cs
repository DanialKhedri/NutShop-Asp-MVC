using Domain.Entities.AboutUs;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class AboutUsRepository : IAboutUsRepository
{

    #region Ctor

    private readonly DataContext _datacontext;

    public AboutUsRepository(DataContext dataContext)
    {
        _datacontext = dataContext;

    }

    #endregion

    #region GetAboutUs

    public async Task<AboutUs?> GetAboutUs()
    {
        return await _datacontext.AboutUs.FirstOrDefaultAsync();
    }

    #endregion

    #region EditAboutUs

}
