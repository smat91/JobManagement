using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOrderConnection : IBaseConnection<OrderDto>
    {
        OrderDto GetSingleById(int id);
        void AddNewPositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position);
        void DeletePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position);
        void UpdatePositionByOrderDtoAndPositionDto(OrderDto order, PositionDto position);
    }
}
