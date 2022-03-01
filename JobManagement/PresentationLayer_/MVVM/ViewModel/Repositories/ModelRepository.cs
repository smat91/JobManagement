using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PresentationLayer.MVVM.Model
{
    public sealed class ModelRepository
    {
        private static readonly object lock_ = new object ();  
        private static ModelRepository instance_ = null;

        public IConfiguration Configuration { get; private set; }

        private readonly AddressRepository addressRepository_;
        private readonly CustomerRepository customerRepository_;
        private readonly ItemGroupRepository itemGroupRepository_;
        private readonly ItemRepository itemRepository_;
        private readonly OrderRepository orderRepository_;
        private readonly PositionRepository positionRepository_;
        
        ModelRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            addressRepository_ = new AddressRepository(Configuration.GetConnectionString("JobManagement"));
            customerRepository_ = new CustomerRepository(Configuration.GetConnectionString("JobManagement"));
            itemGroupRepository_ = new ItemGroupRepository(Configuration.GetConnectionString("JobManagement"));
            itemRepository_ = new ItemRepository(Configuration.GetConnectionString("JobManagement"));
            orderRepository_ = new OrderRepository(Configuration.GetConnectionString("JobManagement"));
            positionRepository_ = new PositionRepository(Configuration.GetConnectionString("JobManagement"));
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
