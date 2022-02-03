using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using BinaryBlox.SDK.Data.Models.Configuration;
using BinaryBlox.SDK.Web.Http.Request;
using BinaryBlox.SDK.Web.Http.Response;
 
using StoredProcedureEFCore;

using Swashbuckle.AspNetCore.Annotations;

using {{cookiecutter.project_api_pkg_configuration}}.DAL;
using {{cookiecutter.project_api_pkg_configuration}}.Microservice.V1.ViewModels;

namespace {{cookiecutter.project_api_pkg_configuration}}.Microservice.V1.Controllers
{
      /// <summary>
    /// 
    /// </summary>
    /// 
    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class BxConfigurationAggregateController : ControllerBase
    {
        private readonly BxApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        public const string API_CONTROLLER_TAG = "BxConfigurationAggregate";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BxConfigurationAggregateController(BxApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of type GetBxConfigurationResult
        /// </summary> 
        /// Sample request
        ///     
        ///     (* Existing Configuration)
        ///     POST /api/BxConfigurationAggregate/UpdateBxConfigurationResult
        ///     {
        ///         "configId": "2bda423e-f36b-1410-8fde-00f7ca0ad523",
        ///         "groupId": "00000000-0000-0000-0000-000000000000",
        ///         "attributes": [
        ///         {  
        ///             "attribTemplateId": "fdd9423e-f36b-1410-8fde-00f7ca0ad523",
        ///             "attribOverrideTemplateId": "2cda423e-f36b-1410-8fde-00f7ca0ad523",
        ///             "configId": "2bda423e-f36b-1410-8fde-00f7ca0ad523", 
        ///             "sortIdx": 5,
        ///             "value": "2nd Landing Page Dashboard now available",
        ///             "mediaId": "00000000-0000-0000-0000-000000000000"
        ///         }
        ///     ]
        ///     
        ///     (* New Configuration)
        ///     POST /api/BxConfigurationAggregate/UpdateBxConfigurationResult
        ///     { 
        ///         "groupId": "00000000-0000-0000-0000-000000000000",
        ///         "attributes": [
        ///         {  
        ///             "attribTemplateId": "fdd9423e-f36b-1410-8fde-00f7ca0ad523",
        ///             "attribOverrideTemplateId": "2cda423e-f36b-1410-8fde-00f7ca0ad523",
        ///             "configId": "2bda423e-f36b-1410-8fde-00f7ca0ad523",
        ///             "name": "2nd Banner Title",
        ///             "description": "2nd Banner Title", 
        ///             "sortIdx": 5,
        ///             "value": "2nd Landing Page Dashboard now available",
        ///             "mediaId": "00000000-0000-0000-0000-000000000000"
        ///         }
        ///     ]
        /// }   
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost("Update")]
        [SwaggerOperation(
            Summary = "Updates a BxConfigurationResult object.",
            Description = "[BX_UPDATE]",
            Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(BxConfigurationAggregateResponse))]
        public async Task<ActionResult<BxConfigurationAggregateResponse>> UpdateBxConfigurationResult([FromBody] BxConfigurationAggregateRequest request)
        {
            if (request == null || request.RequestBody == null)
                return BadRequest();

            var entity = request.RequestBody.Entity;
            var result = new BxConfigurationAggregateResponse();

            List<BxConfigurationAggregateViewModel> bxConfigList = null;
            try
            {
                var bxConfig = await _context.BxConfiguration.FindAsync(entity.ConfigId);

                if (bxConfig == null)
                    return NotFound();

                var configHasAttributes = _context.BxConfigurationAttribVal.Any(v => v.ConfigurationId == entity.ConfigId);
                var configAttribTemplates = _context.BxAttribTemplate.Where(v => v.ConfigurationTemplateId == bxConfig.ConfigurationTemplateId).ToList();

                if (!configHasAttributes)
                {
                    return NotFound("No attributes have been configured for existing configuration to override");
                }

                // //foreach (var bxConfig in bxConfig.)
                // _context.LoadStoredProc("sp_bx_get_configuration_data")
                //     .AddParam("option", "GetAllBxConfigurationAttribData")
                //     .AddParam<string>("groupId", "00000000-0000-0000-0000-000000000000")
                //     .AddParam<string>("configId", request.ConfigId == null ? request.ConfigId.ToString() : "00000000-0000-0000-0000-000000000000")
                //     .Exec(r => bxConfigAttribList = r.ToList<BxConfigurationResultAttribData>());

                // // No attributes have been configured for existing configuration to override.
                // if (bxConfigAttribList == null || bxConfigAttribList.Count() <= 0)
                // {
                //     return NotFound("No attributes have been configured for existing configuration to override");
                // }

                foreach (var bxAttrib in entity.Attributes)
                {

                    // bxConfigAttribList.Find(v => v.AttribTemplateId == bxAttrib.AttribTemplateId);

                    // AttribOverrideTemplateId is the actual BxConfigurationAttribVal Id value.
                    var attribResult = await _context.BxConfigurationAttribVal.FindAsync(bxAttrib.AttribOverrideTemplateId);
                    BxConfigurationAttribVal targetAttrib = (attribResult != null) ? attribResult : new BxConfigurationAttribVal();

                    var attribTemplate = configAttribTemplates.Find(v => v.Id == bxAttrib.AttribTemplateId);

                    if (attribTemplate == null)
                        throw new Exception("Attribute Template cannot be null.");

                    bool isNewEntity = false;

                    if (targetAttrib.Id == null || targetAttrib.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        isNewEntity = true;
                        targetAttrib.ConfigurationId = bxAttrib.ConfigId;
                        targetAttrib.AttribTemplateId = bxAttrib.AttribTemplateId;
                        targetAttrib.Name = (bxAttrib.Name != null) ? bxAttrib.Name : attribTemplate.Name;
                        targetAttrib.Description = (bxAttrib.Description != null) ? bxAttrib.Description : attribTemplate.Description;
                        targetAttrib.Code = attribTemplate.Code;
                        Console.WriteLine($"Is  NEW ENTITY");
                    }
                    else
                    {
                        Console.WriteLine($"Is not NEW ENTITY");
                    }

                    // Set values for existing entity. 
                    targetAttrib.Value = bxAttrib.Value;
                    targetAttrib.Options = bxAttrib.Options;
                    targetAttrib.SortIndex = bxAttrib.SortIdx;
                    targetAttrib.ConfigurationMediaId = bxAttrib.MediaId;

                    if (isNewEntity)
                        _context.BxConfigurationAttribVal.Add(targetAttrib);
                    else
                        _context.BxConfigurationAttribVal.Update(targetAttrib);

                    await _context.SaveChangesAsync();
                }

                bxConfigList = GetBxConfigurationList(entity.GroupId, entity.ConfigId);

                result.ResponseBody = new BxResponseBody<BxConfigurationAggregateViewModel, Guid>();
                result.ResponseBody.Data = bxConfigList;
                result.ResponseBody.Count = (bxConfigList != null) ? bxConfigList.Count() : 0;
                result.ResponseBody.Ids = (bxConfigList != null) ? bxConfigList.Select(p => new {p.ConfigId}).OfType<Guid>().ToList() : null;
                result.Metadata = new BxResponseMetadata();
                result.Metadata.ResponseType = "GetAll";
                result.Metadata.StatusCode = "Success"; 
            }
            catch (System.Exception error)
            {
                return BadRequest($"Error occured processing request: {error.Message}");
            }
 
            return result;
        }

        /// <summary>
        /// Returns a list of type GetBxConfigurationResult
        /// </summary> 
        /// Sample request:
        ///
        ///     POST /api/BxConfigurationAggregate/UpdateBxConfigurationResult
        ///     {
        ///         "configId": "2bda423e-f36b-1410-8fde-00f7ca0ad523",
        ///         "groupId": "00000000-0000-0000-0000-000000000000",
        ///         "maxRows":  200
        ///     } 
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost("GetAllConfig")]
        [SwaggerOperation(
            Summary = "Get all values for BxConfigurationResult",
            Description = "[BX_GET_ALL]",
            Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(BxConfigurationAggregateResponse))]
        public ActionResult<BxConfigurationAggregateResponse> GetAllConfig([FromBody] BxConfigurationAggregateRequest request)
        {
            if (request == null || request.RequestBody == null)
                return BadRequest();

            var entity = request.RequestBody.Entity;
            var result = new BxConfigurationAggregateResponse();

            List<BxConfigurationAggregateViewModel> bxConfigList = null;

            bxConfigList = GetBxConfigurationList(entity.GroupId, entity.ConfigId, 200);

            result.ResponseBody = new BxResponseBody<BxConfigurationAggregateViewModel, Guid>();
            result.ResponseBody.Data = bxConfigList;
            result.ResponseBody.Count = (bxConfigList != null) ? bxConfigList.Count() : 0;
            result.ResponseBody.Ids = (bxConfigList != null) ? bxConfigList.Select(p => new {p.ConfigId}).OfType<Guid>().ToList() : null;
            result.Metadata = new BxResponseMetadata();
            result.Metadata.ResponseType = "GetAll";
            result.Metadata.StatusCode = "Success"; 
            return result;
        }

