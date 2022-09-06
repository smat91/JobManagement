using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order GetSingleById(int id);
        void AddNewPositionByOrderAndPosition(Order order, Position position);
        void DeletePositionByOrderAndPosition(Order order, Position position);
        void UpdatePositionByOrderAndPosition(Order order, Position position);
    }
}
