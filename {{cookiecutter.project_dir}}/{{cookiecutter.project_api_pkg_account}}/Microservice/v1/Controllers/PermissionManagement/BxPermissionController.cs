using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
 
using BinaryBlox.SDK.Account.Services;
using BinaryBlox.SDK.Constants;
using BinaryBlox.SDK.Data.ViewModels.Entity;
using BinaryBlox.SDK.Identity.Interfaces;
using BinaryBlox.SDK.Web.Http.Enum;
using BinaryBlox.SDK.Web.Http.Interfaces;
using BinaryBlox.SDK.Web.Http.Request;
using BinaryBlox.SDK.Web.Http.Response;

using StoredProcedureEFCore;

using Swashbuckle.AspNetCore.Annotations;
 
using {{cookiecutter.project_api_pkg_account}}.DAL;
using {{cookiecutter.project_api_pkg_account}}.Services;

#pragma warning disable 1591
namespace BinaryBlox.SDK.Account.Controllers
{

    // [Authorize(AuthenticationSchemes = BinaryBloxIdentityDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class BxPermissionController : ControllerBase
    {
        private readonly BxApplicationDbContext _dbContext;
        private readonly BxAccountIdentityDbContext _identityDbContext;
        private readonly BxAccountPermissionDbContext _permissionDbContext;
        private readonly PermissionManagerCoreService _permissionManagerService;
        private readonly IAccountManager _accountManagerService;
        private readonly IRolesManagementService _userRolesService;
        private readonly IMapper _mapper;
        private readonly ILogger<BxPermissionController> _logger;
        // private readonly IEmailSender _emailSender;
 

        public const string SP_GET_PERMISSION_CLAIMS_BY_ENTITY = "sp_bx_get_permission_user_claims_by_entity";
        
        /// <summary>
        /// 
        /// </summary>
        public const string API_CONTROLLER_TAG = "PermissionManager";



        public BxPermissionController(BxApplicationDbContext dbContext,
            BxAccountIdentityDbContext identityDbContext,
            BxAccountPermissionDbContext permissionDbContext,
            IAccountManager accountManagerService,
            IRolesManagementService userRolesService,
            ILogger<BxPermissionController> logger,
            PermissionManagerCoreService permissionManagerService,
            IMapper mapper
            // IEmailSender emailSender,
            // ILogger<BxPermissionController> logger,
            )
        {
            _dbContext = dbContext;
            _identityDbContext = identityDbContext;
            _permissionDbContext = permissionDbContext;
            _accountManagerService = accountManagerService;
            _userRolesService = userRolesService;
            _permissionManagerService = permissionManagerService;
            _mapper = mapper;
            _logger = logger;
            // _emailSender = emailSender;
            // _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns> 
        // [Authorize(Policies.ViewAllUsersPolicy)]
        // [Authorize(Policy = "UserNamePolicy")]
        [HttpPost("GetAllEntities")]
        [SwaggerOperation(
          Summary = "POST BxPermissionEntityRequest (GetAllEntities)",
          Description = ApiWebActionOpenAPI.BX_GET_ALL,
          Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(IBxResponse<BxPermissionEntityViewModel, Guid>))]
        public async Task<ActionResult<IBxResponse<BxPermissionEntityViewModel, Guid>>> GetAllEntities([FromBody] BxPermissionEntityRequest request)
        {

            var requestDetail = _permissionManagerService.ValidateBxRequest(request);
            if (!requestDetail.valid) return BadRequest();

            var entity = request.RequestBody.Entity;
            IBxResponse<BxPermissionEntityViewModel, Guid> result;

            try
            {

                var values = await _permissionManagerService.GetAllEntities();
                List<BxPermissionEntityViewModel> entityVms = new List<BxPermissionEntityViewModel>();

                foreach (var item in values)
                    entityVms.Add(_mapper.Map<BxPermissionEntityViewModel>(item));

                result = _permissionManagerService.BuildBxResponse<BxPermissionEntityViewModel>(entityVms, ResponseType.GetAll);

            }
            catch (System.Exception error)
            {
                throw error;
                // return BadRequest($"Error occured processing request: {error.Message}");
            }

            return Ok(result);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUserClaimsAggregate")]
        [SwaggerOperation(
          Summary = "POST BxPermissionEntityRequest (GetUserClaimsAggregate)",
          Description = ApiWebActionOpenAPI.BX_GET_ALL,
          Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(IBxResponse<PmEntityUserClaimAggregateViewModel, Guid>))]
        public async Task<ActionResult<IBxResponse<PmEntityUserClaimAggregateViewModel, Guid>>> GetUserClaimsAggregate([FromBody] BxRequest<PmEntityUserClaimAggregateViewModel, Guid> request)
        {
            //  "entity": {
            //       "applicationClientId": "e1ef7a24-d2c4-46b6-ea31-08d9ae0765ba", 
            //       "entityId": "7bc5443e-f36b-1410-8ffc-00f7ca0ad523",
            //       "userId": "79c5443e-f36b-1410-8ffc-00f7ca0ad523" 
            //     }

            var requestDetail = _permissionManagerService.ValidateBxRequest(request);
            if (!requestDetail.valid) return BadRequest();

            var reqEntity = request.RequestBody.Entity;
            IBxResponse<PmEntityUserClaimAggregateViewModel, Guid> result;

            try
            {
                _logger.LogInformation($"ApplicationClient...{reqEntity.ApplicationClientId.ToString()}\nEntity...{reqEntity.EntityId.ToString()}\nUser...{reqEntity.UserId.ToString()}");

                var values = await GetAllUserClaimsListAsync(reqEntity.ApplicationClientId.ToString(),
                    reqEntity.EntityId.ToString(),
                    reqEntity.UserId.ToString(),
                    requestDetail.pageSize);

                List<PmEntityUserClaimAggregateViewModel> entityVms = new List<PmEntityUserClaimAggregateViewModel>();

                _logger.LogInformation($"Count...{values.Count}");

                foreach (var item in values)
                    entityVms.Add(_mapper.Map<PmEntityUserClaimAggregateViewModel>(item));

                result = _permissionManagerService.BuildBxResponse<PmEntityUserClaimAggregateViewModel>(entityVms, ResponseType.GetAll);
            }
            catch (System.Exception error)
            {
                return BadRequest($"Error occured processing request: {error.Message}\nStack: {error.StackTrace}");
            }

            return Ok(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationClientId"></param>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <param name="maxRows"></param>
        /// <returns></returns>
        private async Task<List<PmEntityUserClaimAggregateViewModel>> GetAllUserClaimsListAsync(string applicationClientId, string entityId, string userId, int maxRows = 2000)
        {

            List<PmEntityUserClaimAggregateViewModel> result = new List<PmEntityUserClaimAggregateViewModel>();

            try
            {
                _logger.LogInformation($"Executing {SP_GET_PERMISSION_CLAIMS_BY_ENTITY}...");
                await _permissionDbContext.LoadStoredProc(SP_GET_PERMISSION_CLAIMS_BY_ENTITY)
                         .AddParam("option", "GetPermissionUserClaimsByEntity")
                         .AddParam<string>("applicationClientId", applicationClientId)
                         .AddParam<string>("entityId", entityId)
                         .AddParam<string>("userId", userId)
                         //  .AddParam<string>("applicationClientId", applicationClientId != null ? applicationClientId.ToString() : "00000000-0000-0000-0000-000000000000")
                         //  .AddParam<string>("entityId", entityId != null ? entityId.ToString() : "00000000-0000-0000-0000-000000000000")
                         //  .AddParam<string>("userId", userId != null ? userId.ToString() : "00000000-0000-0000-0000-000000000000")
                         .AddParam<int>("maxRows", maxRows > 0 ? maxRows : 2000)
                         .ExecAsync(async r => result = await r.ToListAsync<PmEntityUserClaimAggregateViewModel>());

                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "GetAllUserClaimsListAsync");
            }


            return result;

        }
 

#nullable disable
    }

    public class BxPermissionViewModel : BxAuditableEntityContentDto<Guid>
    {
        public Guid ApplicationId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000"); // Default to empty
        public string NormalizedName { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; }
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }

    public class BxPermissionEntityViewModel : BxAuditableEntityContentDto<Guid>
    {
        // public Guid ApplicationId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000"); // Default to empty
        // public string NormalizedName { get; set; } 
        // public virtual ICollection<IdentityUserRole<string>> Users { get; set; }
        // public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }

    public class PmEntityUserClaimAggregateViewModel : BxEntityDto<Guid>
    {
        // PmEntityViewModel Entity {get; set;}
        public Guid ApplicationClientId { get; set; }
        public Guid UserId { get; set; }
        public Guid ExternalUserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EntityClaimKey { get; set; }
        public string EntityModuleRoleKey { get; set; }
        public int EntityKeyIdx { get; set; }
        public int EntityGroupKeyIdx { get; set; }
        public int ModuleKeyIdx { get; set; }
        public int ModuleRoleKeyIdx { get; set; }
        public int ClaimKeyIdx { get; set; }
        public string Entity { get; set; }
        public string EntityCode { get; set; }
        public Guid EntityId { get; set; }
        public string EntityGroup { get; set; }
        public string EntityGroupCode { get; set; }
        public Guid EntityGroupId { get; set; }
        public string Module { get; set; }
        public string ModuleCode { get; set; }
        public Guid ModuleId { get; set; }
        public string ModuleRole { get; set; }
        public string ModuleRoleCode { get; set; }
        public Guid ModuleRoleId { get; set; }
        public string Claim { get; set; }
        public string ClaimCode { get; set; }
        public Guid ClaimId { get; set; }
        //         public string EntityGroupRole {get; set;}
        // public string EntityGroupRoleCode {get; set;}
        public Guid EntityGroupRoleId { get; set; }
        public Guid ApplicationRoleId { get; set; }
        public Guid EntityGroupRoleUserId { get; set; }

    }



    /// <summary>
    /// 
    /// </summary>
    public class BxPermissionRequest : BxRequest<BxPermissionViewModel, Guid> { }

    /// <summary>
    /// 
    /// </summary>
    public class BxPermissionResponse : BxResponse<BxPermissionViewModel, Guid> { }

    /// <summary>
    /// 
    /// </summary>
    public class BxPermissionEntityRequest : BxRequest<BxPermissionEntityViewModel, Guid> { }

    /// <summary>
    /// 
    /// </summary>
    public class BxPermissionEntityResponse : BxResponse<BxPermissionEntityViewModel, Guid> { }


}
#pragma warning restore 1591
