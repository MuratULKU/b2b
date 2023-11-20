﻿using Entity;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IVirtualPosRepository
    {
        List<VirtualPos> GetAll();
        VirtualPos Get(Guid id);
        VirtualPos GetByBankCode(int id, bool isBusiness);
        List<VirtualPos> GetByBrandCode(int brandCode);
        VirtualPos Create(VirtualPos virtualPos);
        VirtualPos Update(VirtualPos virtualPos);
        VirtualPos Delete(VirtualPos virtualPos);
    }
}
