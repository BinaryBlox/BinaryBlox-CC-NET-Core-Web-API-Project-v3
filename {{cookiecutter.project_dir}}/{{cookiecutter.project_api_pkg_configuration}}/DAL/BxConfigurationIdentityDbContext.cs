using System;

using Microsoft.EntityFrameworkCore; 

using BinaryBlox.SDK.Data.Context;

namespace {{cookiecutter.project_api_pkg_configuration}}.DAL
{ 
  
    /// <summary>
    /// 
    /// </summary>
    public partial class BxConfigurationIdentityDbContext : BxIdentityDbContext
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public BxConfigurationIdentityDbContext(DbContextOptions<BxConfigurationIdentityDbContext> options) : base(options) { }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}