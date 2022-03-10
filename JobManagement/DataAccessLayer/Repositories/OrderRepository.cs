using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository
    {
        public Order GetOrderById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var order = context.Orders.Find(id);
                context.Entry(order).Reference(o => o.Customer).Load();
                context.Entry(order).Reference(o => o.Positions).Load();

                return order;
            }
        }

        public List<Order> GetOrdersBySearchTerm(string searchTerm)
        {
            List<Order> orderList = new List<Order>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Positions)
                    .AsEnumerable()
                    .Where(order => search.EvaluateSearchTerm(searchTerm, order))
                    .ToList()
                    .ForEach(order => orderList.Add(order));
            }

            return orderList;
        }

        public List<Order> GetAllOrders()
        {
            using (var context = new JobManagementContext())
            {
                List<Order> ordersList = new List<Order>();

                context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Positions)
                    .ToList()
                    .ForEach(order => ordersList.Add(order));

                return ordersList;
            }
        }

        public void AddNewOrder(Order order)
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

                if (!order.Positions.IsNullOrEmpty())
                {
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
                }

                context.Orders.Add((Order)order);
                context.SaveChanges();
            }
        }

        public void DeleteOrderByDto(Order order)
        {
            using (var context = new JobManagementContext())
            {
                context.Orders.Remove((Order)order);
                context.SaveChanges();
            }
        }

        public void UpdateOrderByDto(Order order)
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

        public void AddNewPositionByOrderAndPosition(Order order, IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Positions)
                    .FirstOrDefault(o => o.Id == order.Id);

                if (orderTemp == default(Order))
                    return;

                var positionTemp = context.Positions
                    .Include(p => p.Item)
                    .FirstOrDefault(p => p.Id == position.Id);

                if (positionTemp == default(Position))
                {
                    positionTemp = (Position)position;
                }

                orderTemp.Positions.Add(positionTemp);

                context.SaveChanges();
            }
        }

        public void DeletePositionByOrderAndPosition(Order order, IPosition position)
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

        public void UpdatePositionByOrderAndPosition(Order order, IPosition position)
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
