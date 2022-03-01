using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PresentationLayer.MVVM.Model
{
    public sealed class ModelRepository
    {
        private static readonly object lock_ = new object ();  
        private static ModelRepository instance_ = null;

        public IConfiguration Configuration { get; private set; }

        ModelRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static ModelRepository Instance
        {
            get
            {
                lock (lock_)
                {
                    if (instance_ == null)
                    {
                        instance_ = new ModelRepository();
                    }
                    return instance_;
                }
            }
        }
    }
}
