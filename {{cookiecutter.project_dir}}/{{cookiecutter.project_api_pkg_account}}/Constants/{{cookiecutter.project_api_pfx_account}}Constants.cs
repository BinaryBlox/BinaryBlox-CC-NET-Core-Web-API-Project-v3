#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_account}}.Constants
{
    // TODO: Eventually replace with appSettings
    public class ApiEndpointConstants
    { 
        // {{cookiecutter.project_api_name_account}} - Endpoints
        public const string {{cookiecutter.project_api_const_pfx_account|upper}}_ENDPOINT = "https://binaryblox-account-api.azurewebsites.net";
        public const string {{cookiecutter.project_api_const_pfx_account|upper}}_ENDPOINT_REDIRECT_ENDPOINT= "https://binaryblox-account-api.azurewebsites.net/oauth2-redirect.htm";
        public const string {{cookiecutter.project_api_const_pfx_account|upper}}_ENDPOINT_DEV = "http://localhost:5002";
        public const string {{cookiecutter.project_api_const_pfx_account|upper}}_REDIRECT_ENDPOINT_DEV  = "http://localhost:5002/oauth2-redirect.htm";
    } 
}