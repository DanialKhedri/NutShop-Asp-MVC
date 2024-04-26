using Application.Dtos.AboutUsDTO;
using Application.Services.Interfaces;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

public class AboutUsService : IAboutUsService
{

    #region Ctor

    private readonly IAboutUsRepository _IAboutUsRepository;

    public AboutUsService(IAboutUsRepository aboutUsRepository)
    {
        _IAboutUsRepository = aboutUsRepository;
    }

    #endregion


    #region GetAboutUs
    public async Task<AboutUsDTO?> GetAboutUs()
    {


        var aboutus = await _IAboutUsRepository.GetAboutUs();

        if (aboutus != null)
        {
            AboutUsDTO aboutUsDTO = new AboutUsDTO()
            {
                Id = aboutus.Id,
                Title = aboutus.Title,
                Tilte2 = aboutus.Tilte2,
                Description = aboutus.Description,

            };

            return aboutUsDTO;
        }

        return null;
    }
    #endregion

}
