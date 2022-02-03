#pragma warning disable 1591
namespace {{cookiecutter.project_spa_pkg}}.Constants
{

    public class ApiEndpointConstants
    { 
        // {{cookiecutter.project_api_name_configuration}} - Endpoints
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_ENDPOINT = "https://binaryblox-account-api.azurewebsites.net";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_ENDPOINT_REDIRECT_ENDPOINT= "https://binaryblox-account-api.azurewebsites.net/oauth2-redirect.htm";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_ENDPOINT_DEV = "http://localhost:3000";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_REDIRECT_ENDPOINT_DEV  = "http://localhost:3000/oauth2-redirect.htm";
    }

    public class ApiResourceConstants
    { 
        // {{cookiecutter.project_api_name_configuration}}  - Resources
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_KEY = "{{cookiecutter.project_spa_const_pfx|lower}}";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_NAME = "{{cookiecutter.project_api_name_configuration}}";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_SCOPE_KEY = "{{cookiecutter.project_spa_const_pfx|lower}}.admin";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_SCOPE_NAME= "{{cookiecutter.project_api_name_configuration}} - Full Access";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_RO_SCOPE_KEY = "{{cookiecutter.project_spa_const_pfx|lower}}.read_only";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_RO_SCOPE_NAME = "{{cookiecutter.project_api_name_configuration}} - Read Only";
 
    }

    public class ApiClientConstants
    { 
        // {{cookiecutter.project_api_name_configuration}}  - Clients
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_CLIENT_ID = "{{cookiecutter.project_spa_const_pfx|lower}}.client";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_CLIENT_ID_SWAGGER = "{{cookiecutter.project_spa_const_pfx|lower}}.swagger.client";
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_CLIENT_SECRET = "secret";
  
    }

    public class ApiScopeConstants
    {
        
        // {{cookiecutter.project_api_name_configuration}}  - Scopes
        public const string {{cookiecutter.project_spa_const_pfx|upper}}_SCOPE = "Client-{{cookiecutter.project_spa_const_pfx|upper}}";
    }
     
}