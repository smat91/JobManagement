using PresentationLayerWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerWPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM(get; set;)
        private object _currentView;
        public object CurrentView
        {
            get { return CurrentView; }
            set
            {
                CurrentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
        }
   
    }
}
