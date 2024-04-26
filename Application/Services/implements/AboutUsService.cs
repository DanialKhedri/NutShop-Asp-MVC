using Application.Dtos.AboutUsDTO;
using Application.Dtos.ShopDTO;
using Application.Services.Interfaces;
using Domain.Entities.AboutUs;
using Domain.Entities.Shop;
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

    #region EditAboutUs


    public async Task EditAboutUs(AboutUsDTO aboutUsDTO)
    {

        if (aboutUsDTO != null)
        {
            AboutUs aboutUs = new AboutUs()
            {

                Id = aboutUsDTO.Id,
                Title = aboutUsDTO.Title,
                Tilte2 = aboutUsDTO.Tilte2,
                Description = aboutUsDTO.Description,

            };

            await _IAboutUsRepository.EditAboutUs(aboutUs);
        }




    }

    #endregion
}