        /// <summary>
        /// Returns a list of type GetBxConfigurationResult
        /// </summary> 
        /// Sample request:
        ///
        ///     POST /api/BxConfigurationAggregate/UpdateBxConfigurationResult
        ///     {
        ///         "configId": "2bda423e-f36b-1410-8fde-00f7ca0ad523",
        ///         "groupId": "00000000-0000-0000-0000-000000000000",
        ///         "maxRows":  200
        ///     } 
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost("GetAllOtherConfig")]
        [SwaggerOperation(
           Summary = "Get all values for BxConfigurationResult",
           Description = "[BX_GET_ALL]",
           Tags = new[] { API_CONTROLLER_TAG })]
        [ProducesResponseType(200, Type = typeof(BxConfigurationAggregateResponse))]
        public ActionResult<BxConfigurationAggregateResponse> GetAllOtherConfig([FromBody] BxConfigurationAggregateRequest request)
        {
 
            if (request == null || request.RequestBody == null)
                return BadRequest();

            var result = new BxConfigurationAggregateResponse();
            List<BxConfigurationAggregateViewModel> bxConfigList = null;

            var entity = request.RequestBody.Entity;

            try
            {
                bxConfigList = GetBxConfigurationList(entity.GroupId, entity.ConfigId, 200);

                result.ResponseBody = new BxResponseBody<BxConfigurationAggregateViewModel, Guid>();
                result.ResponseBody.Data = bxConfigList;
                result.ResponseBody.Count = (bxConfigList != null) ? bxConfigList.Count() : 0;
                result.ResponseBody.Ids = (bxConfigList != null) ? bxConfigList.Select(p => new {p.ConfigId}).OfType<Guid>().ToList() : null;
                result.Metadata = new BxResponseMetadata();
                result.Metadata.ResponseType = "GetAll";
                result.Metadata.StatusCode = "Success"; 
            }
            catch (System.Exception error)
            {
                return BadRequest($"Error occured processing request: {error.Message}");
            }


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<BxConfigurationAggregateViewModel> GetBxConfigurationList(Guid groupId, Guid configId, int maxRows = 200)
        {
            // List<BxConfigurationResult> bxConfigList = null;
            List<BxConfigurationAggregateViewModel> bxConfigList = null;
            List<BxConfigurationResultAttribData> bxConfigAttribList = null;
            // var option = new SqlParameter("@Option", "GetAllBxConfigurationData");
            // var groupId = new SqlParameter("@GroupId", "00000000-0000-0000-0000-000000000000");
            // var compId = new SqlParameter("@ConfigId", "35b9433e-f36b-1410-8fdb-00f7ca0ad523"); //new SqlParameter("compId", "35b9433e-f36b-1410-8fdb-00f7ca0ad523");

            // IMPORTANT! Need snake case osdn the values to work
            // var results1 = _context.BxConfigurationResult
            // //.FromSqlRaw("EXECUTE dbo.sp_bx_get_configuration_data @option, @groupId", option, groupId)
            // .FromSqlRaw("EXECUTE dbo.sp_bx_get_configuration_data @Option, @GroupId, @ConfigId", option, groupId, compId)
            // .ToList();
            _context.LoadStoredProc("sp_bx_get_configuration_data")
                              .AddParam("option", "GetAllBxConfigurationData")
                              .AddParam<string>("groupId", groupId != null ? groupId.ToString() : "00000000-0000-0000-0000-000000000000")
                              .AddParam<string>("configId", configId != null ? configId.ToString() : "00000000-0000-0000-0000-000000000000")
                              .AddParam<int>("maxRows", maxRows > 0 ? maxRows : 200)
                              .Exec(r => bxConfigList = r.ToList<BxConfigurationAggregateViewModel>());

            _context.LoadStoredProc("sp_bx_get_configuration_data")
                .AddParam("option", "GetAllBxConfigurationAttribData")
                .AddParam<string>("groupId", "00000000-0000-0000-0000-000000000000")
                .AddParam<string>("configId", configId == null ? configId.ToString() : "00000000-0000-0000-0000-000000000000")
                .Exec(r => bxConfigAttribList = r.ToList<BxConfigurationResultAttribData>());

            // Create aggregate from BxConfigList
            foreach (var bxConfig in bxConfigList)
                foreach (var bxConfigAttrib in bxConfigAttribList)
                    if (bxConfig.ConfigId == bxConfigAttrib.ConfigId)
                        bxConfig.Attributes.Add(bxConfigAttrib);

            return bxConfigList;
        }
 
    }

    /// <summary>
    /// 
    /// </summary>
    public class BxConfigurationAggregateRequest : BxRequest<BxConfigurationAggregateViewModel, Guid> { }
    
    /// <summary>
    /// 
    /// </summary>
    public class BxConfigurationAggregateResponse : BxResponse<BxConfigurationAggregateViewModel, Guid> { }


}


