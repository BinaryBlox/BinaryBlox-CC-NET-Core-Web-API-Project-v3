using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Constants;
using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Models.Identity;
using BinaryBlox.SDK.Identity.Interfaces;
using BinaryBlox.SDK.Identity.Authorization;

#pragma warning disable 1591
namespace BinaryBlox.SDK.Account.Services
{
    public class RolesManagementService : IRolesManagementService
    {
        private readonly BxIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesManagementService(IIdentityDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IHttpContextAccessor httpAccessor)
        {
            _context = context as BxIdentityDbContext;
            _context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IList<ApplicationRole>> GetRolesAsync() { 
            return await _roleManager.Roles.ToListAsync();
        }

      
        // Role Permissions
 
        
        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId) => await _roleManager.FindByIdAsync(roleId);

        public Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}