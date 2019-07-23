using Microsoft.EntityFrameworkCore;

namespace TransIT.DAL.Models
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public partial class TransITDBContext : IdentityDbContext<User>
    {
        public TransITDBContext() { }

        public TransITDBContext(DbContextOptions<TransITDBContext> options)
            : base(options) { }

        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<IssueLog> IssueLog { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Malfunction> Malfunction { get; set; }
        public virtual DbSet<MalfunctionGroup> MalfunctionGroup { get; set; }
        public virtual DbSet<MalfunctionSubgroup> MalfunctionSubgroup { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Transition> Transition { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            #region Configuration
            modelBuilder.ApplyConfiguration(new ActionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new IssueLogConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new MalfunctionConfiguration());
            modelBuilder.ApplyConfiguration(new MalfunctionGroupConfiguration());
            modelBuilder.ApplyConfiguration(new MalfunctionSubgroupConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new TransitionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
            #endregion
        }
    }
}
