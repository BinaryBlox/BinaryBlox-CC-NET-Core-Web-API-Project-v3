using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
 
using BinaryBlox.SDK.Account.ViewModels;
using BinaryBlox.SDK.Data.Context;
using BinaryBlox.SDK.Data.Models.Identity;
using BinaryBlox.SDK.Identity.Interfaces;
using BinaryBlox.SDK.Web.Http.Request;
using BinaryBlox.SDK.Web.Http.Response;
using BinaryBlox.SDK.Constants;

using StoredProcedureEFCore;

using Swashbuckle.AspNetCore.Annotations;

using {{cookiecutter.project_api_pkg_account}}.DAL; 
  

#pragma warning disable 1591
namespace BinaryBlox.SDK.Account.Controllers
{

    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class BxAccountUserController : ControllerBase
    {
        private readonly BxApplicationDbContext _context;
        private readonly BxAccountIdentityDbContext _identityContext;
        private readonly IAccountManager _accountManager;
        private readonly IMapper _mapper;
        // private readonly IEmailSender _emailSender;
        // private readonly ILogger _logger;


        /// <summary>
        /// 
        /// </summary>
        public const string API_CONTROLLER_TAG = "ApiUser";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityContext"></param>
        /// <param name="accountManager"></param> 
        /// <param name="mapper"></param>
        public BxAccountUserController(BxApplicationDbContext context,
            BxAccountIdentityDbContext identityContext,
            IAccountManager accountManager,
            IMapper mapper
            // IEmailSender emailSender,
            // ILogger<BxAccountUserController> logger,
            )
        {
            _context = context;
            _identityContext = identityContext;
            _accountManager = accountManager;
            _mapper = mapper;
            // _emailSender = emailSender;
            // _logger = logger;
        }



        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns> 
        // [Authorize(Policies.ViewAllUsersPolicy)]
        [HttpPost("GetAllUsers")]
        [SwaggerOperation(
          Summary = "POST BxAccountUserRequest (GetAllUsers)",
          Description = ApiWebActionOpenAPI.BX_GET_ALL,
          Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(BxAccountUserResponse))]
        public async Task<ActionResult<BxAccountUserResponse>> GetUsers([FromBody] BxAccountUserRequest request)
        {

            if (request == null || request.RequestBody == null || request.Metadata == null)
                return BadRequest();

            var entity = request.RequestBody.Entity;
            var result = new BxAccountUserResponse();
            var page = request.Metadata.Page.HasValue ? request.Metadata.Page.Value : 0;
            var pageSize = (request.Metadata.PageSize.HasValue && request.Metadata.PageSize.Value > 0) ? request.Metadata.PageSize.Value : 200;
            // var filterCriteria = ((request.Metadata.FilterCriteria != null) && (request.Metadata.FilterCriteria.Length > 0)) ? request.Metadata.FilterCriteria : null; 
            // var filterValue = entity.FullName;//(filterCriteria != null) ? filterCriteria[0].FieldValue : null;

            try
            {

                var usersAndRoles = await GetUsersAndRolesAsync(page, pageSize, null);

                List<BxAccountUserViewModel> entityList = new List<BxAccountUserViewModel>();

                foreach (var item in usersAndRoles)
                {
                    var userVM = _mapper.Map<BxAccountUserViewModel>(item.Item1);
                    userVM.Roles = item.Item2;

                    entityList.Add(userVM);
                }

                result.ResponseBody = new BxResponseBody<BxAccountUserViewModel, Guid>();
                result.ResponseBody.Data = entityList;
                result.ResponseBody.Count = (entityList != null) ? entityList.Count() : 0;
                result.ResponseBody.Ids = new List<Guid>();

                // Return list of Id's
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (BxAccountUserViewModel entityItem in entityList)
                        result.ResponseBody.Ids.Add(entityItem.Id);
                }

                // Status code
                result.Metadata = new BxResponseMetadata();
                result.Metadata.ResponseType = "GetAll";
                result.Metadata.StatusCode = "Success";
            }
            catch (System.Exception error)
            {
                return BadRequest($"Error occured processing request: {error.Message}");
            }

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns> 
        // [Authorize(Policies.ViewAllUsersPolicy)]
        [HttpPost("CreateUser")]
        [SwaggerOperation(
          Summary = "POST BxAccountUserRequest (CreatUser)",
          Description = ApiWebActionOpenAPI.BX_CREATE,
          Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(BxAccountUserResponse))]
        // [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<BxAccountUserResponse>> CreatUser([FromBody] BxAccountUserRequest request)
        {

            if (request == null || request.RequestBody == null || request.Metadata == null)
                return BadRequest();

            var entity = request.RequestBody.Entity;
            var result = new BxAccountUserResponse();
            var page = request.Metadata.Page.HasValue ? request.Metadata.Page.Value : 0;
            var pageSize = (request.Metadata.PageSize.HasValue && request.Metadata.PageSize.Value > 0) ? request.Metadata.PageSize.Value : 200;
            // var filterCriteria = ((request.Metadata.FilterCriteria != null) && (request.Metadata.FilterCriteria.Length > 0)) ? request.Metadata.FilterCriteria : null; 
            // var filterValue = entity.FullName;//(filterCriteria != null) ? filterCriteria[0].FieldValue : null;

            try
            {

                var usersAndRoles = await GetUsersAndRolesAsync(page, pageSize, null);

                List<BxAccountUserViewModel> usersVM = new List<BxAccountUserViewModel>();

                foreach (var item in usersAndRoles)
                {
                    var userVM = _mapper.Map<BxAccountUserViewModel>(item.Item1);
                    userVM.Roles = item.Item2;

                    usersVM.Add(userVM);
                }

                result.ResponseBody = new BxResponseBody<BxAccountUserViewModel, Guid>();
                result.ResponseBody.Data = usersVM;
                result.ResponseBody.Count = (usersVM != null) ? usersVM.Count() : 0;
                result.ResponseBody.Ids = (usersVM != null) ? usersVM.Select(p => new { p.Id }).OfType<Guid>().ToList() : null;
                result.Metadata = new BxResponseMetadata();
                result.Metadata.ResponseType = "GetAll";
                result.Metadata.StatusCode = "Success";
            }
            catch (System.Exception error)
            {
                return BadRequest($"Error occured processing request: {error.Message}");
            }

            return Ok(result);
        }

#nullable enable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<List<Tuple<ApplicationUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize, string? name)
        {
            IQueryable<ApplicationUser> usersQuery = (name == null) ?
                _identityContext.Users
                    .Include(u => u.Roles)
                    .OrderBy(u => u.UserName) :
                _identityContext.Users
                    .Include(u => u.Roles)
                    .OrderBy(u => u.UserName)
                    .Where(u => u.UserName.Contains(name));


            if (page != -1)
                usersQuery = usersQuery.Skip((page - 1) * pageSize);

            if (pageSize != -1)
                usersQuery = usersQuery.Take(pageSize);

            var users = await usersQuery.ToListAsync();

            var userRoleIds = users.SelectMany(u => u.Roles.Select(r => r.RoleId)).ToList();

            var roles = await _identityContext.Roles
                .Where(r => userRoleIds.Contains(r.Id))
                .ToArrayAsync();

            return users.Select(u => Tuple.Create(u,
                roles.Where(r => u.Roles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToArray()))
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        virtual protected async Task<ApplicationUser> CreateUserAsync(string userName, string password, string givenName, string familyName, string email, string phoneNumber, string[] roles)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName,
                GivenName = givenName,
                FamilyName = familyName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");

            return applicationUser;
        }

#nullable disable 
    }

    /// <summary>
    /// 
    /// </summary>
    public class BxAccountUserRequest : BxRequest<BxAccountUserViewModel, Guid> { }

    /// <summary>
    /// 
    /// </summary>
    public class BxAccountUserResponse : BxResponse<BxAccountUserViewModel, Guid> { }

}
#pragma warning restore 1591
