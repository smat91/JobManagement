using System.IO;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public sealed class DataAccessConnection
    {
        private static readonly object lock_ = new object ();  
        private static DataAccessConnection? instance_ = null;

        public IConfiguration Configuration { get; private set; }

        //public readonly AddressConnection AddressConnection;
        //public readonly CustomerConnection CustomerConnection;
        public readonly ItemConnection ItemConnection;
        //public readonly ItemGroupConnection ItemGroupConnection;
        //public readonly OrderConnection OrderConnection;
        //public readonly PositionConnection PositionConnection;

        DataAccessConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            //AddressConnection = new AddressConnection(Configuration.GetConnectionString("JobManagement"));
            //CustomerConnection = new CustomerConnection(Configuration.GetConnectionString("JobManagement"));
            //ItemGroupConnection = new ItemGroupConnection(Configuration.GetConnectionString("JobManagement"));
            ItemConnection = new ItemConnection(Configuration.GetConnectionString("JobManagement"));
            //OrderConnection = new OrderConnection(Configuration.GetConnectionString("JobManagement"));
            //PositionConnection = new PositionConnection(Configuration.GetConnectionString("JobManagement"));
        }

        public static DataAccessConnection Instance
        {
            get
            {
                lock (lock_)
                {
                    return instance_ ?? new DataAccessConnection();
                }
            }
        }


    }
}
