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


}
