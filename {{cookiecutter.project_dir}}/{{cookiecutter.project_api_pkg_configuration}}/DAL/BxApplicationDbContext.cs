using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using BinaryBlox.SDK.Data.Context;  
using BinaryBlox.SDK.Data.Enums;
using BinaryBlox.SDK.Data.Models.Configuration;
using BinaryBlox.SDK.Data.Models.Identity; 

using Serilog;
#pragma warning disable 1591

namespace {{cookiecutter.project_api_pkg_configuration}}.DAL
{ 
    //--------------------------------------
    // MIGRATIONS
    //--------------------------------------
    // dotnet ef migrations remove -c BxApplicationDbContext
    // dotnet ef migrations add InitialConfigurationApiMigration -c BxApplicationDbContext -o Migrations
    // dotnet ef database update -c BxApplicationDbContext

    //--------------------------------------
    // IMPORTANT: Requires Nuget package for custom indexatrribute
    // https://www.nuget.org/packages/Toolbelt.EntityFrameworkCore.IndexAttribute
    //--------------------------------------
  
    //***************************************
    //- FOR POST-MIGRATION
    //*************************************** 
    // public partial class BxApplicationDbContext : BxDbContext
    // {

    //     private readonly BxConfigurationIdentityDbContext _identityDbContext;

    //     /// <summary>
    //     /// 
    //     /// </summary>
    //     /// <param name="options"></param>
    //     /// <param name="identityDbContext"></param>
    //     public BxApplicationDbContext(DbContextOptions<BxApplicationDbContext> options, BxConfigurationIdentityDbContext identityDbContext)
    //         : base(options, EntityCaseType.SnakeCase)
    //     {

    //         // NOTE; keep to prevent lazy loading
    //         // this.Configuration.LazyLoadingEnabled = false;
    //         // this.Configuration.ProxyCreationEnabled = false; 
    //         _identityDbContext = identityDbContext;
 
