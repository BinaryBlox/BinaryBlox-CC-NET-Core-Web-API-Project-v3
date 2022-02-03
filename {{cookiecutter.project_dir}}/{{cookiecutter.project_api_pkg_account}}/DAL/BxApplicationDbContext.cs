using System.Linq;

using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Enums;
using BinaryBlox.SDK.Data.Models.Identity;

using Duende.IdentityServer.EntityFramework.Entities;

namespace {{cookiecutter.project_api_pkg_account }}.DAL
{ 
    //--------------------------------------
    // MIGRATIONS
    //--------------------------------------
    // IMPORTANT NOTE: Migrations are not required in this API right now. All is being done in Identity project.
    // dotnet ef migrations remove -c BxApplicationDbContext
    // dotnet ef migrations add InitialAccountApiMigration -c BxApplicationDbContext -o Migrations
    // dotnet ef database update -c BxApplicationDbContext // ***NOTE: IF ERRORS FROM ABOVE DO THIS 
#pragma warning disable 1591
    /// <summary>
    /// 
    /// </summary>
    public partial class BxApplicationDbContext : BxDbContext
    {
        private BxAccountIdentityDbContext _identityDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="identityDbContext"></param>
        public BxApplicationDbContext(DbContextOptions<BxApplicationDbContext> options, BxAccountIdentityDbContext identityDbContext)
            : base(options, EntityCaseType.SnakeCase)
        {
            // Important; keep to prevent lazy loading
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;

            _identityDbContext = identityDbContext;

            ApplicationUser user = _identityDbContext.Users.First();
            this.CurrentUserId = (user != null) ? user.Id : string.Empty;
            _identityDbContext.CurrentUserId = this.CurrentUserId;
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder); 
        }
       
    }


    //***************************************
    //- FOR PRE-MIGRATION
    //***************************************
    // public partial class ApplicationDbContext : DbContext
    // { 
    //     public ApplicationDbContext()
    //     {
    //     }

    //     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //         : base(options)
    //     {
    //     }

    //     //***************************************
    //     //- NOT NEEDED: 1/26/2021 - > COMMENT after ADD or REMOVE migration
    //     //***************************************
    //     // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     // {
    //     //     if (!optionsBuilder.IsConfigured)
    //     //     {
    //     //         optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=binaryblox.newver.identity.server.dev;User=sa;Password=KP_g00cortp_2020;MultipleActiveResultSets=true");
    //     //     }
    //     // } 
    // }
}
#pragma warning restore 1591