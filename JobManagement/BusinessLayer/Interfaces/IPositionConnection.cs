using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    internal interface IPositionConnection : IBaseConnection<PositionDto>
    {
        PositionDto GetSingleById(int id);
    }
}
