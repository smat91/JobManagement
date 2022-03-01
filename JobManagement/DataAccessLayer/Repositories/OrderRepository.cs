using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository
    {
        private static string ConnectionString { get; set; }

        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IOrder GetOrderById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var order = context.Orders.Find(id);
                return order;
            }
        }

        public void AddNewOrder(IOrder order)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Add((Order)order);
                context.SaveChanges();
            }
        }

        public void DeleteOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Remove((Order)order);
                context.SaveChanges();
            }
        }

        public void UpdateOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Orders.Update((Order)order);
                context.SaveChanges();
            }
        }

        public void AddNewPositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var orderTemp = context.Orders.Find(order);

                if (orderTemp == null)
                    return;

                var positionTemp = context.Positions.Find(position);

                if (positionTemp != null)
                {
                    PositionRepository positionRepository = new PositionRepository(ConnectionString);
                    positionRepository.AddNewPosition((Position)position);
                    positionTemp = context.Positions.Find(position);
                }

                orderTemp.Positions.Add(positionTemp);

                context.SaveChanges();
            }
        }

        public void DeletePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var orderTemp = context.Orders.Find(order);

                if (orderTemp == null)
                    return;

                orderTemp.Positions.Remove((Position)position);

                context.SaveChanges();
            }
        }

        public void UpdatePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var orderTemp = context.Orders.Find(order);

                if (orderTemp == null)
                    return;

                var positionTemp = orderTemp.Positions
                    .Where(pos => pos.Id == position.Id)
                    .FirstOrDefault();

                if (positionTemp != null)
                {
                    positionTemp = (Position)position;
                    context.SaveChanges();
                }
                else
                    return;
            }
        }
    }
}
