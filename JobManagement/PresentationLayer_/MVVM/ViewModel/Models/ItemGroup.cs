namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class ItemGroup : DataAccessLayer.Models.ItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroup ParentItemGroup { get; set; }
    }
}
