using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository
    {
        private static string ConnectionString { get; set; }

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public OrderDto GetOrderById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var order = context.Orders.Find(id);

                if (order != null)
                    return new OrderDto()
                    {
                        Id = order.Id,
                        Date = order.Date,
                        Customer = order.Customer,
                        Positions = order.Positions
                    };
                else
                    return null;
            }
        }

        public void AddNewOrder(OrderDto orderDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Add(orderDto);
                context.SaveChanges();
            }
        }

        public void DeleteOrderByDto(OrderDto orderDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Remove(orderDto);
                context.SaveChanges();
            }
        }

        public void UpdateOrderByDto(OrderDto orderDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Update(orderDto);
                context.SaveChanges();
            }
        }

        public void AddNewPositionByOrderDtoAndPositionDto(OrderDto orderDto, PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var order = context.Orders.Find(orderDto);

                if (order == null)
                    return;

                var position = context.Positions.Find(positionDto);

                if (position != null)
                {
                    PositionRepository positionRepository = new PositionRepository(ConnectionString);
                    positionRepository.AddNewPosition(positionDto);
                    position = context.Positions.Find(positionDto);
                }

                order.Positions.Add(position);

                context.SaveChanges();
            }
        }

        public void DeletePositionByOrderDtoAndPositionDto(OrderDto orderDto, PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var order = context.Orders.Find(orderDto);

                if (order == null)
                    return;

                order.Positions.Remove(positionDto);

                context.SaveChanges();
            }
        }

        public void UpdatePositionByOrderDtoAndPositionDto(OrderDto orderDto, PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var order = context.Orders.Find(orderDto);

                if (order == null)
                    return;

                var position = order.Positions
                    .Where(pos => pos.Id == positionDto.Id)
                    .FirstOrDefault();

                if (position != null)
                {
                    position.Item = positionDto.Item;
                    position.Amount = positionDto.Amount;
                    context.SaveChanges();
                }
                else
                    return;
            }
        }
    }
}
