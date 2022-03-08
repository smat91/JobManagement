using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class PositionRepository
    {
        public static IPosition GetPositionById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var position = context.Positions.Find(id);
                context.Entry(position).Reference(p => p.Item).Load();
                return position;
            }
        }

        public static List<IPosition> GetAllPositions()
        {
            using (var context = new JobManagementContext())
            {
                List<IPosition> positionsList = new List<IPosition>();

                context.Positions
                    .Include(p => p.Item)
                    .ToList()
                    .ForEach(position => positionsList.Add(position));

                return positionsList;
            }
        }

        public static void AddNewPosition(IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                if (position.Item != null)
                {
                    var item = context.Items
                        .Find(position.Item.Id);
                    if (item != null)
                        position.Item = item;
                }

                context.Positions.Add((Position)position);
                context.SaveChanges();
            }
        }

        public static void DeletePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                context.Positions.Remove((Position)position);
                context.SaveChanges();
            }
        }

        public static void UpdatePositionByDto(IPosition position)
        {
            using (var context = new JobManagementContext())
            {
                if (position.Item != null)
                {
                    var item = context.Items
                        .Find(position.Item.Id);
                    if (item != null)
                        position.Item = item;
                }

                context.Positions.Update((Position)position);
                context.SaveChanges();
            }
        }
    }
}
