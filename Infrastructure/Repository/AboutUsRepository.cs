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
    public async Task EditAboutUs(AboutUs aboutUs)
    {
        var originAboutUs = await _datacontext.AboutUs.FirstOrDefaultAsync();

        if (originAboutUs != null)
        {
            originAboutUs.Title = aboutUs.Title;
            originAboutUs.Tilte2 = aboutUs.Tilte2;
            originAboutUs.Description = aboutUs.Description;

            _datacontext.Update(originAboutUs);
            await _datacontext.SaveChangesAsync();
        }

        else
        {
            await _datacontext.AboutUs.AddAsync(aboutUs);
            await _datacontext.SaveChangesAsync();
        }

    }

    #endregion
}
