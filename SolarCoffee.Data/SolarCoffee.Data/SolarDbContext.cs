using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Models;
using System;

namespace SolarCoffee.Data
{
    public class SolarDbContext: IdentityDbContext
    {
        public SolarDbContext()
        {

        }
        public SolarDbContext(DbContextOptions options):base(options)
        { }

        public virtual DbSet<CustomerData> Customers { get; set; }
        public virtual DbSet<CustomerAddressData> CustomersAddresses { get; set; }
        public virtual DbSet<ProductData> Products { get; set; }
        public virtual DbSet<ProductInventoryData> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshotData> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrderData> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItemData> SalesOrderItems { get; set; }

    }
}
