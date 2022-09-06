﻿using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Helper
{
    internal interface IInvoiceConnection
    {
        List<InvoiceDto> GetInvoicesByFilterTerm(string searchTerm);
    }
}
