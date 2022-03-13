using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            this.DataContext = new CustomerViewModel();
            InitializeComponent();
        }
    }
}
