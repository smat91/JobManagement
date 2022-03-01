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
        private static string connectionString_;

        public OrderRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public IOrder GetOrderById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var order = context.Orders.Find(id);
                return order;
            }
        }

        public void AddNewOrder(IOrder order)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Orders.Add((Order)order);
                context.SaveChanges();
            }
        }

        public void DeleteOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Orders.Remove((Order)order);
                context.SaveChanges();
            }
        }

        public void UpdateOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Orders.Update((Order)order);
                context.SaveChanges();
            }
        }

        public void AddNewPositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var orderTemp = context.Orders.Find(order);

                if (orderTemp == null)
                    return;

                var positionTemp = context.Positions.Find(position);

                if (positionTemp != null)
                {
                    PositionRepository positionRepository = new PositionRepository(connectionString_);
                    positionRepository.AddNewPosition((Position)position);
                    positionTemp = context.Positions.Find(position);
                }

                orderTemp.Positions.Add(positionTemp);

                context.SaveChanges();
            }
        }

        public void DeletePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext(connectionString_))
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
            using (var context = new JobManagementContext(connectionString_))
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
