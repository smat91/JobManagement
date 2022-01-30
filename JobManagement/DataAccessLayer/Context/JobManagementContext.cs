using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Protocols;

namespace DataAccessLayer.Context
{
    public class JobManagementContext : DbContext
    {
        public DbSet<AddressDto> Addresses { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
        public DbSet<ItemDto> Items { get; set; }
        public DbSet<ItemGroupDto> ItemGroups { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<PositionDto> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("JobManagement"));

            optionsBuilder.UseLazyLoadingProxies();

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ;
        }
    }
}
