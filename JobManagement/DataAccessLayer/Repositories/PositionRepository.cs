using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class PositionRepository
    {
        public Position GetPositionById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var position = context.Positions.Find(id);
                context.Entry(position).Reference(p => p.Item).Load();
                return position;
            }
        }

        public List<Position> GetPositionsBySearchTerm(string searchTerm)
        {
            List<Position> positionList = new List<Position>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Positions
                    .Include(p => p.Item)
                    .AsEnumerable()
                    .Where(position => search.EvaluateSearchTerm(searchTerm, position))
                    .ToList()
                    .ForEach(position => positionList.Add(position));
            }

            return positionList;
        }

        public List<Position> GetAllPositions()
        {
            using (var context = new JobManagementContext())
            {
                List<Position> positionsList = new List<Position>();

                context.Positions
                    .Include(p => p.Item)
                    .ToList()
                    .ForEach(position => positionsList.Add(position));

                return positionsList;
            }
        }

        public void AddNewPosition(Position position)
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

        public void DeletePositionByDto(Position position)
        {
            using (var context = new JobManagementContext())
            {
                context.Positions.Remove((Position)position);
                context.SaveChanges();
            }
        }

        public void UpdatePositionByDto(Position position)
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
