using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BinaryBlox.SDK.Data.Enums.Repository;
using BinaryBlox.SDK.Data.Models.Permission;

using Serilog;

#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.Services
{

     public partial class PermissionManagerCoreService {

        private DbContext TestContext;

        /*******************************************/
        // GET ALL
        /*******************************************/

        //         public async Task<IList<BxPermEntity>> GetAllEntities() {

        // // Log.Debug($"Here fools");
        // // return null;


        //             // return await this.PermissionRepositoryContext.DbContext.BxPermEntity.ToListAsync();
        //             // return await this.PermissionRepositoryContext.BxPermEntity.GetAll();
        //            return await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntity);

        //         }

        
       
        public async Task<IList<BxPermEntity>> GetAllEntities() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntity);
        public async Task<IList<BxPermEntityGroup>> GetAllEntityGroups() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermEntityGroup);
        public async Task<IList<BxPermClaim>> GetAllClaims() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermClaim);
        public async Task<IList<BxPermModule>> GetAllModules() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermModule);
        public async Task<IList<BxPermModuleRole>> GetAllRoles() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermModuleRole);
        public async Task<IList<BxPermUser>> GetAllUsers() => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAll, this.PermissionRepositoryContext.BxPermUser);

        public async Task<IList<BxPermEntity>> GetAllEntities(Expression<Func<BxPermEntity, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermEntity, predicate);
        public async Task<IList<BxPermEntityGroup>> GetAllEntityGroups(Expression<Func<BxPermEntityGroup, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermEntityGroup, predicate);
        public async Task<IList<BxPermClaim>> GetAllClaims(Expression<Func<BxPermClaim, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermClaim, predicate);
        public async Task<IList<BxPermModule>> GetAllModules(Expression<Func<BxPermModule, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermModule, predicate);
        public async Task<IList<BxPermModuleRole>> GetAllRoles(Expression<Func<BxPermModuleRole, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermModuleRole, predicate);
        public async Task<IList<BxPermUser>> GetAllUsers(Expression<Func<BxPermUser, bool>> predicate) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllWhere, this.PermissionRepositoryContext.BxPermUser, predicate);


        /*******************************************/
        // GET ALL - Paginated
        /*******************************************/

#nullable enable
        public async Task<IList<BxPermEntity>> GetAllEntities(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntity, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroup>> GetAllEntityGroups(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroup, page, pageCount, orderBy);
        public async Task<IList<BxPermClaim>> GetAllClaims(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermClaim, page, pageCount, orderBy);
        public async Task<IList<BxPermModule>> GetAllModules(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModule, page, pageCount, orderBy);
        public async Task<IList<BxPermModuleRole>> GetAllRoles(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModuleRole, page, pageCount, orderBy);
        public async Task<IList<BxPermUser>> GetAllUsers(int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermUser, page, pageCount, orderBy);

        public async Task<IList<BxPermEntity>> GetAllEntities(Expression<Func<BxPermEntity, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntity, predicate, page, pageCount, orderBy);
        public async Task<IList<BxPermEntityGroup>> GetAllEntityGroups(Expression<Func<BxPermEntityGroup, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermEntityGroup, predicate, page, pageCount, orderBy);
        public async Task<IList<BxPermClaim>> GetAllClaims(Expression<Func<BxPermClaim, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermClaim, predicate, page, pageCount, orderBy);
        public async Task<IList<BxPermModule>> GetAllModules(Expression<Func<BxPermModule, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModule, predicate, page, pageCount, orderBy);
        public async Task<IList<BxPermModuleRole>> GetAllRoles(Expression<Func<BxPermModuleRole, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermModuleRole, predicate, page, pageCount, orderBy);
        public async Task<IList<BxPermUser>> GetAllUsers(Expression<Func<BxPermUser, bool>> predicate, int page, int? pageCount, string? orderBy) => await this.PermissionRepositoryContext.RepositoryAction(Rp_GetMany_Action.GetAllPaginated, this.PermissionRepositoryContext.BxPermUser, predicate, page, pageCount, orderBy);
#nullable disable

        /*******************************************/
        // GET  
        /*******************************************/

        public async Task<IList<BxPermEntity>> GetEntity(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntity, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntity);
        public async Task<IList<BxPermEntityGroup>> GetEntityGroup(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermEntityGroup, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermEntityGroup);
        public async Task<IList<BxPermClaim>> GetClaim(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermClaim, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermClaim);
        public async Task<IList<BxPermModule>> GetModule(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermModule, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermModule);
        public async Task<IList<BxPermModuleRole>> GetRole(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermModuleRole, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermModuleRole);
        public async Task<IList<BxPermUser>> GetUser(string id) => await this.PermissionRepositoryContext.RepositoryAction<BxPermUser, string>(id, Rp_Get_Action.Get, this.PermissionRepositoryContext.BxPermUser);


        /*******************************************/
        // ADD  
        /*******************************************/

        public async Task<IList<BxPermEntity>> AddEntity(BxPermEntity value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntity);
        public async Task<IList<BxPermEntityGroup>> AddEntityGroup(BxPermEntityGroup value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermEntityGroup);
        public async Task<IList<BxPermClaim>> AddClaim(BxPermClaim value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermClaim);
        public async Task<IList<BxPermModule>> AddModule(BxPermModule value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermModule);
        public async Task<IList<BxPermModuleRole>> AddRole(BxPermModuleRole value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermModuleRole);
        public async Task<IList<BxPermUser>> AddUser(BxPermUser value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Add, this.PermissionRepositoryContext.BxPermUser);


        /*******************************************/
        // UPDATE  
        /*******************************************/

        public async Task<IList<BxPermEntity>> UpdateEntity(BxPermEntity value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntity);
        public async Task<IList<BxPermEntityGroup>> UpdateEntityGroup(BxPermEntityGroup value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroup);
        public async Task<IList<BxPermClaim>> UpdateClaim(BxPermClaim value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermClaim);
        public async Task<IList<BxPermModule>> UpdateModule(BxPermModule value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModule);
        public async Task<IList<BxPermModuleRole>> UpdateRole(BxPermModuleRole value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModuleRole);
        public async Task<IList<BxPermUser>> UpdateUser(BxPermUser value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermUser);


        /*******************************************/
        // DELETE  
        /*******************************************/

        public async Task<IList<BxPermEntity>> DeleteEntity(BxPermEntity value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntity);
        public async Task<IList<BxPermEntityGroup>> DeleteEntityGroup(BxPermEntityGroup value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermEntityGroup);
        public async Task<IList<BxPermClaim>> DeleteClaim(BxPermClaim value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermClaim);
        public async Task<IList<BxPermModule>> DeleteModule(BxPermModule value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModule);
        public async Task<IList<BxPermModuleRole>> DeleteRole(BxPermModuleRole value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermModuleRole);
        public async Task<IList<BxPermUser>> DeleteUser(BxPermUser value) => await this.PermissionRepositoryContext.RepositoryAction(value, Rp_CRUD_Action.Update, this.PermissionRepositoryContext.BxPermUser);
 
    }
}