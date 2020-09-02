using Demo.DataAccess.Models.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Demo.DataAccess.Models.Mapping
{
    public class DemoContext: DbContext
    {
        static DemoContext()
        {
            Database.SetInitializer<DemoContext>(null);
        }

        public DemoContext()
            : base("Name=DemoContext")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductsMap());

        }
    }
}
