using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class ItemGroupDto :IItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroup ParentItemGroup { get; set; }
    }
}
