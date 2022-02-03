using Microsoft.AspNetCore.Identity;

using AutoMapper;

using BinaryBlox.SDK.Account.Controllers;
using BinaryBlox.SDK.Account.ViewModels; 
using BinaryBlox.SDK.Data.Models.Application;
using BinaryBlox.SDK.Data.Models.Identity;
using BinaryBlox.SDK.Data.Models.Permission;
using BinaryBlox.SDK.Data.ViewModels.Account;
using BinaryBlox.SDK.Identity.Authorization;

namespace {{cookiecutter.project_api_pkg_account}}.Microservice.Mapper
{
     /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore());

            CreateMap<ApplicationUser, UserEditViewModel>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserEditViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore());

            CreateMap<ApplicationUser, UserPatchViewModel>()
                .ReverseMap();

            CreateMap<ApplicationRole, RoleViewModel>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims))
                .ForMember(d => d.UsersCount, map => map.MapFrom(s => s.Users != null ? s.Users.Count : 0))
                .ReverseMap();
            CreateMap<RoleViewModel, ApplicationRole>();

            CreateMap<IdentityRoleClaim<string>, ClaimViewModel>()
                .ForMember(d => d.Type, map => map.MapFrom(s => s.ClaimType))
                .ForMember(d => d.Value, map => map.MapFrom(s => s.ClaimValue))
                .ReverseMap();

            CreateMap<ApplicationPermission, PermissionViewModel>()
                .ReverseMap();

            // TODO:
            // CreateMap<IdentityRoleClaim<string>, PermissionViewModel>()
            //     .ConvertUsing(s => Mapper.Map<PermissionViewModel>(ApplicationPermissions.GetPermissionByValue(s.ClaimValue)));

            // BxClientApplication
            CreateMap<BxClientApplicationViewModel, BxClientApplication>()
                .ReverseMap();
            CreateMap<BxClientApplication, BxClientApplicationViewModel>()
                .ReverseMap();

            // BxClientApplicationUserMap
            CreateMap<BxClientApplicationUserMapViewModel, BxClientApplicationUserMap>()
                .ReverseMap();
            CreateMap<BxClientApplicationUserMap, BxClientApplicationUserMapViewModel>()
                .ReverseMap();

            // BxAccountUserViewModel
            CreateMap<ApplicationUser, BxAccountUserViewModel>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<BxAccountUserViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore());


            /*************************************
            * Identity Client Entities
            *************************************/
            
            // BxIdentityClientViewModel
            CreateMap<BxIdentityClientViewModel, BxIdentityClient>()
                .ForMember(d => d.IdentityProviderRestrictions, map => map.Ignore())
                .ForMember(d => d.Claims, map => map.Ignore())
                .ForMember(d => d.AllowedCorsOrigins, map => map.Ignore())
                .ForMember(d => d.Properties, map => map.Ignore())
                .ForMember(d => d.AllowedScopes, map => map.Ignore())
                .ForMember(d => d.ClientSecrets, map => map.Ignore())
                .ForMember(d => d.RedirectUris, map => map.Ignore())
                .ForMember(d => d.PostLogoutRedirectUris, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClient, BxIdentityClientViewModel>()
                .ReverseMap();

            // BxIdentityClientCorsOriginViewModel
            CreateMap<BxIdentityClientCorsOriginViewModel, BxIdentityClientCorsOrigin>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientCorsOrigin, BxIdentityClientCorsOriginViewModel>()
                .ReverseMap();

            // BxIdentityClientClaimViewModel
            CreateMap<BxIdentityClientClaimViewModel, BxIdentityClientClaim>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientClaim, BxIdentityClientClaimViewModel>()
                .ReverseMap();

            // BxIdentityClientPropertyViewModel
            CreateMap<BxIdentityClientPropertyViewModel, BxIdentityClientProperty>()
            .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientProperty, BxIdentityClientPropertyViewModel>()
                .ReverseMap();

            // BxIdentityClientScopeViewModel
            CreateMap<BxIdentityClientScopeViewModel, BxIdentityClientScope>()
            .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientScope, BxIdentityClientScopeViewModel>()
                .ReverseMap();

            // BxIdentityClientSecretViewModel
            CreateMap<BxIdentityClientSecretViewModel, BxIdentityClientSecret>()
            .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientSecret, BxIdentityClientSecretViewModel>()
                .ReverseMap();

            // BxIdentityClientRedirectUriViewModel
            CreateMap<BxIdentityClientRedirectUriViewModel, BxIdentityClientRedirectUri>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientRedirectUri, BxIdentityClientRedirectUriViewModel>()
                .ReverseMap();

            // BxIdentityClientPostLogoutRedirectUriViewModel
            CreateMap<BxIdentityClientPostLogoutRedirectUriViewModel, BxIdentityClientPostLogoutRedirectUri>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientPostLogoutRedirectUri, BxIdentityClientPostLogoutRedirectUriViewModel>()
                .ReverseMap();
 
            // BxIdentityClientGrantTypeViewModel
            CreateMap<BxIdentityClientGrantTypeViewModel, BxIdentityClientGrantType>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientGrantType, BxIdentityClientGrantTypeViewModel>()
                .ReverseMap();
 
            // BxIdentityClientIdPRestrictionViewModel
            CreateMap<BxIdentityClientIdPRestrictionViewModel, BxIdentityClientIdPRestriction>()
                .ForMember(d => d.Client, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityClientIdPRestriction, BxIdentityClientIdPRestrictionViewModel>()
                .ReverseMap();

            /*************************************
            * API Scope Entities
            *************************************/
            // BxApiScopeViewModel
            CreateMap<BxApiScopeViewModel, BxApiScope>()
                .ForMember(d => d.Properties, map => map.Ignore())
                .ForMember(d => d.UserClaims, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiScope, BxApiScopeViewModel>()
                .ReverseMap();

            // BxApiScopeClaimViewModel
            CreateMap<BxApiScopeClaimViewModel, BxApiScopeClaim>()
                .ForMember(d => d.Scope, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiScopeClaim, BxApiScopeClaimViewModel>()
                .ReverseMap();

            // BxApiScopePropertyViewModel
            CreateMap<BxApiScopePropertyViewModel, BxApiScopeProperty>()
            .ForMember(d => d.Scope, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiScopeProperty, BxApiScopePropertyViewModel>()
                .ReverseMap();


            /*************************************
            * Identity Resource Entities
            *************************************/
            // BxIdentityResourceViewModel
            CreateMap<BxIdentityResourceViewModel, BxIdentityResource>()
                .ForMember(d => d.Properties, map => map.Ignore())
                .ForMember(d => d.UserClaims, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityResource, BxIdentityResourceViewModel>()
                .ReverseMap();

            // BxIdentityResourceClaimViewModel
            CreateMap<BxIdentityResourceClaimViewModel, BxIdentityResourceClaim>()
                .ForMember(d => d.IdentityResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityResourceClaim, BxIdentityResourceClaimViewModel>()
                .ReverseMap();

            // BxIdentityResourcePropertyViewModel
            CreateMap<BxIdentityResourcePropertyViewModel, BxIdentityResourceProperty>()
            .ForMember(d => d.IdentityResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxIdentityResourceProperty, BxIdentityResourcePropertyViewModel>()
                .ReverseMap();

            /*************************************
            * API Resource Entities
            *************************************/
            // BxApiResourceViewModel
            CreateMap<BxApiResourceViewModel, BxApiResource>()
                .ForMember(d => d.Properties, map => map.Ignore())
                .ForMember(d => d.UserClaims, map => map.Ignore())
                .ForMember(d => d.Scopes, map => map.Ignore())
                .ForMember(d => d.Secrets, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiResource, BxApiResourceViewModel>()
                .ReverseMap();

            // BxApiResourceClaimViewModel
            CreateMap<BxApiResourceClaimViewModel, BxApiResourceClaim>()
                .ForMember(d => d.ApiResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiResourceClaim, BxApiResourceClaimViewModel>()
                .ReverseMap();

            // BxApiResourcePropertyViewModel
            CreateMap<BxApiResourcePropertyViewModel, BxApiResourceProperty>()
            .ForMember(d => d.ApiResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiResourceProperty, BxApiResourcePropertyViewModel>()
                .ReverseMap();

            // BxApiResourceScopeViewModel
            CreateMap<BxApiResourceScopeViewModel, BxApiResourceScope>()
            .ForMember(d => d.ApiResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiResourceScope, BxApiResourceScopeViewModel>()
                .ReverseMap();

            // BxApiResourceSecretViewModel
            CreateMap<BxApiResourceSecretViewModel, BxApiResourceSecret>()
            .ForMember(d => d.ApiResource, map => map.Ignore())
                .ReverseMap();
            CreateMap<BxApiResourceSecret, BxApiResourceSecretViewModel>()
                .ReverseMap();

            /*************************************
            * Application Roles
            *************************************/
            //     // BxApiResourceSecretViewModel
            // CreateMap<BxApplicationRoleViewModel, ApplicationRole>()
            // // .ForMember(d => d.ApiResource, map => map.Ignore())
            //     .ReverseMap();
            // CreateMap<ApplicationRole, BxApplicationRoleViewModel>()
            //     .ReverseMap();

            /*************************************
            * Permission Roles
            *************************************/
            CreateMap<BxPermissionEntityViewModel, BxPermEntity>()  
                .ReverseMap();
            CreateMap<BxPermEntity, BxPermissionEntityViewModel>()
                .ReverseMap();

        }
    }
}
