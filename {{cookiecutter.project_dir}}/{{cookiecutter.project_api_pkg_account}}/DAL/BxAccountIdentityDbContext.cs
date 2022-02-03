using System;

using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Enums; 

#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.DAL
{ 
    //***************************************
    //- FOR PRE-MIGRATION 
    //***************************************

    /*** We aren't migrating anything right now so, POST Migration configuration is employed ***/ 
    // public partial class BxAccountIdentityDbContext : BxIdentityDbContext
    // {

    //     /// <summary>
    //     /// 
    //     /// </summary>
    //     /// <param name="options"></param>
    //     public BxAccountIdentityDbContext(DbContextOptions<BxAccountIdentityDbContext> options)
    //         : base(options)
    //     {
    //     }

    //     // /// <summary>
    //     // /// 
    //     // /// </summary>
    //     // /// <param name="builder"></param>
    //     // protected override void OnModelCreating(ModelBuilder builder)
    //     // {
    //     //     base.OnModelCreating(builder);
    //     // }
    // }

    //***************************************
    //- FOR POST-MIGRATION
    //***************************************

    /// <summary>
    /// 
    /// </summary>
    public partial class BxAccountIdentityDbContext : BxIdentityDbContext
    {

          private static readonly EntityCaseType ENTITY_CASE_TYPE = EntityCaseType.SnakeCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public BxAccountIdentityDbContext(DbContextOptions<BxAccountIdentityDbContext> options)
             : base(options,  ENTITY_CASE_TYPE)
        {
        }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
 
            /*********************************
            * API Scopes
            *********************************/
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiScope>().ToTable("api_scopes").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiScopeClaim>().ToTable("api_scope_claims").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiScopeProperty>().ToTable("api_scope_properties").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

            /*********************************
            * Identity Resources
            *********************************/
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResource>().ToTable("identity_resources").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResourceClaim>().ToTable("identity_resource_claims").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResourceProperty>().ToTable("identity_resource_properties").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

            /*********************************
            * API Resources
            *********************************/
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiResource>().ToTable("api_resources").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceProperty>().ToTable("api_resource_properties").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceClaim>().ToTable("api_resource_claims").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceScope>().ToTable("api_resource_scopes").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceSecret>().ToTable("api_resource_secrets").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);

            /*********************************
            * Identity Clients
            *********************************/
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClient>().ToTable("clients").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientClaim>().ToTable("client_claims").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientGrantType>().ToTable("client_grant_types").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientIdPRestriction>().ToTable("client_id_prestrictions").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientProperty>().ToTable("client_properties").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientScope>().ToTable("client_scopes").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientSecret>().ToTable("client_secrets").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientCorsOrigin>().ToTable("client_cors_origins").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientRedirectUri>().ToTable("client_redirect_uris").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);
            builder.Entity<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientPostLogoutRedirectUri>().ToTable("client_post_logout_redirect_uris").Ignore(t => t.isArchived).Ignore(t => t.isDeleted);


            // Set Entity Case
            switch (ENTITY_CASE_TYPE)
            {
                case EntityCaseType.SnakeCase:
                    builder.AddSnakeCaseToEntities();
                    break;
                case EntityCaseType.PascalCase:
                case EntityCaseType.UpperCamelCase:
                case EntityCaseType.LowerCamelCase:
                default:

                    // Do nothing for any other case
                    break;
            }
        }
        
        /*********************************
        * IMPORTANT NOTE: Do NOT need to add the entities below because they are already mapped above. For example,
        * userContext.ClientSecrets dbset will automatically be mapped to userContext.BxClientSecrets with the declaration below.
        * Choosing to leave the below entities commented out for future reference or in the event that they are needed.
        *********************************/

        // /*********************************
        // * API Scopes
        // *********************************/
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiScope> BxApiScopes { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiScopeClaim> BxApiScopeClaims { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiScopeProperty> BxApiScopeProperties { get; set; }

        // /*********************************
        // * Identity Resources
        // *********************************/
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResource> BxIdentityResources { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResourceClaim> BxIdentityResourceClaims { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityResourceProperty> BxIdentityResourceProperties { get; set; }

        // /*********************************
        // * API Resources
        // *********************************/
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiResource> BxApiResources { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceClaim> BxApiResourceClaims { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceProperty> BxApiResourceProperties { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceScope> BxApiResourceScopes { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxApiResourceSecret> BxApiResourceSecrets { get; set; }

        // /*********************************
        // * Identity Clients
        // *********************************/
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClient> BxIdentityClients { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientCorsOrigin> BxIdentityClientCorsOrigins { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientClaim> BxIdentityClientClaims { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientProperty> BxIdentityClientProperties { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientScope> BxIdentityClientscopes { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientSecret> BxIdentityClientsecrets { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientRedirectUri> BxIdentityClientRedirectUris { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientPostLogoutRedirectUri> BxIdentityClientPostLogoutRedirectUris { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientGrantType> BxIdentityClientGrantTypes { get; set; }
        // public DbSet<BinaryBlox.SDK.Data.Models.Identity.BxIdentityClientIdPRestriction> BxIdentityClientIdPRestrictions { get; set; }
    
    }
}