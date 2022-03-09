using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helper
{
    public class Search
    {
        public bool EvaluateSearchTerm(string searchTerm, object obj)
        {
            bool result = true;

            string[] searchTerms = searchTerm.Split(' ');

            foreach (string term in searchTerms)
            {
                result &= EvaluateSingleTerm(term, obj);
            }
            
            return result;
        }

        private bool EvaluateSingleTerm(string term, object obj)
        {
            bool result = false;

            foreach (var property in obj.GetType().GetProperties())
            {
                result |= property.GetValue(obj).ToString().Contains(term, StringComparison.OrdinalIgnoreCase);
            }

            return result;
        }
    }
}
