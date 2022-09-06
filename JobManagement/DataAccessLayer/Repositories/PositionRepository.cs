using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.HeplerRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class PositionRepository : BaseRepository<Position>
    {
        public override string TableName => "Position";

        public Position GetSingleById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var position = context.Positions
                    .Include(position => position.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .Single(position => position.Id == id);
                return position;
            }
        }

        public new List<Position> GetBySearchTerm(string searchTerm)
        {
            List<Position> positionList = new List<Position>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Positions
                    .Include(position => position.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .AsEnumerable()
                    .Where(position => search.EvaluateSearchTerm(searchTerm, position))
                    .ToList()
                    .ForEach(position => positionList.Add(position));
            }

            return positionList;
        }

        public new List<Position> GetAll()
        {
            using (var context = new JobManagementContext())
            {
                List<Position> positionsList = new List<Position>();

                context.Positions
                    .Include(position => position.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .ToList()
                    .ForEach(position => positionsList.Add(position));

                return positionsList;
            }
        }

        public new void Add(Position position)
        {
            using (var context = new JobManagementContext())
            {
                if (position.Item != null)
                {
                    var item = context.Items
                        .Include(item => item.Group)
                        .ThenInclude(group => group.ParentItemGroup)
                        .FirstOrDefault(item => item.Id == position.Item.Id);
                    if (item != default(Item))
                        position.Item = item;
                }

                context.Positions.Add(position);
                context.SaveChanges();
            }
        }

        public new void Update(Position position)
        {
            using (var context = new JobManagementContext())
            {
                if (position.Item != null)
                {
                    var item = context.Items
                        .Include(item => item.Group)
                        .ThenInclude(group => group.ParentItemGroup)
                        .FirstOrDefault(item => item.Id == position.Item.Id);
                    if (item != default(Item))
                        position.Item = item;
                }

                context.Positions.Update(position);
                context.SaveChanges();
            }
        }
    }
}
