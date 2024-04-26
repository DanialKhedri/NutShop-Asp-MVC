﻿using Domain.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository;

public interface IShopRepository
{
    public Task<Shop?> GetShopDetail();

    public Task EditShopDetail(Shop shop);


}