//***********************************************
// OLD CODE FOR REFERENCE
//***********************************************

// /**
// * getXpComponentConfigurationValues
// * @param requestBody
// * @return
// **/
// // GET: api/UserAggregateValuesByUser/5
// [HttpGet("UserAggregateValuesByUser/{id}" )]
// // [Authorize(Policies.ViewScheduleApiPolicy )]  
// [ProducesResponseType(200, Type = typeof(BxConfigurationResult))]
// [ProducesResponseType(403)]
// [ProducesResponseType(404)]
// public async Task<ActionResult<BxConfigurationResult>> GetUserAggregateValuesByUserId(int id)
// {
//     // var userResult = await _context.ScheduleUsers.FindAsync(id); 
//     // if (userResult == null) { return NotFound(); }  

//     //-----------------------------------------
//     // Schedule User
//     //-----------------------------------------

//     BxConfigurationResult itemVM = new BxConfigurationResult();

//     //itemVM.ScheduleUser = Mapper.Map<ScheduleUserViewModel>(userResult);

//     List<ScheduleUserClientViewModel> scList = new List<ScheduleUserClientViewModel>();
//     var scResult = await _context.ScheduleUserClients.Where(userClient => userClient.ScheduleUserId == id).ToListAsync();

//     if (scResult != null)
//     {
//         foreach (var item in scResult)
//         {
//             ApplicationClient appClient = ScheduleApiHelper.GetApplicationClientWithContext(item.ApplicationClientId, _context);
//             ScheduleUserClientViewModel scheduleUserclient = ScheduleUserDbHelper.PatchScheduleUserClientViewModel(appClient, item);
//             scList.Add(scheduleUserclient);
//         }
//     }

