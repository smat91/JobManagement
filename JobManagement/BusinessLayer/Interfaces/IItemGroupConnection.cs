using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IItemGroupConnection : IBaseConnection<ItemGroupDto>
    {
        ItemGroupDto GetSingleById(int id);
        List<ItemGroupHierarchyDto> GetItemsWithLevel();
    }
}
