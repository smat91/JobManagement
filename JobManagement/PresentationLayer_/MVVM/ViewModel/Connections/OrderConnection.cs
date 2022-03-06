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
        public static IOrder GetOrderById(int id)
        {
             var order = OrderRepository.GetOrderById(id);
             return order;
        }

        public static void AddNewOrder(IOrder order)
        {
            OrderRepository.AddNewOrder(order);
        }

        public static void DeleteOrderByDto(IOrder order)
        {
            OrderRepository.DeleteOrderByDto(order);
        }

        public static void UpdateOrderByDto(IOrder order)
        {
            OrderRepository.UpdateOrderByDto(order);
        }

        public static void AddNewPositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            OrderRepository.AddNewPositionByOrderDtoAndPositionDto(order, position);                
        }

        public static void DeletePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            OrderRepository.DeletePositionByOrderDtoAndPositionDto(order, position);
        }

        public static void UpdatePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            OrderRepository.UpdatePositionByOrderDtoAndPositionDto(order, position);
        }
        
    }
}
