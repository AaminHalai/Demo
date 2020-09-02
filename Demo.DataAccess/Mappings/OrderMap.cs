using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Demo.DataAccess.Models.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.id);



            // Table & Column Mappings
            this.ToTable("order");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.orderid).HasColumnName("orderid");
            this.Property(t => t.paymenttype).HasColumnName("paymenttype");
            this.Property(t => t.salepintype).HasColumnName("salepintype");
            this.Property(t => t.orderdate).HasColumnName("orderdate");
            this.Property(t => t.price).HasColumnName("price");
            this.Property(t => t.quantity).HasColumnName("quantity");
            this.Property(t => t.customerid).HasColumnName("customerid");
            this.Property(t => t.productid).HasColumnName("productid");
        }
    }
}
