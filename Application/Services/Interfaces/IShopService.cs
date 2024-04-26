using Application.Dtos.ShopDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface IShopService
{

    public Task<ShopDTO?> GetShopDetail();

}
