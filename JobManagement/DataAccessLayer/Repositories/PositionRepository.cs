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
    public class PositionRepository
    {
        private static string connectionString_;

        public PositionRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public IPosition GetPositionById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var position = context.Positions.Find(id);
                return position;
            }
        }

        public void AddNewPosition(IPosition position)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Positions.Add((Position)position);
                context.SaveChanges();
            }
        }

        public void DeletePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Positions.Remove((Position)position);
                context.SaveChanges();
            }
        }

        public void UpdatePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Positions.Update((Position)position);
                context.SaveChanges();
            }
        }
    }
}
