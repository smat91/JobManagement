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
        public static IOrder GetOrderById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var order = context.Orders.Find(id);
                return order;
            }
        }

        public static void AddNewOrder(IOrder order)
        {
            using (var context = new JobManagementContext())
            {
                if (order.Customer != null)
                {
                    var customer = context.Customers
                        .Find(order.Customer.Id);
                    if (customer != null)
                        order.Customer = customer;
                }

                foreach (Position pos in order.Positions)
                {
                    if (pos != null)
                    {
                        var position = context.Positions
                            .Find(pos.Id);
                        if (position != null)
                        {
                            order.Positions.Remove(pos);
                            order.Positions.Add(position);
                        }
                    }
                }

                context.Orders.Add((Order)order);
                context.SaveChanges();
            }
        }

        public static void DeleteOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext())
            {
                context.Orders.Remove((Order)order);
                context.SaveChanges();
            }
        }

        public static void UpdateOrderByDto(IOrder order)
        {
            using (var context = new JobManagementContext())
            {
                if (order.Customer != null)
                {
                    var customer = context.Customers
                        .Find(order.Customer.Id);
                    if (customer != null)
                        order.Customer = customer;
                }

                foreach (Position pos in order.Positions)
                {
                    if (pos != null)
                    {
                        var position = context.Positions
                            .Find(pos.Id);
                        if (position != null)
                        {
                            order.Positions.Remove(pos);
                            order.Positions.Add(position);
                        }
                    }
                }

                context.Orders.Update((Order)order);
                context.SaveChanges();
            }
        }

        public static void AddNewPositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders.Find(order.Id);

                if (orderTemp == null)
                    return;

                var positionTemp = context.Positions.Find(position.Id);

                if (positionTemp != null)
                {
                    positionTemp = (Position)position;
                }

                orderTemp.Positions.Add(positionTemp);

                context.SaveChanges();
            }
        }

        public static void DeletePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders.Find(order.Id);

                if (orderTemp == null)
                    return;

                orderTemp.Positions.Remove((Position)position);

                context.SaveChanges();
            }
        }

        public static void UpdatePositionByOrderDtoAndPositionDto(IOrder order, IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders.Find(order.Id);

                if (orderTemp == null)
                    return;

                var positionTemp = orderTemp.Positions
                    .FirstOrDefault(pos => pos.Id == position.Id);

                if (positionTemp == null)
                    return;

                orderTemp.Positions.Remove(positionTemp);
                orderTemp.Positions.Add((Position)position);
                context.SaveChanges();
            }
        }
    }
}
