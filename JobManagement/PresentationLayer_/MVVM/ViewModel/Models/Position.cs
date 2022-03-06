using System;
using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Position : IPosition
    {
        public int Id { get; set; }
        public DataAccessLayer.Models.Item Item { get; set; }
        public int Amount { get; set; }
    }
}
