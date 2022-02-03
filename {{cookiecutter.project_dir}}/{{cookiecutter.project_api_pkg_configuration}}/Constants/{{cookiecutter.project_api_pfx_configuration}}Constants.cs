#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_configuration}}.Constants
{

    public class ApiEndpointConstants
    { 
        // {{cookiecutter.project_api_name_configuration}} - Endpoints
        public const string {{cookiecutter.project_api_const_pfx_configuration|upper}}_ENDPOINT = "https://binaryblox-account-api.azurewebsites.net";
        public const string {{cookiecutter.project_api_const_pfx_configuration|upper}}_ENDPOINT_REDIRECT_ENDPOINT= "https://binaryblox-account-api.azurewebsites.net/oauth2-redirect.htm";
        public const string {{cookiecutter.project_api_const_pfx_configuration|upper}}_ENDPOINT_DEV = "http://localhost:5003";
        public const string {{cookiecutter.project_api_const_pfx_configuration|upper}}_REDIRECT_ENDPOINT_DEV  = "http://localhost:5003/oauth2-redirect.htm";
    } 
     
}