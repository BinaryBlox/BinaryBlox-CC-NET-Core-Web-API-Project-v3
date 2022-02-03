using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using AutoMapper;

using BinaryBlox.SDK.Data.Configuration.Seed;
using BinaryBlox.SDK.Data.Interfaces.Context;
using BinaryBlox.SDK.Data.Interfaces.Entity;
using BinaryBlox.SDK.Data.Models.Identity;
using BinaryBlox.SDK.Data.Service;
using BinaryBlox.SDK.Identity.Interfaces;
using BinaryBlox.SDK.Web.Http.Enum;
 
using {{cookiecutter.project_api_pkg_account}}.DAL; 

#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.Services
{

    
    public partial class PermissionManagerCoreService : BxPermissionRepositoryContextService<BxAccountPermissionDbContext>
    {

        private readonly PermissionManagerConfigurationSeed _permissionManagerSeed;

        public PermissionManagerCoreService(IIdentityDbContext identityContext,
            IBxPermissionDbContext permissionDbContext,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IHttpContextAccessor httpAccessor) : base(identityContext, permissionDbContext, userManager, httpAccessor, configuration)
        {

             _permissionManagerSeed = new PermissionManagerConfigurationSeed(identityContext, permissionDbContext, userManager, configuration); 
            //    _identityContext.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim(); 
        }

        public async Task SeedPermissionData()
        {
            await _permissionManagerSeed.SeedAsyncWithoutMigration();
        } 


           /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isAscending"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<int> GetNextKeyIndex<TEntity>(IBxPermissionDbContext context, bool isAscending = false)
        where TEntity : class, IBxIndexableAuditableEntityType<int>
        {
            var value = await context.Set<TEntity>().OrderByDescending(o => o.KeyIndex).FirstOrDefaultAsync();

            return (value != null) ? value.KeyIndex + 1 : 1;
        }

 
    }
}
