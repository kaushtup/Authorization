using CrossoverSpa.Entities;

using Microsoft.EntityFrameworkCore;


namespace CrossoverSpa.Database
{
    public class SpaDbContext : DbContext
    {


        public SpaDbContext(DbContextOptions<SpaDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(p => new { p.Id});
            base.OnModelCreating(builder);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EmployeeContact> EmployeeContacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }
    }
}
