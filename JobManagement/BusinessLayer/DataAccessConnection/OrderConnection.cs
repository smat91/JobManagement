using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataAccessConnection
{
    public class OrderConnection : IOrderConnection
    {
        private readonly IOrderRepository orderRepository_;

        public OrderConnection(IOrderRepository orderRepository)
        {
            orderRepository_ = orderRepository;
        }
        
        public OrderDto GetSingleById(int id)
        {
             var order = orderRepository_.GetSingleById(id);
             return new OrderDto(order);
        }

        public List<OrderDto> GetBySearchTerm(string searchTerm)
        {
            var ordersList = orderRepository_.GetBySearchTerm(searchTerm);
            return OrderDto.OrderListToOrderDtoList(ordersList);
        }

        public List<OrderDto> GetAll()
        {
            var ordersList = orderRepository_.GetAll();
            return OrderDto.OrderListToOrderDtoList(ordersList);
        }

        public void Add(OrderDto order)
        {
            orderRepository_.Add(OrderDto.OrderDtoToOrder(order));
        }

        public string Delete(OrderDto order)
        {
            return orderRepository_.Delete(OrderDto.OrderDtoToOrder(order));
        }

        public void Update(OrderDto order)
        {
            orderRepository_.Update(OrderDto.OrderDtoToOrder(order));
        }

        public void AddNewPositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.AddNewPositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));                
        }

        public void DeletePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.DeletePositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));
        }

        public void UpdatePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position)
        {
            orderRepository_.UpdatePositionByOrderAndPosition(
                OrderDto.OrderDtoToOrder(order),
                PositionDto.PositionDtoToPosition(position));
        }
    }
}
