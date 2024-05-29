using Domain.Entities.Shop;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class ShopRepository : IShopRepository
{

    #region Ctor
    private readonly DataContext _datacontext;

    public ShopRepository(DataContext dataContext)
    {
        _datacontext = dataContext;

    }

    #endregion


    #region GetShopDetail
    public async Task<Shop?> GetShopDetail()
    {
        return await _datacontext.Shop.FirstOrDefaultAsync();
    }
    #endregion

    #region EditShopDetail

    public async Task EditShopDetail(Shop shop)
    {
        var tempshop = await _datacontext.Shop.FirstOrDefaultAsync(s => s.Id == shop.Id);

        if (tempshop != null)
        {
            tempshop.Phone = shop.Phone;
            tempshop.Address = shop.Address;
            tempshop.ShopName = shop.ShopName;

            //Intro
            tempshop.IntroTitle = shop.IntroTitle;
            tempshop.IntroDescription = shop.IntroDescription;

            if(shop.IntroImage != null)
            tempshop.IntroImage = shop.IntroImage;

            //Why

            tempshop.WhyNutsTitle = shop.WhyNutsTitle;
            tempshop.WhyNutsDescription = shop.WhyNutsDescription;

            tempshop.WhyUsTitle = shop.WhyUsTitle;
            tempshop.WhyUsDescription =shop.WhyUsDescription;

            tempshop.WhyUsTitle2 = shop.WhyUsTitle2;
            tempshop.WhyUsDescription2 = shop.WhyUsDescription2;


            _datacontext.Update(tempshop);
            await _datacontext.SaveChangesAsync();

        }
        else
        {
            await _datacontext.Shop.AddAsync(shop);
            await _datacontext.SaveChangesAsync();

        }
    }
    #endregion
}
