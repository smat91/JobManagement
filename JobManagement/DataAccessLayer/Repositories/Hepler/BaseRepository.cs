using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.HeplerRepositories
{
    public abstract class BaseRepository<M> : IBaseRepository<M> where M : class
    {
        public abstract string TableName { get; }

        public M GetSingleById<P>(P pkValue)
        {
            using (var context = new JobManagementContext())
            {
                var entity = context.Find<M>(pkValue);
                return entity;
            }
        }

        public List<M> GetBySearchTerm(string searchTerm)
        {
            List<M> entityList = new List<M>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Set<M>()
                    .AsEnumerable()
                    .Where(entity => search.EvaluateSearchTerm(searchTerm, entity))
                    .ToList()
                    .ForEach(entity => entityList.Add(entity));
            }

            return entityList;
        }

        public List<M> GetAll()
        {
            List<M> entityList = new List<M>();

            using (var context = new JobManagementContext())
            {
                context.Set<M>()
                    .ToList()
                    .ForEach(entity => entityList.Add(entity));
            }

            return entityList;
        }


        public void Add(M entity)
        {
            using (var context = new JobManagementContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public string Delete(M entity)
        {
            using (var context = new JobManagementContext())
            {
                context.Remove(entity);

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

        public void Update(M entity)
        {
            using (var context = new JobManagementContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
