using System;

namespace PresentationLayer.MVVM.ViewModel.Models
{
    public class Position
    {
        public int Id { get; set; }
        public virtual Item Item {
            //get {}
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
        public int Amount { get; set; }
    }
}
