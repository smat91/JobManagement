using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Helper
{
    public interface IBaseConnection<M> where M : class
    {
        List<M> GetAll();
        List<M> GetBySearchTerm(string searchTerm);
        void Add(M entity);
        string Delete(M entity);
        void Update(M entity);
    }
}
