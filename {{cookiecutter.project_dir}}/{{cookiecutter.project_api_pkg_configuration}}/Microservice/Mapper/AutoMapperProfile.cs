using AutoMapper;

using BinaryBlox.SDK.Configuration.ViewModels;
using BinaryBlox.SDK.Data.Models.Configuration;  
 
using {{cookiecutter.project_api_pkg_configuration}}.Microservice.V1.ViewModels;

namespace {{cookiecutter.project_api_pkg_configuration}}.Microservice.Mapper
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

            // BxAttribAccessPriority
            CreateMap<BxAttribAccessPriorityViewModel, BxAttribAccessPriority>()
                .ReverseMap();
            CreateMap<BxAttribAccessPriority, BxAttribAccessPriorityViewModel>()
                .ReverseMap();

            // BxAttribTemplate
            CreateMap<BxAttribTemplateViewModel, BxAttribTemplate>()
                .ReverseMap();
            CreateMap<BxAttribTemplate, BxAttribTemplateViewModel>()
                .ReverseMap();

            // BxAttribValType
            CreateMap<BxAttribValTypeViewModel, BxAttribValType>()
                .ReverseMap();
            CreateMap<BxAttribValType, BxAttribValTypeViewModel>()
                .ReverseMap();

            // BxConfiguration
            CreateMap<BxConfigurationViewModel, BxConfiguration>()
                .ReverseMap();
            CreateMap<BxConfiguration, BxConfigurationViewModel>()
                .ReverseMap();

            // BxConfigurationAccessPriority
            CreateMap<BxConfigurationAccessPriorityViewModel, BxConfigurationAccessPriority>()
                .ReverseMap();
            CreateMap<BxConfigurationAccessPriority, BxConfigurationAccessPriorityViewModel>()
                .ReverseMap();

            // BxConfigurationAttribVal
            CreateMap<BxConfigurationAttribValViewModel, BxConfigurationAttribVal>()
                .ReverseMap();
            CreateMap<BxConfigurationAttribVal, BxConfigurationAttribValViewModel>()
                .ReverseMap();

            // BxConfigurationAttribValOverride
            CreateMap<BxConfigurationAttribValOverrideViewModel, BxConfigurationAttribValOverride>()
                .ReverseMap();
            CreateMap<BxConfigurationAttribValOverride, BxConfigurationAttribValOverrideViewModel>()
                .ReverseMap();

            // BxConfigurationMedia
            CreateMap<BxConfigurationMediaViewModel, BxConfigurationMedia>()
                .ReverseMap();
            CreateMap<BxConfigurationMedia, BxConfigurationMediaViewModel>()
                .ReverseMap();

            // BxConfigurationTemplate
            CreateMap<BxConfigurationTemplateViewModel, BxConfigurationTemplate>()
                .ReverseMap();
            CreateMap<BxConfigurationTemplate, BxConfigurationTemplateViewModel>()
                .ReverseMap();

            // BxConfigurationType
            CreateMap<BxConfigurationTypeViewModel, BxConfigurationType>()
                .ReverseMap();
            CreateMap<BxConfigurationType, BxConfigurationTypeViewModel>()
                .ReverseMap();

            // BxPriorityRank
            CreateMap<BxPriorityRankViewModel, BxPriorityRank>()
                .ReverseMap();
            CreateMap<BxPriorityRank, BxPriorityRankViewModel>()
                .ReverseMap();

            //****************************************
            // Custom Objects (not one-to-one entity mappings)
            //****************************************
            
            // BxConfigurationAggregate
            CreateMap<BxConfigurationAggregateViewModel, BxConfigurationResult>()
                .ReverseMap();
            CreateMap<BxConfigurationResult, BxConfigurationAggregateViewModel>()
                .ReverseMap(); 
        }
    }
}
