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

        public async Task<IList<BxPermApplicationRoleXref>> GetAllApplicationRolesXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermApplicationRoleXref);
        public async Task<IList<BxPermEntityApplicationXref>> GetAllEntityApplicationsXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityApplicationXref);
        public async Task<IList<BxPermEntityGroupRoleXref>> GetAllEntityGroupRolesXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref);
        public async Task<IList<BxPermModuleClaimXref>> GetAllModuleClaimsXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermModuleClaimXref);
        public async Task<IList<BxPermRoleModuleClaimXref>> GetAllRoleModuleClaimsXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref); 
        public async Task<IList<BxPermEntityGroupRoleUserXref>> GetAllEntityGroupUserRolesXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref);
        public async Task<IList<BxPermEntityGroupUserXref>> GetAllEntityGroupUsersXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityGroupUserXref);
        public async Task<IList<BxPermEntityUserXref>> GetAllEntityUsersXref() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityUserXref);


        /*******************************************/
        // GET ALL - Paginated
        /*******************************************/
#nullable enable
        public async Task<IList<BxPermApplicationRoleXref>> GetAllApplicationRolesXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermApplicationRoleXref, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityApplicationXref>> GetAllEntityApplicationsXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityApplicationXref, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroupRoleXref>> GetAllEntityGroupRolesXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref, page, pageCount, orderBy);
        public async Task<IList<BxPermModuleClaimXref>> GetAllModuleClaimsXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModuleClaimXref, page, pageCount, orderBy);
        public async Task<IList<BxPermRoleModuleClaimXref>> GetAllRoleModuleClaimsXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroupRoleUserXref>> GetAllEntityGroupUserRolesXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroupUserXref>> GetAllEntityGroupUsersXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroupUserXref, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityUserXref>> GetAllEntityUsersXref(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityUserXref, page, pageCount, orderBy);
#nullable disable

        /*******************************************/
        // GET  
        /*******************************************/

        public async Task<IList<BxPermApplicationRoleXref>> GetApplicationRoleXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermApplicationRoleXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermApplicationRoleXref);
        public async Task<IList<BxPermEntityApplicationXref>> GetEntityApplicationXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityApplicationXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityApplicationXref);
        public async Task<IList<BxPermEntityGroupRoleXref>> GetEntityGroupRoleXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityGroupRoleXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref);
        public async Task<IList<BxPermModuleClaimXref>> GetModuleClaimXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermModuleClaimXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermModuleClaimXref);
        public async Task<IList<BxPermRoleModuleClaimXref>> GetRoleModuleClaimXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermRoleModuleClaimXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref); 
        public async Task<IList<BxPermEntityGroupRoleUserXref>> GetEntityGroupUserRoleXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityGroupRoleUserXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref);
        public async Task<IList<BxPermEntityGroupUserXref>> GetEntityGroupUserXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityGroupUserXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityGroupUserXref);
        public async Task<IList<BxPermEntityUserXref>> GetEntityUserXref(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityUserXref, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityUserXref);


        /*******************************************/
        // ADD  
        /*******************************************/

        public async Task<IList<BxPermApplicationRoleXref>> AddApplicationRoleXref(BxPermApplicationRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermApplicationRoleXref);
        public async Task<IList<BxPermEntityApplicationXref>> AddEntityApplicationXref(BxPermEntityApplicationXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityApplicationXref);
        public async Task<IList<BxPermEntityGroupRoleXref>> AddEntityGroupRoleXref(BxPermEntityGroupRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref);
        public async Task<IList<BxPermModuleClaimXref>> AddModuleClaimXref(BxPermModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermModuleClaimXref);
        public async Task<IList<BxPermRoleModuleClaimXref>> AddRoleModuleClaimXref(BxPermRoleModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref); 
        public async Task<IList<BxPermEntityGroupRoleUserXref>> AddEntityGroupUserRoleXref(BxPermEntityGroupRoleUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref);
        public async Task<IList<BxPermEntityGroupUserXref>> AddEntityGroupUserXref(BxPermEntityGroupUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityGroupUserXref);
        public async Task<IList<BxPermEntityUserXref>> AddEntityUserXref(BxPermEntityUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityUserXref);


        /*******************************************/
        // UPDATE  
        /*******************************************/

        public async Task<IList<BxPermApplicationRoleXref>> UpdateApplicationRoleXref(BxPermApplicationRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermApplicationRoleXref);
        public async Task<IList<BxPermEntityApplicationXref>> UpdateEntityApplicationXref(BxPermEntityApplicationXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityApplicationXref);
        public async Task<IList<BxPermEntityGroupRoleXref>> UpdateEntityGroupRoleXref(BxPermEntityGroupRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref);
        public async Task<IList<BxPermModuleClaimXref>> UpdateModuleClaimXref(BxPermModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModuleClaimXref);
        public async Task<IList<BxPermRoleModuleClaimXref>> UpdateRoleModuleClaimXref(BxPermRoleModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref); 
        public async Task<IList<BxPermEntityGroupRoleUserXref>> UpdateEntityGroupUserRoleXref(BxPermEntityGroupRoleUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref);
        public async Task<IList<BxPermEntityGroupUserXref>> UpdateEntityGroupUserXref(BxPermEntityGroupUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroupUserXref);
        public async Task<IList<BxPermEntityUserXref>> UpdateEntityUserXref(BxPermEntityUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityUserXref);


        /*******************************************/
        // DELETE  
        /*******************************************/

        public async Task<IList<BxPermApplicationRoleXref>> DeleteApplicationRoleXref(BxPermApplicationRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermApplicationRoleXref);
        public async Task<IList<BxPermEntityApplicationXref>> DeleteEntityApplicationXref(BxPermEntityApplicationXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermEntityApplicationXref);
        public async Task<IList<BxPermEntityGroupRoleXref>> DeleteEntityGroupRoleXref(BxPermEntityGroupRoleXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermEntityGroupRoleXref);
        public async Task<IList<BxPermModuleClaimXref>> DeleteModuleClaimXref(BxPermModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermModuleClaimXref);
        public async Task<IList<BxPermRoleModuleClaimXref>> DeleteRoleModuleClaimXref(BxPermRoleModuleClaimXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermRoleModuleClaimXref); 
        public async Task<IList<BxPermEntityGroupRoleUserXref>> DeleteEntityGroupUserRoleXref(BxPermEntityGroupRoleUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermEntityGroupRoleUserXref);
        public async Task<IList<BxPermEntityGroupUserXref>> DeleteEntityGroupUserXref(BxPermEntityGroupUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermEntityGroupUserXref);
        public async Task<IList<BxPermEntityUserXref>> DeleteEntityUserXref(BxPermEntityUserXref value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Delete, this.PermissionRepositoryContext.BxPermEntityUserXref);

    }
}