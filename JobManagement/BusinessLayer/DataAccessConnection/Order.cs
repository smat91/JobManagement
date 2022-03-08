using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Order
    {
        private readonly OrderRepository orderRepository_;

        public Order()
        {
            orderRepository_ = new OrderRepository();
        }
        
        public OrderDto GetOrderById(int id)
        {
             var order = orderRepository_.GetOrderById(id);
             return (OrderDto)order;
        }

        public List<OrderDto> GetAllOrders()
        {
            var ordersList = orderRepository_.GetAllOrders();
            return ordersList.ConvertAll(
                new Converter<IOrder, OrderDto>(IOrderToOrderDto));
        }

        public void AddNewOrder(OrderDto order)
        {
            orderRepository_.AddNewOrder(order);
        }

        public void DeleteOrderByDto(OrderDto order)
        {
            orderRepository_.DeleteOrderByDto(order);
        }

        public void UpdateOrderByDto(OrderDto order)
        {
            orderRepository_.UpdateOrderByDto(order);
        }

        public void AddNewPositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.AddNewPositionByOrderDtoAndPositionDto(order, position);                
        }

        public void DeletePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.DeletePositionByOrderDtoAndPositionDto(order, position);
        }

        public void UpdatePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.UpdatePositionByOrderDtoAndPositionDto(order, position);
        }

        private static OrderDto IOrderToOrderDto(IOrder address)
        {
            return (OrderDto)address;
        }
    }
}
