﻿using Application.Dtos.ShopDTO;
using Domain.Entities.Shop;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

public class ShopService
{
    #region Ctor

    private readonly IShopRepository _IshopRepository;

    public ShopService(IShopRepository shopRepository)
    {

        _IshopRepository = shopRepository;

    }

    #endregion

    #region GetShopDetils
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




}
