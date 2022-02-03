using System;
using System.Collections.Generic;

using BinaryBlox.SDK.Data.Models.Configuration;
using BinaryBlox.SDK.Data.ViewModels.Entity;

#pragma warning disable 1591
namespace {{cookiecutter.project_api_pkg_configuration}}.Microservice.V1.ViewModels
{ 
    public class BxConfigurationAggregateViewModel : BxEntityDto<Guid>
    {
        //  public BxConfigurationAggregateViewModel() : base() { 


        //  }
        public Guid ConfigId { get; set; }
        public Guid GroupId { get; set; }
        public List<BxConfigurationResultAttribData> Attributes { get; set; } = new List<BxConfigurationResultAttribData>();
    }
}
#pragma warning restore 1591


