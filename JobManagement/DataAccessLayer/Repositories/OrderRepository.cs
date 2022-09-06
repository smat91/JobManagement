using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Helper;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public override string TableName => "Order";

        public Order GetSingleById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var order = context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .Single(order => order.Id == id);

                return order;
            }
        }

        public new List<Order> GetBySearchTerm(string searchTerm)
        {
            List<Order> orderList = new List<Order>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .AsEnumerable()
                    .Where(order => search.EvaluateSearchTerm(searchTerm, order))
                    .ToList()
                    .ForEach(order => orderList.Add(order));
            }

            return orderList;
        }

        public new List<Order> GetAll()
        {
            using (var context = new JobManagementContext())
            {
                List<Order> ordersList = new List<Order>();

                context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .ToList()
                    .ForEach(order => ordersList.Add(order));

                return ordersList;
            }
        }

        public new void Add(Order order)
        {
            using (var context = new JobManagementContext())
            {
                if (order.Customer != null)
                {
                    var customer = context.Customers
                        .Include(customer => customer.Address)
                        .FirstOrDefault(customer => customer.Id == order.Customer.Id);
                    if (customer != default(Customer))
                        order.Customer = customer;
                }

                if (!order.Positions.IsNullOrEmpty())
                {
                    ICollection<Position> tempList = new List<Position>();

                    foreach (Position pos in order.Positions)
                    {
                        if (pos != null)
                        {
                            var position = context.Positions
                                .Include(positions => positions.Item)
                                .ThenInclude(item => item.Group)
                                .ThenInclude(group => group.ParentItemGroup)
                                .FirstOrDefault(position => position.Id == pos.Id);

                            if (position != default(Position))
                            {
                                tempList.Add(position);
                            }
                            else
                            {
                                tempList.Add(new Position()
                                {
                                    Item = context.Items
                                        .Include(item => item.Group)
                                        .ThenInclude(group => group.ParentItemGroup)
                                        .Single(item => item.Id == pos.Item.Id),
                                    Amount = pos.Amount
                                });
                            }
                        }
                    }

                    order.Positions = tempList;
                }

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public new string Delete(Order order)
        {
            order.Positions.Clear();
            Update(order);

            using (var context = new JobManagementContext())
            {

                context.Orders.Remove(GetSingleById(order.Id));

                try
                {
                    context.SaveChanges();
                    return "Datensatz erfolgreich gelöscht";
                }
                catch (DbUpdateException e)
                {
                    return "Datensatz konnte nicht gelöscht werden.\nBitte zuerst Datensätze erntfernen in denen der Datensatz verwendet wird.";
                }
            }
        }

        public new void Update(Order order)
        {
            using (var context = new JobManagementContext())
            {
                if (order.Customer != null)
                {
                    var customer = context.Customers
                        .Include(customer => customer.Address)
                        .FirstOrDefault(customer => customer.CustomerNumber == order.Customer.CustomerNumber);
                    if (customer != default(Customer))
                        order.Customer = customer;
                }

                ICollection<Position> tempList = new List<Position>();

                foreach (Position pos in order.Positions)
                {
                    if (pos != null)
                    {
                        var position = context.Positions
                            .Include(positions => positions.Item)
                            .ThenInclude(item => item.Group)
                            .ThenInclude(group => group.ParentItemGroup)
                            .FirstOrDefault(position => position.Id == pos.Id);

                        if (position != default(Position))
                        {
                            tempList.Add(position);
                        }
                        else
                        {
                            tempList.Add(new Position()
                            {
                                Item = context.Items
                                    .Include(item => item.Group)
                                    .ThenInclude(group => group.ParentItemGroup)
                                    .Single(item => item.Id == pos.Item.Id),
                                Amount = pos.Amount
                            });
                        }
                    }
                }

                order.Positions = tempList;

                context.Orders.Update(order);
                context.SaveChanges();
            }
        }

        public void AddNewPositionByOrderAndPosition(Order order, Position position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .FirstOrDefault(o => o.Id == order.Id);

                if (orderTemp == default(Order))
                    return;

                var positionTemp = context.Positions
                    .Include(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .FirstOrDefault(p => p.Id == position.Id);

                if (positionTemp == default(Position))
                {
                    positionTemp = new Position()
                    {
                        Item = context.Items
                            .Include(item => item.Group)
                            .ThenInclude(group => group.ParentItemGroup)
                            .Single(item => item.Id == position.Item.Id),
                        Amount = position.Amount
                    }; 
                }

                orderTemp.Positions.Add(positionTemp);

                context.SaveChanges();
            }
        }

        public void DeletePositionByOrderAndPosition(Order order, Position position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .FirstOrDefault(o => o.Id == order.Id);

                if (orderTemp == default(Order))
                    return;

                orderTemp.Positions.Remove(position);

                context.SaveChanges();
            }
        }

        public void UpdatePositionByOrderAndPosition(Order order, Position position)
        {
            using (var context = new JobManagementContext())
            {
                var orderTemp = context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .FirstOrDefault(o => o.Id == order.Id);

                if (orderTemp == default(Order))
                    return;

                var positionTemp = orderTemp.Positions
                    .FirstOrDefault(pos => pos.Id == position.Id);

                if (positionTemp == default(Position))
                    return;

                orderTemp.Positions.Remove(positionTemp);
                orderTemp.Positions.Add(position);
                context.SaveChanges();
            }
        }
    }
}