    //         // ApplicationUser user = _identityDbContext.Users.First();
    //         // this.CurrentUserId = (user != null) ? user.Id : string.Empty;
    //         // _identityDbContext.CurrentUserId = this.CurrentUserId;  
    //     }

    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribAccessPriority> BxAttribAccessPriority { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribTemplate> BxAttribTemplate { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribValType> BxAttribValType { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration> BxConfiguration { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAccessPriority> BxConfigurationAccessPriority { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribVal> BxConfigurationAttribVal { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribValOverride> BxConfigurationAttribValOverride { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationMedia> BxConfigurationMedia { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationTemplate> BxConfigurationTemplate { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationType> BxConfigurationType { get; set; }
    //     public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxPriorityRank> BxPriorityRank { get; set; }

    // }
 
    //***************************************
    //- FOR POST-MIGRATION - END
    //*************************************** 
 
    //***************************************
    //- FOR PRE-MIGRATION
    //***************************************
    public partial class BxApplicationDbContext : DbContext
    {
 
        public BxApplicationDbContext() { }

        public BxApplicationDbContext(DbContextOptions<BxApplicationDbContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = GetContextConfiguration();
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("{{cookiecutter.project_api_pkg_configuration}}"));
 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);  

            // Important: Creates snake case for the Entities
            builder.AddSnakeCaseToEntities();

            // Displays logging for model mappings
            System.Console.WriteLine("Creating {{cookiecutter.project_api_pkg_configuration}} Model Mapping(s)...");
            
            builder.SetDefaultValuesTableName(true);

            // BxAttribAccessPriority
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxAttribAccessPriority>().Property(c => c.PriorityRankId).IsRequired();

            // BxAttribTemplate
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxAttribTemplate>().Property(c => c.ConfigurationTemplateId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxAttribTemplate>().Property(c => c.AttribValueTypeId).IsRequired();

            // BxAttribValType
            // -NONE-

            // BxConfiguration
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration>().Property(c => c.ConfigurationTemplateId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration>().Property(c => c.ConfigurationGroupId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration>().Property(c => c.IsEnabled).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration>().Property(c => c.Name).IsRequired();

            // BxConfiguration - Index
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration>().HasIndex(
                   idx => new { idx.ConfigurationTemplateId, idx.ConfigurationGroupId, idx.Name }).IsUnique(true);

            // BxConfigurationAccessPriority
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAccessPriority>().Property(c => c.ConfigurationId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAccessPriority>().Property(c => c.PriorityRankId).IsRequired();

            // BxConfigurationAttribVal
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribVal>().Property(c => c.ConfigurationId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribVal>().Property(c => c.AttribTemplateId).IsRequired();

            // BxConfigurationAttribVal - Index
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribVal>().HasIndex(
                   idx => new { idx.ConfigurationId, idx.AttribTemplateId }).IsUnique(true);

            // BxConfigurationAttribValOverride
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribValOverride>().Property(c => c.ConfigurationAttribValId).IsRequired();
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribValOverride>().Property(c => c.AttribAccessPriorityId).IsRequired();

            // BxConfigurationMedia
            // -NONE-

            // BxConfigurationTemplate
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationTemplate>().Property(c => c.ConfigurationTypeId).IsRequired();

            // BxConfigurationType
            // -NONE- 

            // BxPriorityRank
            builder.Entity<BinaryBlox.SDK.Data.Models.Configuration.BxPriorityRank>().Property(c => c.PriorityRank).IsRequired();

           
        }

        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribAccessPriority> BxAttribAccessPriority { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribTemplate> BxAttribTemplate { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxAttribValType> BxAttribValType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfiguration> BxConfiguration { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAccessPriority> BxConfigurationAccessPriority { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribVal> BxConfigurationAttribVal { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationAttribValOverride> BxConfigurationAttribValOverride { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationMedia> BxConfigurationMedia { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationTemplate> BxConfigurationTemplate { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxConfigurationType> BxConfigurationType { get; set; }
        public DbSet<BinaryBlox.SDK.Data.Models.Configuration.BxPriorityRank> BxPriorityRank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IConfigurationRoot GetContextConfiguration()
        {
            Log.Debug("Adding configuration paths...");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Production.json", optional: true)
                .AddJsonFile("appsettings.Staging.json", optional: true)
                .AddJsonFile("appsettings.QA.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            return configuration;
        }
    }
    //***************************************
    //- FOR PRE-MIGRATION - END
    //*************************************** 
}

#pragma warning restore 1591


// OTHER CODE FOR LATER
// public void AttachEntity(IEntityWithKey entity)
// {
//     ((IObjectContextAdapter)this).ObjectContext.Attach(entity);
// }

// public void DetachEntity(object entity)
// {
//     ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
// }

// /// <summary>
// /// 
// /// </summary>
// /// <param name="acceptAllChangesOnSuccess"></param>
// /// <returns></returns>
// public override int SaveChanges(bool acceptAllChangesOnSuccess)
// {
//     OnBeforeSaving();
//     return base.SaveChanges(acceptAllChangesOnSuccess);
// }

// /// <summary>
// /// 
// /// </summary>
// /// <returns></returns>
// public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
// {
//     OnBeforeSaving();
//     return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
// }

// /// <summary>
// /// 
// /// </summary>
// private void OnBeforeSaving()
// {
//     ApplicationUser user = _apiIdentityDbContext.Users.First();
//     Log.Debug ($"DBContext-OnBeforeSaving: UserName [$ { user.UserName } ]");

//     var entries = ChangeTracker.Entries();
//     foreach (var entry in entries)
//     {
//         var now = DateTime.UtcNow; 
//         if (entry.Entity is IBBEntity trackable)
//         {
//             switch (entry.State)
//             {
//                 case EntityState.Added:
//                     Log.Debug ($"DBContext: Update IBBEntity - State Added [ {entry.Entity.GetType().FullName} ]");
//                     trackable.GuidId = Guid.NewGuid();
//                     break;
//             }
//         }

//         if (entry.Entity is IBBAuditableEntity auditable)
//         {
//             switch (entry.State)
//             {
//                 case EntityState.Modified:
//                 Log.Debug ($"DBContext: Update IBBAuditableEntity - State Modified [ {entry.Entity.GetType().FullName}]");
//                     auditable.UpdatedDate = now;
//                     auditable.UpdatedBy = user.Id;
//                     break;

//                 case EntityState.Added:
//                     Log.Debug ($"DBContext: Update IBBAuditableEntity - State Added [ {entry.Entity.GetType().FullName} ]");
//                     auditable.CreatedDate = now;
//                     auditable.CreatedBy = user.Id;;
//                     auditable.UpdatedDate = now;
//                     auditable.UpdatedBy = user.Id;;
//                     break;
//             }
//         }
//     }
// }

// /// <summary>
// /// 
// /// </summary>
// /// <returns></returns>
// #pragma warning disable 1998
// private string GetCurrentUser()
// {
//     return "UserName"; // TODO implement your own logic

//     // If you are using ASP.NET Core, you should look at this answer on StackOverflow
//     // https://stackoverflow.com/a/48554738/2996339
// }

// /// <summary>
// /// 
// /// </summary>
// #pragma warning disable 1998
// public void UndoChanges()
// {
//    var changedEntries = this.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

//     foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
//     {
//         entry.CurrentValues.SetValues(entry.OriginalValues);
//         entry.State = EntityState.Unchanged;
//     }

//     foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
//     {
//         entry.State = EntityState.Detached;
//     }

//     foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
//     {
//         entry.State = EntityState.Unchanged;
//     }
// } 