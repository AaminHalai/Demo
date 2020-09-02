using System.Data.Entity.ModelConfiguration;

namespace Demo.DataAccess.Models.Mappings
{
    public  class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);



            // Table & Column Mappings
            this.ToTable("customer");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.address).HasColumnName("address");

        }
    }
}
