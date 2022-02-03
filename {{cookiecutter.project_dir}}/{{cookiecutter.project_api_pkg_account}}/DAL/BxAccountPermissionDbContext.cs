
using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Enums;
using BinaryBlox.SDK.Data.Interfaces.Context;
using BinaryBlox.SDK.Data.Models.Permission;

using Serilog; 

namespace {{cookiecutter.project_api_pkg_account }}.DAL
{
  //--------------------------------------
    // MIGRATIONS
    //--------------------------------------
    // IMPORTANT NOTE: Migrations are not required in this API right now. All is being done in Identity project.
    // dotnet ef migrations remove -c BxPermissionDbContext
    // dotnet ef migrations add InitialAccountApiMigration -c BxPermissionDbContext -o Migrations
    // dotnet ef database update -c BxPermissionDbContext // ***NOTE: IF ERRORS FROM ABOVE DO THIS 
#pragma warning disable 1591

    //***************************************
    //- FOR POST-MIGRATION
    //***************************************

    /// <summary>
    /// 
    /// </summary>
    public class BxAccountPermissionDbContext : BxDbContext, IBxPermissionDbContext
    {

        private BxAccountIdentityDbContext _identityDbContext;

        // Required if you want to update archive and delete values.
        private bool _addRetentionSchema = true;

        /// <summary>
        /// BxAccountPermissionDbContext
        /// </summary>
        /// <param name="options"></param>
        /// <param name="identityDbContext"></param>
        public BxAccountPermissionDbContext(DbContextOptions<BxAccountPermissionDbContext> options, BxAccountIdentityDbContext identityDbContext)
            : base(options, EntityCaseType.SnakeCase)
        {
            // Important; keep to prevent lazy loading
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;

            _identityDbContext = identityDbContext;

            // ApplicationUser user = _identityDbContext.Users.First();
            // this.CurrentUserId = (user != null) ? user.Id : string.Empty;
            // _identityDbContext.CurrentUserId = this.CurrentUserId;
        }

        /// <summary>
        /// Model contstraints for the Permission API
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Leave to 'true'; initially thought we only needed during migration but we require  
            var permModelConfig = new PermissionModelBuilderConfig();
                permModelConfig.AddIdentityToKeyIndex = true;
                permModelConfig.AddRetentionSchema = _addRetentionSchema;

            builder.CreatePermissionModel(permModelConfig);  
        }

