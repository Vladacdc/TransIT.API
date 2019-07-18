using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Extensions;

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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TransITDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            #region Seeding

            modelBuilder.SeedStates();

            #endregion

            #region Configuration
            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ACTION_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ACTION_T__D9C1FA00D8EDC403")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.ActionTypeCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_ACTION_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.ActionTypeMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_ACTION_TYPE_USER");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Sum)
                    .HasColumnName("SUM")
                    .HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.BillCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_BILL_USER");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.DocumentId)
                    .HasConstraintName("FK_BILL_DOCUMENT");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILL_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.BillMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_BILL_USER");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("COUNTRY");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__COUNTRY__D9C1FA008FF4E681")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.CountryCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_COUNTRY_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.CountryMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_COUNTRY_USER");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("CURRENCY");

                entity.HasIndex(e => e.ShortName)
                    .HasName("UQ__CURRENCY__F4E7E33EEBE730B7")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("FULL_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnName("SHORT_NAME")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.CurrencyCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_CURRENCY_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.CurrencyMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_CURRENCY_USER");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("DOCUMENT");

                entity.HasIndex(e => e.Path)
                    .HasName("UQ_PATH_DOCUMENT")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.IssueLogId).HasColumnName("ISSUE_LOG_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.NewDate)
                    .HasColumnName("NEW_DATE")
                    .HasColumnType("datetime");


                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("PATH")
                    .HasMaxLength(500)
                    .IsUnicode(true)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.DocumentCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_DOCUMENT_USER");

                entity.HasOne(d => d.IssueLog)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.IssueLogId)
                    .HasConstraintName("FK_DOCUMENT_ISSUE_LOG");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.DocumentMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_DOCUMENT_USER");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.HasIndex(e => e.BoardNumber)
                    .HasName("UQ_EMPLOYEE_BOARD_NUMBER_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ShortName)
                    .HasName("UQ_EMPLOYEE_SHORT_NAME_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoardNumber).HasColumnName("BOARD_NUMBER");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.PostId).HasColumnName("POST_ID");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnName("SHORT_NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.EmployeeCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_MOD_EMPLOYEE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.EmployeeMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_EMPLOYEE_ROLE");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_POST");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("ISSUE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignedToId).HasColumnName("ASSIGNED_TO");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deadline)
                    .HasColumnName("DEADLINE")
                    .HasColumnType("datetime");

                entity.Property(e => e.MalfunctionId).HasColumnName("MALFUNCTION_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Number).HasColumnName("NUMBER");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.StateId)
                    .HasColumnName("STATE_ID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Summary).HasColumnName("SUMMARY");

                entity.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");

                entity.Property(e => e.Warranty).HasColumnName("WARRANTY");

                entity.HasOne(d => d.AssignedTo)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.AssignedToId);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_ISSUE_USER");

                entity.HasOne(d => d.Malfunction)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.MalfunctionId)
                    .HasConstraintName("FK_ISSUE_MALFUNCTION");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_ISSUE_USER");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_STATE");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Issue)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_VEHICLE");
            });

            modelBuilder.Entity<IssueLog>(entity =>
            {
                entity.ToTable("ISSUE_LOG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasColumnName("ACTION_TYPE_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.Expenses)
                    .HasColumnName("EXPENSES")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.NewStateId).HasColumnName("NEW_STATE_ID");

                entity.Property(e => e.OldStateId).HasColumnName("OLD_STATE_ID");

                entity.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.ActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_LOG_ACTION_TYPE");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.IssueLogCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_ISSUE_LOG_USER");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ISSUE_LOG_ISSUE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.IssueLogMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_ISSUE_LOG_USER");

                entity.HasOne(d => d.NewState)
                    .WithMany(p => p.IssueLogNewState)
                    .HasForeignKey(d => d.NewStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NEW_ISSUE_LOG_STATE");

                entity.HasOne(d => d.OldState)
                    .WithMany(p => p.IssueLogOldState)
                    .HasForeignKey(d => d.OldStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLD_ISSUE_LOG_STATE");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IssueLog)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_ISSUE_LOG_SUPPLIER");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("LOCATION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.LocationCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_LOCATION_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.LocationMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_LOCATION_USER");
            });

            modelBuilder.Entity<Malfunction>(entity =>
            {
                entity.ToTable("MALFUNCTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionSubgroupId).HasColumnName("MALFUNCTION_SUBGROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_ROLE");

                entity.HasOne(d => d.MalfunctionSubgroup)
                    .WithMany(p => p.Malfunction)
                    .HasForeignKey(d => d.MalfunctionSubgroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_MALFUNCTION_USER");
            });

            modelBuilder.Entity<MalfunctionGroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_GROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionGroupCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK__MALFUNCTI__CREAT__73BA3083");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionGroupMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK__MALFUNCTI__MOD_I__74AE54BC");
            });

            modelBuilder.Entity<MalfunctionSubgroup>(entity =>
            {
                entity.ToTable("MALFUNCTION_SUBGROUP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.MalfunctionGroupId).HasColumnName("MALFUNCTION_GROUP_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.MalfunctionSubgroupCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_MALFUNCTION_SUBGROUP_USER");

                entity.HasOne(d => d.MalfunctionGroup)
                    .WithMany(p => p.MalfunctionSubgroup)
                    .HasForeignKey(d => d.MalfunctionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.MalfunctionSubgroupMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_MALFUNCTION_SUBGROUP_USER");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("POST");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__POST__D9C1FA00297EABB2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.PostCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_MOD_POST_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.PostMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_POST_ROLE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.TransName)
                    .HasName("UQ_ROLE_TRANS_NAME")
                    .IsUnique();

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.TransName)
                    .HasColumnName("TRANS_NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.RoleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_ROLE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.RoleModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_ROLE_USER");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("STATE");

                entity.HasIndex(e => e.TransName)
                    .HasName("UQ_STATE_TRANS_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.TransName)
                    .HasColumnName("TRANS_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("SUPPLIER");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__SUPPLIER__D9C1FA0021944BFA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("COUNTRY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.CurrencyId).HasColumnName("CURRENCY");

                entity.Property(e => e.Edrpou)
                    .IsRequired()
                    .HasColumnName("EDRPOU")
                    .HasMaxLength(14);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("FULL_NAME")
                    .HasMaxLength(500);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Country");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.SupplierCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_SUPPLIER_USER");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Currency");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.SupplierMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_SUPPLIER_USER");
            });

            modelBuilder.Entity<Transition>(entity =>
            {
                entity.ToTable("TRANSITION");

                entity.HasIndex(e => new { e.FromStateId, e.ActionTypeId, e.ToStateId })
                    .HasName("CK_ISSUE_TRANSITION_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasColumnName("ACTION_TYPE_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.FromStateId).HasColumnName("FROM_STATE_ID");

                entity.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.ToStateId).HasColumnName("TO_STATE_ID");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.ActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTION_TYPE_ISSUE");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.TransitionCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_ISSUE_TRANSITION_USER");

                entity.HasOne(d => d.FromState)
                    .WithMany(p => p.TransitionFromState)
                    .HasForeignKey(d => d.FromStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FROM_STATE");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.TransitionMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_ISSUE_TRANSITION_USER");

                entity.HasOne(d => d.ToState)
                    .WithMany(p => p.TransitionToState)
                    .HasForeignKey(d => d.ToStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TO_STATE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.InverseCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_CREATE_USER_ROLE");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.InverseMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_USER_ROLE");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("VEHICLE");

                entity.HasIndex(e => e.Vincode)
                    .HasName("UQ_VINCODE_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Brand)
                    .HasColumnName("BRAND")
                    .HasMaxLength(50);

                entity.Property(e => e.CommissioningDate)
                    .HasColumnName("COMMISSIONING_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("INVENTORY_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.LocationId).HasColumnName("LOCATION_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Model)
                    .HasColumnName("MODEL")
                    .HasMaxLength(50);

                entity.Property(e => e.RegNum)
                    .HasColumnName("REG_NUM")
                    .HasMaxLength(15);

                entity.Property(e => e.VehicleTypeId).HasColumnName("VEHICLE_TYPE_ID");

                entity.Property(e => e.Vincode)
                    .IsRequired()
                    .HasColumnName("VINCODE")
                    .HasMaxLength(20);

                entity.Property(e => e.WarrantyEndDate)
                    .HasColumnName("WARRANTY_END_DATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.VehicleCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_MOD_VEHICLE_USER");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_VEHICLE_LOCATION");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_VEHICLE_ROLE");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_TYPE");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VEHICLE_TYPE");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__VEHICLE___D9C1FA0095358636")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.VehicleTypeCreate)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_USER");

                entity.HasOne(d => d.Mod)
                    .WithMany(p => p.VehicleTypeMod)
                    .HasForeignKey(d => d.ModifiedById)
                    .HasConstraintName("FK_MOD_VEHICLE_TYPE_ROLE");
            });
            #endregion
        }
    }
}
