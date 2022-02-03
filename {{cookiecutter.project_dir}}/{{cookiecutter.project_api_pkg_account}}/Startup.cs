
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using AutoMapper;

using BinaryBlox.SDK.Account.Repository;
using BinaryBlox.SDK.Account.Services;
using BinaryBlox.SDK.Constants;
using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Interfaces.Context;
using BinaryBlox.SDK.Data.Models.Identity;
using BinaryBlox.SDK.Identity.Authorization;
using BinaryBlox.SDK.Identity.Interfaces;
using BinaryBlox.SDK.Identity.Managers;
using BinaryBlox.SDK.Middleware;
using BinaryBlox.SDK.Utils;
using BinaryBlox.SDK.Web.Startup;
 
using {{cookiecutter.project_api_pkg_account}}.Constants;
using {{cookiecutter.project_api_pkg_account}}.DAL;
using {{cookiecutter.project_api_pkg_account}}.DAL.Configuration;
using {{cookiecutter.project_api_pkg_account}}.Microservice.Mapper;
using {{cookiecutter.project_api_pkg_account}}.Services;

using AppPermissions = BinaryBlox.SDK.Identity.Authorization.ApplicationPermissions;

namespace {{cookiecutter.project_api_pkg_account}}
{
    /// <summary>
    /// Prepare for Azure: dotnet publish -c Release -o ./publish
    /// </summary> 
    public class Startup: BxStartup
    {

        private string _identityConnectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment) { }
        
        /// <summary>
        /// Called in Startup constructor
        /// </summary>
        public override void ConfigureProperties(){ }
 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {

            // IMPORTANT: Keep on top - Adding Permission services...
            services.AddPermissionServices<BxAccountPermissionDbContext>(this.Configuration, this.MigrationsAssembly);
 
            base.ConfigureServices(services);

            // Add functionality to inject IOptions<T>
            services.AddOptions();
 
            // DB Context(s)
            services.AddScoped<IIdentityDbContext, BxAccountIdentityDbContext>();
            services.AddScoped<BxIdentityDbContext, BxAccountIdentityDbContext>(); 

            // DB Creation and Seeding 
            services.AddTransient<IBxDataInitializer, BxAccountDataInitializer>();

            // DB Creation and Seeding 
            // * Required for DI for Data Initializer
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IAccountManager, AccountManager>(); 
            services.AddScoped<PermissionManagerCoreService>();
            services.AddScoped<IRolesManagementService, RolesManagementService>(); 
 
            // NOTE: API Versioning (Added in Startup logic...)  
            // Adding Middleware
            services.AddSwaggerServices<Startup>(Configuration, Environment, this.SwaggerConfiguration);  
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ConfigureDbProviderMSSQL(IServiceCollection services)
        {

            // For Application DbContext
            services.AddDbContext<BxApplicationDbContext>(options =>
                options.UseSqlServer(this.ConnectionString, sql => sql.MigrationsAssembly(this.MigrationsAssembly)));

            // For IdentityDbContext
            this._identityConnectionString = Configuration.GetValue<string>("ConnectionStrings:IdentityDefaultConnection");

            services.AddDbContext<BxAccountIdentityDbContext>(options =>
                options.UseSqlServer(this._identityConnectionString, sql => sql.MigrationsAssembly(this.MigrationsAssembly)));

        }

        /// <summary>
        /// 
        /// </summary>
        public override void ConfigureAutoMapper(IServiceCollection services)
        {
            // AUTO Mapper Profile  (DI)
            services.AddAutoMapper(typeof(Startup));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
                // cfg.AddProfile<AccountAutoMapperProfile>();
                cfg.AllowNullCollections = true;
                // cfg.ShouldMapField = fi => false; 
            });

