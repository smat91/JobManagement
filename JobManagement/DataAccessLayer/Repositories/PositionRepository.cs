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
        public static IPosition GetPositionById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var position = context.Positions.Find(id);
                return position;
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
