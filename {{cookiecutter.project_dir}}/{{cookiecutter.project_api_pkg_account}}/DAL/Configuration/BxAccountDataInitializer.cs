using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Data.Interfaces.Context;

using Serilog;


namespace {{cookiecutter.project_api_pkg_account}}.DAL.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class BxAccountDataInitializer :  IBxDataInitializer
    {
 
        private readonly BxApplicationDbContext _context;
        private readonly BxAccountIdentityDbContext _identityDbcontext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityDbContext"></param>
        public BxAccountDataInitializer(
            BxApplicationDbContext context,
            BxAccountIdentityDbContext identityDbContext)
        {
             _context = context;
             _identityDbcontext = identityDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            // Only seed ApplicationDbContext
            await _context.Database.MigrateAsync().ConfigureAwait(false);
 
            Log.Information($"START: Seeding Account API Data");
 
            //await CreateAccountData();

            Log.Information($"END: Seeding Account API Data");

        }
 
    }
}