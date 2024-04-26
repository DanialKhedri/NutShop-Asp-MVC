using Application.Dtos.ShopDTO;
using Application.Services.Interfaces;
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

            };

            await _IshopRepository.EditShopDetail(shop);

        }



    }
    #endregion

}
