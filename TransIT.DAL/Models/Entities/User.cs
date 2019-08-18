using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public class User : IdentityUser, IAuditableEntity
    {
        public User()
        {
            ActionTypeCreate = new HashSet<ActionType>();
            ActionTypeMod = new HashSet<ActionType>();
            BillCreate = new HashSet<Bill>();
            BillMod = new HashSet<Bill>();
            CountryCreate = new HashSet<Country>();
            CountryMod = new HashSet<Country>();
            CurrencyCreate = new HashSet<Currency>();
            CurrencyMod = new HashSet<Currency>();
            DocumentCreate = new HashSet<Document>();
            DocumentMod = new HashSet<Document>();
            EmployeeCreate = new HashSet<Employee>();
            EmployeeMod = new HashSet<Employee>();
            InverseCreate = new HashSet<User>();
            InverseMod = new HashSet<User>();
            IssueCreate = new HashSet<Issue>();
            IssueLogCreate = new HashSet<IssueLog>();
            IssueLogMod = new HashSet<IssueLog>();
            IssueMod = new HashSet<Issue>();
            LocationCreate = new HashSet<Location>();
            LocationMod = new HashSet<Location>();
            MalfunctionCreate = new HashSet<Malfunction>();
            MalfunctionGroupCreate = new HashSet<MalfunctionGroup>();
            MalfunctionGroupMod = new HashSet<MalfunctionGroup>();
            MalfunctionMod = new HashSet<Malfunction>();
            MalfunctionSubgroupCreate = new HashSet<MalfunctionSubgroup>();
            MalfunctionSubgroupMod = new HashSet<MalfunctionSubgroup>();
            PostCreate = new HashSet<Post>();
            PostMod = new HashSet<Post>();
            SupplierCreate = new HashSet<Supplier>();
            SupplierMod = new HashSet<Supplier>();
            TransitionCreate = new HashSet<Transition>();
            TransitionMod = new HashSet<Transition>();
            VehicleCreate = new HashSet<Vehicle>();
            VehicleMod = new HashSet<Vehicle>();
            VehicleTypeCreate = new HashSet<VehicleType>();
            VehicleTypeMod = new HashSet<VehicleType>();
            Employees = new HashSet<Employee>();
        }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; } 

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedById { get; set; }

        public string UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual User ModifiedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<ActionType> ActionTypeCreate { get; set; }

        public virtual ICollection<ActionType> ActionTypeMod { get; set; }

        public virtual ICollection<Bill> BillCreate { get; set; }

        public virtual ICollection<Bill> BillMod { get; set; }

        public virtual ICollection<Country> CountryCreate { get; set; }

        public virtual ICollection<Country> CountryMod { get; set; }

        public virtual ICollection<Currency> CurrencyCreate { get; set; }

        public virtual ICollection<Currency> CurrencyMod { get; set; }

        public virtual ICollection<Document> DocumentCreate { get; set; }

        public virtual ICollection<Document> DocumentMod { get; set; }

        public virtual ICollection<Employee> EmployeeCreate { get; set; }

        public virtual ICollection<Employee> EmployeeMod { get; set; }

        public virtual ICollection<User> InverseCreate { get; set; }

        public virtual ICollection<User> InverseMod { get; set; }

        public virtual ICollection<Issue> IssueCreate { get; set; }

        public virtual ICollection<IssueLog> IssueLogCreate { get; set; }

        public virtual ICollection<IssueLog> IssueLogMod { get; set; }

        public virtual ICollection<Issue> IssueMod { get; set; }

        public virtual ICollection<Location> LocationCreate { get; set; }

        public virtual ICollection<Location> LocationMod { get; set; }

        public virtual ICollection<Malfunction> MalfunctionCreate { get; set; }

        public virtual ICollection<MalfunctionGroup> MalfunctionGroupCreate { get; set; }

        public virtual ICollection<MalfunctionGroup> MalfunctionGroupMod { get; set; }

        public virtual ICollection<Malfunction> MalfunctionMod { get; set; }

        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupCreate { get; set; }

        public virtual ICollection<MalfunctionSubgroup> MalfunctionSubgroupMod { get; set; }

        public virtual ICollection<Post> PostCreate { get; set; }

        public virtual ICollection<Post> PostMod { get; set; }

        public virtual ICollection<Role> RoleCreatedByNavigation { get; set; }

        public virtual ICollection<Role> RoleModifiedByNavigation { get; set; }

        public virtual ICollection<Supplier> SupplierCreate { get; set; }

        public virtual ICollection<Supplier> SupplierMod { get; set; }

        public virtual ICollection<Transition> TransitionCreate { get; set; }

        public virtual ICollection<Transition> TransitionMod { get; set; }

        public virtual ICollection<Vehicle> VehicleCreate { get; set; }

        public virtual ICollection<Vehicle> VehicleMod { get; set; }

        public virtual ICollection<VehicleType> VehicleTypeCreate { get; set; }

        public virtual ICollection<VehicleType> VehicleTypeMod { get; set; }

        public ICollection<Unit> UnitCreate { get; set; }

        public ICollection<Unit> UnitMod { get; set; }
    }
}