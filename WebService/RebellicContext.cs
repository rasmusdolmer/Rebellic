namespace WebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RebellicContext : DbContext
    {
        public RebellicContext()
            : base("name=RebellicContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeProduct> EmployeeProduct { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.Product_Category)
                .WillCascadeOnDelete(false);
        }
    }
}
