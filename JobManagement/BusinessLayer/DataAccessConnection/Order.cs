using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
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
             var order = orderRepository_.GetSingleById(id);
             return new OrderDto(order);
        }

        public List<OrderDto> GetOrdersBySearchTerm(string searchTerm)
        {
            var ordersList = orderRepository_.GetBySearchTerm(searchTerm);
            return OrderDto.OrderListToOrderDtoList(ordersList);
        }

        public List<OrderDto> GetAllOrders()
        {
            var ordersList = orderRepository_.GetAll();
            return OrderDto.OrderListToOrderDtoList(ordersList);
        }

        public void AddNewOrder(OrderDto order)
        {
            orderRepository_.Add(OrderDto.OrderDtoToOrder(order));
        }

        public string DeleteOrderByDto(OrderDto order)
        {
            return orderRepository_.Delete(OrderDto.OrderDtoToOrder(order));
        }

        public void UpdateOrderByDto(OrderDto order)
        {
            orderRepository_.Update(OrderDto.OrderDtoToOrder(order));
        }

        public void AddNewPositionByIOrderAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.AddNewPositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));                
        }

        public void DeletePositionByIOrderAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.DeletePositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));
        }

        public void UpdatePositionByIOrderAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.UpdatePositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));
        }
    }
}
