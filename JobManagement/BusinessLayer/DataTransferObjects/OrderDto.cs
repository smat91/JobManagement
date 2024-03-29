﻿using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<PositionDto> Positions { get; set; }

        public OrderDto()
        {
        }

        public OrderDto(DataAccessLayer.Models.Order order)
        {
            Id = order.Id;
            Date = order.Date;
            Customer = new CustomerDto(order.Customer);
            Positions = PositionDto.PositionCollectionToPositionDtoCollection(order.Positions);
        }

        public static DataAccessLayer.Models.Order OrderDtoToOrder(OrderDto order)
        {
            return new DataAccessLayer.Models.Order
            {
                Id = order.Id,
                Date = order.Date,
                Customer = CustomerDto.CustomerDtoToCustomer(order.Customer),
                Positions = PositionDto.PositionDtoCollectionToPositionCollection(order.Positions)
            };
        }

        public static List<OrderDto> OrderListToOrderDtoList(List<DataAccessLayer.Models.Order> Orders)
        {
            List<OrderDto> OrderDtos = new List<OrderDto>();
            foreach (var Order in Orders)
            {
                OrderDtos.Add(new OrderDto(Order));
            }

            return OrderDtos;
        }
    }
}
