using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class OrderViewModel
    {
        public List<OrderDto> OrderDtoList { get; set; }

        public OrderViewModel()
        {
            Order order = new Order();
            OrderDtoList = order.GetAllOrders();
        }
    }
}
