using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;

namespace DataAccessLayer.Repositories
{
    public class PositionRepository
    {
        private static string ConnectionString { get; set; }

        public PositionRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public PositionDto GetPositionById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var position = context.Positions.Find(id);

                if (position != null)
                    return new PositionDto()
                    {
                        Id = position.Id,
                        Item = position.Item,
                        Amount = position.Amount
                    };
                else
                {
                    return null;
                }
            }
        }

        public void AddNewPosition(PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Add(positionDto);
                context.SaveChanges();
            }
        }

        public void DeletePositionByDto(PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Remove(positionDto);
                context.SaveChanges();
            }
        }

        public void UpdatePositionByDto(PositionDto positionDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Positions.Update(positionDto);
                context.SaveChanges();
            }
        }
    }
}
