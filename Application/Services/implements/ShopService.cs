using Application.Dtos.ProductDTO;
using Application.Dtos.ShopDTO;
using Application.Extensions.Generators.NameGenerator;
using Application.Services.Interfaces;
using Domain.Entities.Product;
using Domain.Entities.Shop;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

public class ShopService : IShopService
{
    #region Ctor

    private readonly IShopRepository _IshopRepository;

    public ShopService(IShopRepository shopRepository)
    {

        _IshopRepository = shopRepository;

    }

    #endregion


    #region GetShopDetail
    public async Task<ShopDTO?> GetShopDetail()
    {
        var shopdetail = await _IshopRepository.GetShopDetail();

        if (shopdetail != null)
        {
            ShopDTO shopDTO = new ShopDTO()
            {
                Id = shopdetail.Id,
                ShopName = shopdetail.ShopName,
                Address = shopdetail.Address,
                Phone = shopdetail.Phone,

                IntroTitle = shopdetail.IntroTitle,
                IntroDescription = shopdetail.IntroDescription,
                IntroImage = shopdetail.IntroImage,

                WhyNutsTitle = shopdetail.WhyNutsTitle,
                WhyNutsDescription = shopdetail.WhyNutsDescription,

                WhyUsTitle = shopdetail.WhyUsTitle,
                WhyUsDescription = shopdetail.WhyUsDescription,

                WhyUsTitle2 = shopdetail.WhyUsTitle2,
                WhyUsDescription2 = shopdetail.WhyUsDescription2,


            };

            return shopDTO;
        }

        return null;

    }


    #endregion







    #region EditShopDetail

    public async Task EditShopDetail(ShopDTO shopDTO)
    {
        if (shopDTO != null)
        {
            Shop shop = new Shop()
            {
                Id = shopDTO.Id,
                ShopName = shopDTO.ShopName,
                Address = shopDTO.Address,
                Phone = shopDTO.Phone,

                //Intro
                IntroTitle = shopDTO.IntroTitle,
                IntroDescription = shopDTO.IntroDescription,

                //why

                WhyNutsTitle = shopDTO.WhyNutsTitle,
                WhyNutsDescription = shopDTO.WhyNutsDescription,

                WhyUsTitle = shopDTO.WhyUsTitle,
                WhyUsDescription = shopDTO.WhyUsDescription,

                WhyUsTitle2 = shopDTO.WhyUsTitle2,
                WhyUsDescription2 = shopDTO.WhyUsDescription2,

            };



            #region AddImage 

            if (shopDTO.IntroImageIformFile != null)
            {
                //Save New Image
                shop.IntroImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(shopDTO.IntroImageIformFile.FileName);

                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", shop.IntroImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    shopDTO.IntroImageIformFile.CopyTo(stream);
                }
            }
            #endregion

            await _IshopRepository.EditShopDetail(shop);

        }



    }
    #endregion

}
