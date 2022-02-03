using System.Collections.Generic;
using System.Threading.Tasks;

using BinaryBlox.SDK.Data.Enums.Repository; 
using BinaryBlox.SDK.Data.Models.Permission;
 
#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.Services
{
 
    public partial class PermissionManagerCoreService
    {
        /*******************************************/
        // GET ALL
        /*******************************************/

        public async Task<IList<BxPermEntityType>> GetAllEntityTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityType);
        public async Task<IList<BxPermEntityGroupType>> GetAllEntityGroupTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityGroupType);
        public async Task<IList<BxPermClaimType>> GetAllClaimTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermClaimType);
        public async Task<IList<BxPermModuleType>> GetAllModuleTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermModuleType);
        public async Task<IList<BxPermRoleType>> GetAllRoleTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermRoleType);
        public async Task<IList<BxPermUserType>> GetAllUserTypes() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermUserType);


        /*******************************************/
        // GET ALL - Paginated
        /*******************************************/
#nullable enable
        public async Task<IList<BxPermEntityType>> GetAllEntityTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityType, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroupType>> GetAllEntityGroupTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroupType, page, pageCount, orderBy);
        public async Task<IList<BxPermClaimType>> GetAllClaimTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermClaimType, page, pageCount, orderBy);
        public async Task<IList<BxPermModuleType>> GetAllModuleTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModuleType, page, pageCount, orderBy);
        public async Task<IList<BxPermRoleType>> GetAllRoleTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermRoleType, page, pageCount, orderBy);
        public async Task<IList<BxPermUserType>> GetAllUserTypes(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermUserType, page, pageCount, orderBy);
#nullable disable

        /*******************************************/
        // GET  
        /*******************************************/

        public async Task<IList<BxPermEntityType>> GetEntityType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityType);
        public async Task<IList<BxPermEntityGroupType>> GetEntityGroupType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityGroupType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityGroupType);
        public async Task<IList<BxPermClaimType>> GetClaimType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermClaimType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermClaimType);
        public async Task<IList<BxPermModuleType>> GetModuleType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermModuleType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermModuleType);
        public async Task<IList<BxPermRoleType>> GetRoleType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermRoleType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermRoleType);
        public async Task<IList<BxPermUserType>> GetUserType(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermUserType, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermUserType);


        /*******************************************/
        // ADD  
        /*******************************************/

        public async Task<IList<BxPermEntityType>> AddEntityType(BxPermEntityType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityType);
        public async Task<IList<BxPermEntityGroupType>> AddEntityGroupType(BxPermEntityGroupType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityGroupType);
        public async Task<IList<BxPermClaimType>> AddClaimType(BxPermClaimType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermClaimType);
        public async Task<IList<BxPermModuleType>> AddModuleType(BxPermModuleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermModuleType);
        public async Task<IList<BxPermRoleType>> AddRoleType(BxPermRoleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermRoleType);
        public async Task<IList<BxPermUserType>> AddUserType(BxPermUserType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermUserType);


        /*******************************************/
        // UPDATE  
        /*******************************************/

        public async Task<IList<BxPermEntityType>> UpdateEntityType(BxPermEntityType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityType);
        public async Task<IList<BxPermEntityGroupType>> UpdateEntityGroupType(BxPermEntityGroupType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroupType);
        public async Task<IList<BxPermClaimType>> UpdateClaimType(BxPermClaimType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermClaimType);
        public async Task<IList<BxPermModuleType>> UpdateModuleType(BxPermModuleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModuleType);
        public async Task<IList<BxPermRoleType>> UpdateRoleType(BxPermRoleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermRoleType);
        public async Task<IList<BxPermUserType>> UpdateUserType(BxPermUserType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermUserType);


        /*******************************************/
        // DELETE  
        /*******************************************/

        public async Task<IList<BxPermEntityType>> DeleteEntityType(BxPermEntityType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityType);
        public async Task<IList<BxPermEntityGroupType>> DeleteEntityGroupType(BxPermEntityGroupType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroupType);
        public async Task<IList<BxPermClaimType>> DeleteClaimType(BxPermClaimType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermClaimType);
        public async Task<IList<BxPermModuleType>> DeleteModuleType(BxPermModuleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModuleType);
        public async Task<IList<BxPermRoleType>> DeleteRoleType(BxPermRoleType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermRoleType);
        public async Task<IList<BxPermUserType>> DeleteUserType(BxPermUserType value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermUserType);

    }
}