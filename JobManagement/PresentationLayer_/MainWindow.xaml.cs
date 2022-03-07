using System.Windows;
using DataAccessLayer.Models;
using PresentationLayer.MVVM.ViewModel.Connections;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var testGroup1 = new ItemGroup
            //{
            //    Name = "Elektronik"
            //};
            //ItemGroupConnection.AddNewItemGroup(testGroup1);

            //var testGroup2 = new ItemGroup
            //{
            //    Name = "Aktive Bauteile",
            //    ParentItemGroup = testGroup1
            //};
            //ItemGroupConnection.AddNewItemGroup(testGroup2);

            //var testGroup3 = new ItemGroup
            //{
            //    Name = "Passive Bauteile",
            //    ParentItemGroup = testGroup1
            //};
            //ItemGroupConnection.AddNewItemGroup(testGroup3);

            //var testGroup4 = new ItemGroup
            //{
            //    Name = "Wiederstände",
            //    ParentItemGroup = testGroup3
            //};
            //ItemGroupConnection.AddNewItemGroup(testGroup4);

            //var testItem1 = new Item
            //{
            //    Name = "Drahtwiderstand 10Ohm",
            //    Group = testGroup4,
            //    Price = 0.05m,
            //    Vat = 5.80m
            //};
            //ItemConnection.AddNewItem(testItem1);

            //var testItem2 = new Item
            //{
            //    Name = "Drahtwiderstand 20Ohm",
            //    Group = testGroup4,
            //    Price = 0.05m,
            //    Vat = 5.80m
            //};
            //ItemConnection.AddNewItem(testItem2);

            var test = ItemGroupConnection.GetItemsWithLevel();
            var test1 = StatisticsConnection.GetStatisticData();
        }
    }
}
