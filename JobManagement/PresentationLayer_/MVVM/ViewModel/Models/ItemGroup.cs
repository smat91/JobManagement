using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class ItemGroup : IItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataAccessLayer.Models.ItemGroup ParentItemGroup { get; set; }
    }
}
