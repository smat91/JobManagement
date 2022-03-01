using System;
using System.Collections.Generic;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
