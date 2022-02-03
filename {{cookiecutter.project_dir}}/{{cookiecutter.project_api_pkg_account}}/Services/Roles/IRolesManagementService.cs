using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
 
using BinaryBlox.SDK.Data.Models.Identity;

#pragma warning disable 1591
namespace BinaryBlox.SDK.Account.Services
{
    public interface IRolesManagementService
    {
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IList<ApplicationRole>> GetRolesAsync();
    }
}