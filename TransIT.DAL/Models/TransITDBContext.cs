namespace TransIT.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using TransIT.DAL.Models.Entities.Abstractions;
    using TransIT.DAL.Models.DependencyInjection;

    public partial class TransITDBContext : IdentityDbContext<User>
    {
        private readonly IUser _user;

        public TransITDBContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="TransITDBContext"/> class.
        /// </summary>
        /// <param name="options">An options.</param>
        /// <param name="user">A current user, who is using a <see cref="DbContext"/> at the moment.</param>
        public TransITDBContext(DbContextOptions<TransITDBContext> options, IUser user)
            : base(options)
        {
            this._user = user;
        }

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

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_user != null)
            {
                IEnumerable<EntityEntry> unsavedItems = this.ChangeTracker.Entries()
                        .Where(entity => entity.Entity is IAuditableEntity &&
                                         (entity.State == EntityState.Added ||
                                          entity.State == EntityState.Modified));

                foreach (EntityEntry item in unsavedItems)
                {
                    IAuditableEntity entity = (IAuditableEntity)item.Entity;
                    DateTime now = DateTime.Now;
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedById = this._user.CurrentUserId;
                    }
                    entity.UpdatedById = this._user.CurrentUserId;
                    entity.UpdatedDate = now;
                } 
            }

            int records = await base.SaveChangesAsync(cancellationToken);
            return records;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
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
