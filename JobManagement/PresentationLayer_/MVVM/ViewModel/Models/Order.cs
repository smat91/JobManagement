using System;
using System.Collections.Generic;
using DataAccessLayer.Interfaces;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DataAccessLayer.Models.Customer Customer { get; set; }
        public ICollection<DataAccessLayer.Models.Position> Positions { get; set; }
    }
}
