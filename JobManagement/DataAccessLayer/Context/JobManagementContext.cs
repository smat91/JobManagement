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

namespace DataAccessLayer.Context
{
    public class JobManagementContext : DbContext
    {
        private IConfiguration Configuration { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ItemGroupHierarchyRequest> ItemGroupHierarchyRequest { get; set; }
        public DbSet<InvoiceRequest> InvoiceRequest { get; set; }
        public DbSet<OrderNumbersRequest> OrderNumbersRequest { get; set; }
        public DbSet<ItemNumbersRequest> ItemNumbersRequest { get; set; }
        public DbSet<AverageItemNumbersPerOrderRequest> AverageItemNumbersPerOrderRequest { get; set; }
        public DbSet<TotalSalesRequest> TotalSalesRequest { get; set; }
        public DbSet<TotalCustomersSalesRequest> TotalCustomersSalesRequest { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            optionsBuilder
                .UseSqlServer(connection.GetConnectionString("JobManagement"));

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
                .HasNoKey();
            modelBuilder
                .Entity<InvoiceRequest>()
                .HasNoKey(); 
            modelBuilder
                .Entity<OrderNumbersRequest>()
                .HasNoKey();
            modelBuilder
                .Entity<ItemNumbersRequest>()
                .HasNoKey();
            modelBuilder
                .Entity<AverageItemNumbersPerOrderRequest>()
                .HasNoKey();
            modelBuilder
                .Entity<TotalSalesRequest>()
                .HasNoKey();
            modelBuilder
                .Entity<TotalCustomersSalesRequest>()
                .HasNoKey();
        }
    }
}
