#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.Identity.Authorization
{     
  public class ApiPolicies
    {
        ///<summary>Policy to allow viewing all user {{cookiecutter.project_api_name_account}} records.</summary>
        public const string ViewAllApiUsersPolicy = "View All Users";

        ///<summary>Policy to allow adding, removing and updating all {{cookiecutter.project_api_name_account}} user records.</summary>
        public const string ManageAllApiUsersPolicy = "Manage All Users";

        /// <summary>Policy to allow viewing details of all roles.</summary>
        public const string ViewAllApiRolesPolicy = "View All Roles";

        /// <summary>Policy to allow viewing details of all or specific {{cookiecutter.project_api_name_account}} roles (Requires roleName as parameter).</summary>
        public const string ViewApiRoleByApiRoleNamePolicy = "View Role by RoleName";

        /// <summary>Policy to allow adding, removing and updating all {{cookiecutter.project_api_name_account}} roles.</summary>
        public const string ManageAllApiRolesPolicy = "Manage All Roles";

        /// <summary>Policy to allow assigning {{cookiecutter.project_api_name_account}} roles the user has access to (Requires new and current {{cookiecutter.project_api_name_account}} roles as parameter).</summary>
        public const string AssignAllowedApiRolesPolicy = "Assign Allowed Roles"; 
        
    }
 
    /// <summary>
    /// Operation Policy to allow adding, viewing, updating and deleting general or specific user {{cookiecutter.project_api_name_account}} records.
    /// </summary>
    public static class ApiAccountManagementOperations
    {
        public const string CreateOperationName = "Create";
        public const string ReadOperationName = "Read";
        public const string UpdateOperationName = "Update";
        public const string DeleteOperationName = "Delete";

        public static ApiUserAccountAuthorizationRequirement Create = new ApiUserAccountAuthorizationRequirement(CreateOperationName);
        public static ApiUserAccountAuthorizationRequirement Read = new ApiUserAccountAuthorizationRequirement(ReadOperationName);
        public static ApiUserAccountAuthorizationRequirement Update = new ApiUserAccountAuthorizationRequirement(UpdateOperationName);
        public static ApiUserAccountAuthorizationRequirement Delete = new ApiUserAccountAuthorizationRequirement(DeleteOperationName);
    }
}
