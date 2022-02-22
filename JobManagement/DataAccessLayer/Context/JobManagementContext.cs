using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Protocols;

namespace DataAccessLayer.Context
{
    public class JobManagementContext : DbContext
    {
        private static string ConnectionString { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<ItemGroupHierarchyRequest> ItemGroupHierarchyRequest { get; set; }
        public DbSet<InvoiceRequest> InvoiceRequest { get; set; }
        public DbSet<OrderNumbersRequest> OrderNumbersRequest { get; set; }

        public JobManagementContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);

            optionsBuilder
                .UseLazyLoadingProxies();

            optionsBuilder
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // temporal data tables
            modelBuilder
                .Entity<Address>()
                .ToTable("Addresses", b => b.IsTemporal());
            modelBuilder
                .Entity<Customer>()
                .ToTable("Customers", b => b.IsTemporal());
            modelBuilder
                .Entity<Order>()
                .ToTable("Orders", b => b.IsTemporal());
            modelBuilder
                .Entity<Item>()
                .ToTable("Items", b => b.IsTemporal());

            // query types
            modelBuilder
                .Entity<ItemGroupHierarchyRequest>()
                .HasNoKey(); ;
            modelBuilder
                .Entity<InvoiceRequest>()
                .HasNoKey(); ;
            modelBuilder
                .Entity<OrderNumbersRequest>()
                .HasNoKey(); ;
        }
    }
}
