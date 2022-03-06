using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class OrderConnection
    {
        private readonly OrderRepository orderRepository_;

        public OrderConnection(string connectionString)
        {
            orderRepository_ = new OrderRepository(connectionString);
        }

        public IOrder GetOrderById(int id)
        {
             var order = orderRepository_.GetOrderById(id);
             return order;
        }

        public void AddNewOrder(IOrder order)
        {
            orderRepository_.AddNewOrder(order);
        }

        public void DeleteOrderByDto(IOrder order)
        {
            orderRepository_.DeleteOrderByDto(order);
        }

        public void UpdateOrderByDto(IOrder order)
        {
            orderRepository_.UpdateOrderByDto(order);
        }

        public void AddNewPositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            orderRepository_.AddNewPositionByOrderDtoAndPositionDto(order, position);                
        }

        public void DeletePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            orderRepository_.DeletePositionByOrderDtoAndPositionDto(order, position);
        }

        public void UpdatePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            orderRepository_.UpdatePositionByOrderDtoAndPositionDto(order, position);
        }
        
    }
}
