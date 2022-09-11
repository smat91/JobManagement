using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DataAccessLayer.Models;

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
                var propertyType = property.PropertyType.FullName;
                var addressType = typeof(Address).FullName;
                var customerType = typeof(Customer).FullName;
                if ((propertyType == addressType) || (propertyType == customerType))
                {
                    var test = property.GetValue(obj).GetType().GetProperties();
                    foreach (var subProperty in test)
                    {
                        var value = subProperty.GetValue(property.GetValue(obj));
                        if (value != null) {
                            result |= value.ToString().Contains(term, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }
                else
                {
                    result |= property.GetValue(obj).ToString().Contains(term, StringComparison.OrdinalIgnoreCase);
                }
            }

            return result;
        }
    }
}