//     itemVM.ScheduleUserClients = scList;

//     //-----------------------------------------
//     // User Programs
//     //-----------------------------------------

//     List<UserProgramConfigurationViewModel> upcList = await ScheduleUserDbHelper.GetUserProgramConfigurationsByUserId(id, _context);

//     itemVM.UserProgramConfigurations = upcList;

//     List<UserActivityViewModel> userProgramActivities = null;
//     _context.LoadStoredProc("dbo.sp_ScheduleAggregateData")
//         .AddParam("Option", "GetActivitiesByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userProgramActivities = r.ToList<UserActivityViewModel>());

//     itemVM.UserProgramActivities = userProgramActivities;

//     List<UserActivityScheduleViewModel> userProgramActivitySchedules = null;
//     _context.LoadStoredProc("dbo.sp_ScheduleAggregateData")
//         .AddParam("Option", "GetActivityScheduleByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userProgramActivitySchedules = r.ToList<UserActivityScheduleViewModel>());

//     //itemVM.UserProgramActivitySchedules = userProgramActivitySchedules;

//     // Activity Actions
//     List<UserActivityActionScheduleViewModel> userProgramActivityActionsSchedules = null;
//     _context.LoadStoredProc("dbo.sp_Schedule_Activity_Data")
//         .AddParam("Option", "GetProgramActivityActionsByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userProgramActivityActionsSchedules = r.ToList<UserActivityActionScheduleViewModel>());


//     if (userProgramActivityActionsSchedules != null)
//     {

