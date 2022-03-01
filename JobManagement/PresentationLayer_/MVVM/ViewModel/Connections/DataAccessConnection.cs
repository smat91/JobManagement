using System.IO;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public sealed class DataAccessConnection
    {
        private static readonly object lock_ = new object ();  
        private static DataAccessConnection instance_ = null;

        public IConfiguration Configuration { get; private set; }

        private readonly AddressRepository addressRepository_;
        private readonly CustomerRepository customerRepository_;
        private readonly ItemGroupRepository itemGroupRepository_;
        private readonly OrderRepository orderRepository_;
        private readonly PositionRepository positionRepository_;
        
        DataAccessConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            addressRepository_ = new AddressRepository(Configuration.GetConnectionString("JobManagement"));
            customerRepository_ = new CustomerRepository(Configuration.GetConnectionString("JobManagement"));
            itemGroupRepository_ = new ItemGroupRepository(Configuration.GetConnectionString("JobManagement"));
            orderRepository_ = new OrderRepository(Configuration.GetConnectionString("JobManagement"));
            positionRepository_ = new PositionRepository(Configuration.GetConnectionString("JobManagement"));
        }

        public static DataAccessConnection Instance
        {
            get
            {
                lock (lock_)
                {
                    if (instance_ == null)
                    {
                        instance_ = new DataAccessConnection();
                    }
                    return instance_;
                }
            }
        }


    }
}
