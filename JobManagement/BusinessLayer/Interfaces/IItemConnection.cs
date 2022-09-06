﻿using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    internal interface IItemConnection : IBaseConnection<ItemDto>
    {
        ItemDto GetSingleById(int id);
    }
}
