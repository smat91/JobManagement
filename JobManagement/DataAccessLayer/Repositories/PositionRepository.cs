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
        private static string ConnectionString { get; set; }

        public PositionRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IPosition GetPositionById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var position = context.Positions.Find(id);
                return position;
            }
        }

        public void AddNewPosition(IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Add((Position)position);
                context.SaveChanges();
            }
        }

        public void DeletePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Remove((Position)position);
                context.SaveChanges();
            }
        }

        public void UpdatePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Update((Position)position);
                context.SaveChanges();
            }
        }
    }
}