            configuration.CompileMappings();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="dataInitializer"></param>
        /// <param name="provider"></param>
        public override void Configure(IApplicationBuilder app, IHostEnvironment env, IBxDataInitializer dataInitializer, IApiVersionDescriptionProvider provider)
        {
            app.UseDeveloperExceptionPage();

            // Default configuration options in base
            base.Configure(app, env, dataInitializer, provider);

            // Authorization & Authentication
            app.UseAuthentication(); // Must be after UseRouting()
            app.UseAuthorization(); // Must be after UseAuthentication()

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            // IMPORTANT: Middleware required
            app.UseSwaggerConfiguration<Startup>(this,
                Configuration,
                env,
                provider,
                this.SwaggerConfiguration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureIdentityServerOptions(IServiceCollection services)
        {

            /*** 
            * Identity: Authentication & Authorization  
            * Notes: Can override token parameters to do custom token validation - see SDK 
            **/ 
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<BxAccountIdentityDbContext>();

            /*** NOTE: Area for adding Dev Signing Credential if needed later 
            * ... if (Environment.IsDevelopment()) builder.AddDeveloperSigningCredential(persistKey: false);
            **/
            
            /*** Registering Identity Authorization middleware **/
            services.AddMvcCore()
                .AddMvcOptions(options =>
                {
                    options.EnableEndpointRouting = false; // TODO-COMMENT
                })
                .AddAuthorization();
 
            /*** Custom middleware doing the heavy lifting - can override and use default middleware **/
            services.AddBxJwtAuthentication(JwtBearerDefaults.AuthenticationScheme, this.IdentityAuthorizationCfg);
  
            /*** Policy Based Authorization - default policies **/ 
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.View_Users));
                options.AddPolicy(Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.Manage_Users));
                options.AddPolicy(Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.View_Roles));
                options.AddPolicy(Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleAuthorizationRequirement()));
                options.AddPolicy(Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, AppPermissions.Manage_Roles));
                options.AddPolicy(Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new AssignRolesAuthorizationRequirement()));
            });

            /*** Policy Based Authorization - registering default authorization handlers **/ 
            services.AddSingleton<IAuthorizationHandler, ViewUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesAuthorizationHandler>();
 
            /*** START: Experimental Policy Based Authorization - default policies **/ 
            // services.AddAuthorization(options =>
            // {
            //       options.AddPolicy(
            //         "UserNamePolicy",
            //         policy => policy.Requirements.Add(new UserNameRequirement("admin")));
            // });
 
            // // services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            // // services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            // /*** Scoped is required for the DB Context since it is scoped ***/
            // services.AddScoped<IAuthorizationHandler, UserNameRequirementHandler>();
            /*** END: Experimental Policy Based Authorization - default policies **/ 

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        override public void ConfigureRepositoryScope(IServiceCollection services)
        {
            base.ConfigureRepositoryScope(services);

            // DI for Dependency Injection.
            services.AddScoped<BxClientApplicationRepository>();
            services.AddScoped<BxClientApplicationUserMapRepository>();

            // Identity   
            services.AddScoped<BxApiScopeRepository>();
            services.AddScoped<BxApiScopeClaimRepository>();
            services.AddScoped<BxApiScopePropertyRepository>();

            services.AddScoped<BxIdentityClientRepository>();
            services.AddScoped<BxIdentityClientCorsOriginRepository>();
            services.AddScoped<BxIdentityClientClaimRepository>();
            services.AddScoped<BxIdentityClientPropertyRepository>();
            services.AddScoped<BxIdentityClientScopeRepository>();
            services.AddScoped<BxIdentityClientSecretRepository>();
            services.AddScoped<BxIdentityClientRedirectUriRepository>();
            services.AddScoped<BxIdentityClientPostLogoutRedirectUriRepository>();
            services.AddScoped<BxIdentityClientGrantTypeRepository>();
            services.AddScoped<BxIdentityClientIdPRestrictionRepository>();

            services.AddScoped<BxIdentityResourceRepository>();
            services.AddScoped<BxIdentityResourceClaimRepository>();
            services.AddScoped<BxIdentityResourcePropertyRepository>();

            services.AddScoped<BxApiResourceRepository>();
            services.AddScoped<BxApiResourceClaimRepository>();
            services.AddScoped<BxApiResourcePropertyRepository>();
            services.AddScoped<BxApiResourceSecretRepository>();
            services.AddScoped<BxApiResourceScopeRepository>();
        }
 
        /// <summary>
        /// Adding the namespace of the controllers should suffice and deprecate this method in the future.
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureControllers(IServiceCollection services){
            
            base.ConfigureControllers(services);

            /*** Don't need to do this step as all the controllers in the entire namespace are added. Leaving one example for reference...**/
            // services.AddMvc().AddApplicationPart(typeof(BxClientApplicationController).Assembly);
       }

#region Addional methods to override if necessary
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInitializer"></param>
        public override void SeedDataFromConfiguration(IBxDataInitializer dataInitializer) => base.SeedDataFromConfiguration(dataInitializer);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureJsonSettings(IServiceCollection services) => base.ConfigureJsonSettings(services);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        override public void ConfigureApiVersioning(IServiceCollection services) => base.ConfigureApiVersioning(services);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public override void ConfigureCors(IApplicationBuilder app) => base.ConfigureCors(app);
 
#endregion
 
    }  
}
