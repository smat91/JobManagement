using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Controls;
using PresentationLayer.MVVM.ViewModel.Connections;

namespace PresentationLayer.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataGridJahresvergleich.DataContext = StatisticsConnection.GetStatisticData().DefaultView;
        }
    }
}