        /*********************************
        * Permission Data
        *********************************/
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermClaim> BxPermClaim { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntity> BxPermEntity { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroup> BxPermEntityGroup { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModule> BxPermModule { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleRole> BxPermModuleRole { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermUser> BxPermUser { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermStatus> BxPermStatus { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermAudit> BxPermAudit { get; set; }

        /*********************************
        * Permission Type Data
        *********************************/
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermClaimType> BxPermClaimType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityType> BxPermEntityType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupType> BxPermEntityGroupType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleType> BxPermModuleType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleType> BxPermRoleType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermUserType> BxPermUserType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermStatusType> BxPermStatusType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermAuditType> BxPermAuditType { get; set; }


        /*********************************
        * Permission Cross (Xref) Reference Data
        *********************************/ 
        public DbSet<BxPermApplicationRoleXref> BxPermApplicationRoleXref { get; set; }
        public DbSet<BxPermEntityApplicationXref> BxPermEntityApplicationXref { get; set; }
        public DbSet<BxPermEntityGroupRoleXref> BxPermEntityGroupRoleXref { get; set; }
        public DbSet<BxPermModuleClaimXref> BxPermModuleClaimXref { get; set; }
        public DbSet<BxPermRoleModuleClaimXref> BxPermRoleModuleClaimXref { get; set; } 
        public DbSet<BxPermEntityGroupRoleUserXref> BxPermEntityGroupRoleUserXref { get; set; }
        public DbSet<BxPermEntityGroupUserXref> BxPermEntityGroupUserXref { get; set; }
        public DbSet<BxPermEntityUserXref> BxPermEntityUserXref { get; set; }
 
    }


    //***************************************
    //- FOR PRE-MIGRATION
    //***************************************
        //  public partial class BxPermissionDbContext : DbContext
        //  { 
        //     public BxPermissionDbContext()
        //     {
        //     }

        //     public BxPermissionDbContext(DbContextOptions<BxPermissionDbContext> options)
        //         : base(options)
        //     {
        //     }


        //     //***************************************
        //     //- Retrieve DB Settings from .appSettings vs' hardcoded
        //     //***************************************
        //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     {
        //         if (!optionsBuilder.IsConfigured)
        //         {
        //             var configuration = GetContextConfiguration();
        //             optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("Asyml8.Account.API"));

        //         }
        //     }

        //     /// <summary>
        //     /// Model contstraints for the Permission API
        //     /// </summary>
        //     /// <param name="builder"></param>
        //     protected override void OnModelCreating(ModelBuilder builder)
        //     {

        //         base.OnModelCreating(builder);  

        //         builder.CreatePermissionModel(false);

        //     //     // Important: Creates snake case for the Entities
        //     //     builder.AddSnakeCaseToEntities();

        //     //     // Displays logging for model mappings
        //     //     System.Console.WriteLine("Creating Asyml8.Account.API Model Mapping(s)...");

        //     //     builder.SetDefaultValuesTableName(true);


        //     //     /*********************************
        //     //     * Permission Data
        //     //     *********************************/

        //     //     // REQUIRED Fields
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermClaim>().Property(c => c.ClaimTypeId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntity>().Property(c => c.EntityTypeId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroup>().Property(c => c.EntityId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroup>().Property(c => c.EntityGroupTypeId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModule>().Property(c => c.ModuleTypeId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRole>().Property(c => c.RoleTypeId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermUser>().Property(c => c.ApplicationUserId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermUser>().Property(c => c.UserTypeId).IsRequired();


        //     //     // IGNORED Fields
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermClaim>().ToTable("bx_perm_claim").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntity>().ToTable("bx_perm_entity").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroup>().ToTable("bx_perm_entity_group").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModule>().ToTable("bx_perm_module").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRole>().ToTable("bx_perm_role").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermUser>().ToTable("bx_perm_user").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

        //     //     /*********************************
        //     //     * Permission Type Data
        //     //     *********************************/

        //     //     // IGNORED Fields
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermClaimType>().ToTable("bx_perm_claim_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityType>().ToTable("bx_perm_entity_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupType>().ToTable("bx_perm_entity_group_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleType>().ToTable("bx_perm_module_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleType>().ToTable("bx_perm_role_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermUserType>().ToTable("bx_perm_user_type").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

        //     //     /*********************************
        //     //     * Permission Cross (Xref) Reference Data
        //     //     *********************************/

        //     //     // IGNORED Fields
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermApplicationRoleXref>().ToTable("bx_perm_application_role_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityApplicationXref>().ToTable("bx_perm_entity_application_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleXref>().ToTable("bx_perm_entity_group_role_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleClaimXref>().ToTable("bx_perm_module_claim_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleClaimXref>().ToTable("bx_perm_role_module_claim_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleXref>().ToTable("bx_perm_role_module_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleUserXref>().ToTable("bx_perm_user_entity_group_role_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupUserXref>().ToTable("bx_perm_user_entity_group_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityUserXref>().ToTable("bx_perm_user_entity_xref").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

        //     //     // REQUIRED & INDEX Fields
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermApplicationRoleXref>().Property(c => c.ApplicationId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermApplicationRoleXref>().Property(c => c.RoleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermApplicationRoleXref>().HasIndex(
        //     //            idx => new { idx.ApplicationId, idx.RoleId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityApplicationXref>().Property(c => c.ApplicationId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityApplicationXref>().Property(c => c.EntityId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityApplicationXref>().HasIndex(
        //     //             idx => new { idx.ApplicationId, idx.EntityId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleXref>().Property(c => c.ApplicationRoleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleXref>().Property(c => c.EntityGroupId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleXref>().HasIndex(
        //     //             idx => new { idx.ApplicationRoleId, idx.EntityGroupId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleClaimXref>().Property(c => c.ModuleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleClaimXref>().Property(c => c.ClaimId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleClaimXref>().HasIndex(
        //     //             idx => new { idx.ModuleId, idx.ClaimId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleClaimXref>().Property(c => c.RoleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleClaimXref>().Property(c => c.ModuleClaimXrefId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleClaimXref>().HasIndex(
        //     //             idx => new { idx.RoleId, idx.ModuleClaimXrefId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleXref>().Property(c => c.RoleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleXref>().Property(c => c.ModuleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleModuleXref>().HasIndex(
        //     //             idx => new { idx.RoleId, idx.ModuleId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleUserXref>().Property(c => c.EntityGroupRoleId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleUserXref>().Property(c => c.UserId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupRoleUserXref>().HasIndex(
        //     //             idx => new { idx.EntityGroupRoleId, idx.UserId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupUserXref>().Property(c => c.UserId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupUserXref>().Property(c => c.EntityGroupId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupUserXref>().HasIndex(
        //     //             idx => new { idx.UserId, idx.EntityGroupId }).IsUnique(true);

        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityUserXref>().Property(c => c.UserId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityUserXref>().Property(c => c.EntityId).IsRequired();
        //     //     builder.Entity<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityUserXref>().HasIndex(
        //     //             idx => new { idx.UserId, idx.EntityId }).IsUnique(true);


        //         base.OnModelCreating(builder);
        //     }


        //     /// <summary>
        //     /// 
        //     /// </summary>
        //     /// <returns></returns>
        //     protected IConfigurationRoot GetContextConfiguration()
        //     {
        //         Log.Debug("Adding configuration paths...");
        //         IConfigurationRoot configuration = new ConfigurationBuilder()
        //             .SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json")
        //             .AddJsonFile("appsettings.Production.json", optional: true)
        //             .AddJsonFile("appsettings.Staging.json", optional: true)
        //             .AddJsonFile("appsettings.QA.json", optional: true)
        //             .AddJsonFile("appsettings.Development.json", optional: true)
        //             .Build();
        //         return configuration;
        //     }

        //     /*********************************
        //     * Permission Data
        //     *********************************/
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermClaim> BxPermClaim { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntity> BxPermEntity { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroup> BxPermEntityGroup { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModule> BxPermModule { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleRole> BxPermModuleRole { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermUser> BxPermUser { get; set; }

        //     /*********************************
        //     * Permission Type Data
        //     *********************************/
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermClaimType> BxPermClaimType { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityType> BxPermEntityType { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermEntityGroupType> BxPermEntityGroupType { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermModuleType> BxPermModuleType { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermRoleType> BxPermRoleType { get; set; }
        //     public DbSet<BinaryBlox.SDK.Data.Models.Permission.BxPermUserType> BxPermUserType { get; set; }

        //     /*********************************
        //     * Permission Cross (Xref) Reference Data
        //     *********************************/ 
        //     public DbSet<BxPermApplicationRoleXref> BxPermApplicationRoleXref { get; set; }
        //     public DbSet<BxPermEntityApplicationXref> BxPermEntityApplicationXref { get; set; }
        //     public DbSet<BxPermEntityGroupRoleXref> BxPermEntityGroupRoleXref { get; set; }
        //     public DbSet<BxPermModuleClaimXref> BxPermModuleClaimXref { get; set; }
        //     public DbSet<BxPermRoleModuleClaimXref> BxPermRoleModuleClaimXref { get; set; } 
        //     public DbSet<BxPermEntityGroupRoleUserXref> BxPermEntityGroupRoleUserXref { get; set; }
        //     public DbSet<BxPermEntityGroupUserXref> BxPermEntityGroupUserXref { get; set; }
        //     public DbSet<BxPermEntityUserXref> BxPermEntityUserXref { get; set; }

        // }
}
#pragma warning restore 1591