//         Log.Debug($"Activity Actions Found: {userProgramActivityActionsSchedules.Count}");

//         foreach (var activity in userProgramActivitySchedules)
//         {
//             List<UserActivityActionScheduleViewModel> actionList = new List<UserActivityActionScheduleViewModel>();

//             foreach (var activityAction in userProgramActivityActionsSchedules)
//             {
//                 Log.Debug($"Adding User Activity Action Name: {activityAction.Name}");
//                 Log.Debug($"Adding User Activity Action Id: {activityAction.Id}");
//                 Log.Debug($"Adding User Activity Id: {activity.ActivityId}");
//                 if (activityAction.ActivityId == activity.ActivityId)
//                 {
//                     actionList.Add(activityAction);
//                     Log.Debug($"Adding User Def Activity Action: {activityAction.Name}");
//                 }
//             }

//             if (actionList != null)
//             {
//                 activity.ActivityActions = actionList;
//             }

//             // var itemVM = Mapper.Map<ScheduleEntityViewModel>(item);
//             // updateEntityWithTypeInfo(ref itemVM, entityType, entityTypeValue); 

//             // entityList.Add(itemVM);
//             // counter++;
//         }
//     }



//     itemVM.UserProgramActivitySchedules = userProgramActivitySchedules;

//     //-----------------------------------------
//     // User Defined Programs
//     //-----------------------------------------

//     List<UserDefinedProgramConfigurationViewModel> udpcList = await ScheduleUserDbHelper.GetUserDefinedProgramConfigurationsByUserId(id, _context);

//     itemVM.UserDefinedProgramConfigurations = udpcList;

//     List<UserActivityViewModel> userDefinedProgramActivities = null;
//     _context.LoadStoredProc("dbo.sp_ScheduleAggregateData")
//         .AddParam("Option", "GetUserDefinedActivitiesByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userDefinedProgramActivities = r.ToList<UserActivityViewModel>());

//     itemVM.UserDefinedProgramActivities = userDefinedProgramActivities;

//     List<UserActivityScheduleViewModel> userDefinedProgramActivitySchedules = null;
//     _context.LoadStoredProc("dbo.sp_ScheduleAggregateData")
//         .AddParam("Option", "GetUserDefinedActivityScheduleByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userDefinedProgramActivitySchedules = r.ToList<UserActivityScheduleViewModel>());

//     //itemVM.UserDefinedProgramActivitySchedules = userDefinedProgramActivitySchedules;

//     // Activity Actions
//     List<UserActivityActionScheduleViewModel> userDefinedProgramActivityActionSchedules = null;
//     _context.LoadStoredProc("dbo.sp_Schedule_Activity_Data")
//         .AddParam("Option", "GetUserDefProgramActivityActionsByUserId")
//         .AddParam("UserId", id)
//         .Exec(r => userDefinedProgramActivityActionSchedules = r.ToList<UserActivityActionScheduleViewModel>());


//     if (userDefinedProgramActivityActionSchedules != null)
//     {

//         Log.Debug($"User Def Activity Actions Found: {userDefinedProgramActivityActionSchedules.Count}");

//         foreach (var activity in userDefinedProgramActivitySchedules)
//         {
//             List<UserActivityActionScheduleViewModel> actionList = new List<UserActivityActionScheduleViewModel>();

//             foreach (var activityAction in userDefinedProgramActivityActionSchedules)
//             {
//                 Log.Debug($"Adding User Def Activity Action Name: {activityAction.Name}");
//                 Log.Debug($"Adding User Def Activity Action Id: {activityAction.Id}");
//                 Log.Debug($"Adding User Def Activity Id: {activity.ActivityId}");
//                 if (activityAction.ActivityId == activity.ActivityId)
//                 {
//                     actionList.Add(activityAction);
//                     Log.Debug($"Adding User Def Activity Action: {activityAction.Name}");
//                 }
//             }

//             if (actionList != null)
//             {
//                 activity.ActivityActions = actionList;
//             }

//             // var itemVM = Mapper.Map<ScheduleEntityViewModel>(item);
//             // updateEntityWithTypeInfo(ref itemVM, entityType, entityTypeValue); 

//             // entityList.Add(itemVM);
//             // counter++;
//         }
//     }

//     itemVM.UserDefinedProgramActivitySchedules = userDefinedProgramActivitySchedules;

//     return itemVM;
// } 