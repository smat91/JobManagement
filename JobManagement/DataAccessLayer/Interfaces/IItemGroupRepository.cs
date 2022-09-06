using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IItemGroupRepository : IBaseRepository<ItemGroup>
    {
        List<ItemGroupHierarchyRequest> GetItemsWithLevel();
    }
}
