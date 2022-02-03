using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using BinaryBlox.SDK.Data.Interfaces.Context;
using BinaryBlox.SDK.Data.Interfaces.Entity;
using BinaryBlox.SDK.Data.Models.Configuration;
using BinaryBlox.SDK.Identity.Interfaces;

using Serilog;

namespace {{cookiecutter.project_api_pkg_configuration}}.DAL.Configuration 
{
 
   
    /// <summary>
    /// 
    /// </summary>
    public class BxConfigurationDataInitializer : IBxDataInitializer
    {

        private readonly IConfiguration _configuration;
        private readonly BxApplicationDbContext _context;
        private readonly BxConfigurationIdentityDbContext _identityDbContext;
        private readonly IAccountManager _accountManager;

        private bool _truncateOnSeed;
        private bool _bypassIfSeeded;

        private IBxEntityType<Guid> _globalPriorityRank;
        private IBxEntityType<Guid> _regionalPriorityRank;
        private IBxEntityType<Guid> _locationPriorityRank;
        private IBxEntityType<Guid> _userPriorityRank;
        private IBxEntityType<Guid> _stringAttribValType;
        private IBxEntityType<Guid> _intAttribValType;
        private IBxEntityType<Guid> _urlAttribValType;
        private IBxEntityType<Guid> _colorAttribValType;
        private IBxEntityType<Guid> _debugAttribValType;
        private IBxEntityType<Guid> _compTypeBanner;
        private IBxEntityContent<Guid> _defaultConfigurationTemplate;
        private IBxEntityContent<Guid> _defaultComponent;
        private IBxEntityContent<Guid> _attribTmpTitle;
        private IBxEntityContent<Guid> _attribTmpSubtitle;
        private IBxEntityContent<Guid> _attribTmpImage;
        private IBxEntityContent<Guid> _attribTmpAlertTitle;
        private IBxEntityContent<Guid> _attribTmpAlertLevel;
        private IBxEntity<Guid> _compAttribValTitle;
        private IBxEntity<Guid> _compAttribValSubtitle;
        private IBxEntity<Guid> _compAttribValAlertLevel;
        private IBxEntity<Guid> _attribSanFranciscoAccessPriorityRegion;
        private IBxEntity<Guid> _attribSacramentoAccessPriorityRegion;
        private IBxEntity<Guid> _attribUser1AccessPriorityUser;
        private IBxEntity<Guid> _attribLocationAccessPriorityLocation;
        private IBxEntity<Guid> _attribBayAreaAccessPriorityRegion;
        private IBxEntity<Guid> _attribSacAccessPriorityRegion;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityDbContext"></param>
        /// <param name="accountManager"></param>
        /// <param name="configuration"></param>
        public BxConfigurationDataInitializer(
            BxApplicationDbContext context,
            BxConfigurationIdentityDbContext identityDbContext,
            IAccountManager accountManager,
            IConfiguration configuration)
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _accountManager = accountManager;
            _configuration = configuration;

            _truncateOnSeed = _configuration.GetSection("DAL:TruncateOnSeed").Get<bool>();
            _bypassIfSeeded = _configuration.GetSection("DAL:BypassOnSeed").Get<bool>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {

            Log.Information($"START: Seeding Configuration API Data");

            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (_truncateOnSeed)
            {
                Log.Information($"PRELIM: Truncating Tables");
                await TruncateTables();
            }

            if (_bypassIfSeeded)
            {
                Log.Information($"BYPASS: Bypass on Seeding enabled.");
            }

            await CreateConfigurationData();

            Log.Information($"END: Seeding Configuration API Data");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        protected async Task TruncateTables()
        {

            if (await _context.BxAttribAccessPriority.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxAttribAccessPriority...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_attrib_access_priority");
            }


            if (await _context.BxAttribTemplate.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxAttribTemplate...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_attrib_template");

            }

            if (await _context.BxAttribValType.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxAttribValType...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_attrib_val_type");

            }

            if (await _context.BxConfigurationAccessPriority.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationAccessPriority...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_access_priority");

            }

            if (await _context.BxConfigurationAttribVal.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationAttribVal...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_attrib_val");

            }

            if (await _context.BxConfigurationAttribValOverride.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationAttribValOverride...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_attrib_val_override");

            }

            if (await _context.BxConfiguration.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfiguration...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration");

            } 

            if (await _context.BxConfigurationMedia.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationMedia...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_media");

            }

            if (await _context.BxConfigurationTemplate.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationTemplate...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_template");

            }

            if (await _context.BxConfigurationType.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxConfigurationType...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_configuration_type");

            }

            if (await _context.BxPriorityRank.AnyAsync())
            {
                Log.Information($"Truncating: Truncating BxPriorityRank...");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bx_priority_rank");
            }

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// IMPORTANT: This method cannot be in the base class because base context classes are different and will not serialize.
        /// </summary>
        /// <returns></returns>
        protected async Task CreateConfigurationData()
        {
            // Return if Configuration is invalid.
            if (_configuration == null)
            {
                Log.Error("Configuration is NULL");
                return;
            }

            // Only seed ApplicationDbContext
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            try
            {
                await InitBxEntityTypeData();

                await InitBxEntityConfigTemplateData();
            }
            catch (System.Exception error)
            {
                Log.Error(error, "Failed to seed BxConfiguration Data");
                return;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        protected async Task InitBxEntityTypeData()
        {
            Log.Information($"Seeding: Intializing Bx Entity type data...");
            // TODO: Checks to see if we want to override data.
            await CreateBxAttribValTypeData();
            await CreateBxConfigurationTypeData();
            await CreateBxPriorityRankData();
            await CreateBxAttribAccessPriorityTypeData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task InitBxEntityConfigTemplateData()
        {
            Log.Information($"Seeding: Intializing Bx Configuration type data...");

            // TODO: Checks to see if we want to override data.
            // Template data
            await CreateBxConfigurationTemplateData();
            await CreateBxAttribTemplateData();

            // Entity data
            await CreateBxConfigurationData();
            await CreateBxConfigurationAttribValData();
            await CreateBxConfigurationAttribValOverrideData();
            await CreateBxConfigurationAccessPriorityData();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxAttribValTypeData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxAttribValType.AnyAsync())
                {
                    Log.Information($"Bypass:  BxAttribValType data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxAttribValType data...");
 
            try
            {
                BxAttribValType entity;
                entity = (BxAttribValType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribValType(),
                    "string_type", "String Value", "String value Type");
                _context.BxAttribValType.Add(entity);
                _context.SaveChanges();
                this._stringAttribValType = entity;

                entity = (BxAttribValType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribValType(),
                        "int_type", "Integer Value", "Integer value Type");
                entity.Constraints = "{\"validation\":[\"type\":\"integer\"]}";
                _context.BxAttribValType.Add(entity);
                _context.SaveChanges();
                this._intAttribValType = entity;

                entity = (BxAttribValType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribValType(),
                        "color_type", "Color(Hex) Value", "Color(Hex) Value Type");
                entity.Constraints = "{\"validation\":[\"type\":\"color\"]}";
                _context.BxAttribValType.Add(entity);
                _context.SaveChanges();
                this._colorAttribValType = entity;

                entity = (BxAttribValType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribValType(),
                        "debug_type", "Debug Type", "Color(Hex) Value Type");
                entity.Constraints = "{\"validation\":[\"type\":\"debug\"]}";
                _context.BxAttribValType.Add(entity);
                _context.SaveChanges();
                this._debugAttribValType = entity;

                entity = (BxAttribValType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribValType(),
                        "url_type", "Url Type Value", "Url Type Value");
                _context.BxAttribValType.Add(entity);
                _context.SaveChanges();
                this._urlAttribValType = entity;

                Log.Information($"Completed: Creating BxAttribValType data...");
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxAttribValTypeData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationTypeData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxConfigurationType.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfigurationType data already exists...");
                    return;
                }
            }


            Log.Information($"Seeding: Creating BxConfigurationType data..."); 

            try
            {
                BxConfigurationType entity;

                entity = (BxConfigurationType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationType(),
                    "comp_banner", "Banner Component Category", "All banner type components.");
                _context.BxConfigurationType.Add(entity);
                _context.SaveChanges();
                this._compTypeBanner = entity;

                entity = (BxConfigurationType)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationType(),
                        "comp_datagrid", "Datagrid Component Category", "All Datagrid type components.");
                _context.BxConfigurationType.Add(entity);
                _context.SaveChanges(); 
            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationTypeData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxPriorityRankData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxPriorityRank.AnyAsync())
                {
                    Log.Information($"Bypass:  BxPriorityRank data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxPriorityRank data..."); 

            try
            {
                BxPriorityRank entity;

                entity = (BxPriorityRank)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxPriorityRank(),
                "global", "Global Priority Rank", "Global Priority Rank(Default) lowest rank.");
                entity.PriorityRank = 0;
                _context.BxPriorityRank.Add(entity);
                _context.SaveChanges();
                this._globalPriorityRank = entity;

                entity = (BxPriorityRank)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxPriorityRank(),
                        "region", "Region Display Priority Rank", "Display only for specified Region");
                entity.PriorityRank = 3;
                _context.BxPriorityRank.Add(entity);
                _context.SaveChanges();
                this._regionalPriorityRank = entity;

                entity = (BxPriorityRank)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxPriorityRank(),
                        "location", "Location Display Priority Rank", "Display only for specified Location");
                entity.PriorityRank = 2;
                _context.BxPriorityRank.Add(entity);
                _context.SaveChanges();
                this._locationPriorityRank = entity;

                entity = (BxPriorityRank)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxPriorityRank(),
                        "user", "User ID Display Priority Rank", "Display only for specified User ID");
                entity.PriorityRank = 1;
                _context.BxPriorityRank.Add(entity);
                _context.SaveChanges();
                this._userPriorityRank = entity;
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "BxPriorityRank method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxAttribAccessPriorityTypeData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxAttribAccessPriority.AnyAsync())
                {
                    Log.Information($"Bypass:  BxAttribAccessPriority data already exists...");
                    return;
                }
            }


            Log.Information($"Seeding: Creating BxAttribAccessPriority data..."); 

            try
            {
                BxAttribAccessPriority entity;

                entity = (BxAttribAccessPriority)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribAccessPriority(),
                   "reg_1", "Bay Area Region Display Priority", "Display attribute only for specified Region");
                entity.PriorityRankId = this._regionalPriorityRank.Id;
                _context.BxAttribAccessPriority.Add(entity);
                _context.SaveChanges();
                this._attribSanFranciscoAccessPriorityRegion = entity;

                entity = (BxAttribAccessPriority)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribAccessPriority(),
                  "reg_2", "Sacramento Region Display Priority", "Display attribute only for specified Region");
                entity.PriorityRankId = this._regionalPriorityRank.Id;
                _context.BxAttribAccessPriority.Add(entity);
                _context.SaveChanges();
                this._attribSacramentoAccessPriorityRegion = entity;

                entity = (BxAttribAccessPriority)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribAccessPriority(),
                  "loc_1", "IceBlocks Display Priority", "Display attribute only for specified Facility");
                entity.PriorityRankId = this._locationPriorityRank.Id;
                _context.BxAttribAccessPriority.Add(entity);
                _context.SaveChanges();
                this._attribLocationAccessPriorityLocation = entity;

                entity = (BxAttribAccessPriority)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribAccessPriority(),
                  "ahhenderson", "User Display Priority", "Display attribute only for specified User");
                entity.PriorityRankId = this._userPriorityRank.Id;
                _context.BxAttribAccessPriority.Add(entity);
                _context.SaveChanges();
                this._attribUser1AccessPriorityUser = entity;
 
            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxAttribAccessPriorityTypeData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationTemplateData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxConfigurationTemplate.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfigurationTemplate data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxConfigurationType data..."); 

            try
            {
                BxConfigurationTemplate entity;

                entity = (BxConfigurationTemplate)this.UpdateXpEntityContentData((IBxEntityContent<Guid>)new BxConfigurationTemplate(),
                     "Generic Alert Banner", "Generic Alert Banner Component");
                entity.ConfigurationTypeId = this._compTypeBanner.Id;
                entity.IsEnabled = true;
                _context.BxConfigurationTemplate.Add(entity);
                _context.SaveChanges();

                this._defaultConfigurationTemplate = entity;
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationTemplateData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxAttribTemplateData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxAttribTemplate.AnyAsync())
                {
                    Log.Information($"Bypass:  BxAttribTemplate data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxAttribTemplate data...");
 

            try
            {
                List<BxAttribTemplate> entities = new List<BxAttribTemplate>();
                BxAttribTemplate entity;

                // Single Flush entities to get ID for later reference
                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "title", "Banner Title", "Banner Title");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "Generic Alert Banner Title";
                _context.BxAttribTemplate.Add(entity);
                _context.SaveChanges();
                this._attribTmpTitle = entity;

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "sub_title", "Banner Subtitle", "Banner Subtitle");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "Generic Alert Banner Subtitle";
                _context.BxAttribTemplate.Add(entity);
                _context.SaveChanges();
                this._attribTmpSubtitle = entity;

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "image", "Banner Image", "Banner Image URL");
                entity.AttribValueTypeId = this._urlAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "1";
                //entity.ConfigurationMediaId;
                entity.Options = "{\"options\": [\"componentMediaKey\", \"local\", \"secureUrl\", \"nonSecureUrl\"]}";
                _context.BxAttribTemplate.Add(entity);
                _context.SaveChanges();
                this._attribTmpImage = entity;

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "alert_title", "Banner Alert Title", "Banner Alert Title");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "Generic Alert Banner Alert Title";
                _context.BxAttribTemplate.Add(entity);
                _context.SaveChanges();
                this._attribTmpAlertTitle = entity;

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "alert_level", "Banner Alert Level", "Banner Alert Level");
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.AttribValueTypeId = this._debugAttribValType.Id;
                entity.CanOverride = true;
                entity.Value = "error";
                entity.Options = "{\"options\": [\"info\", \"error\", \"warn\", \"debug\", \"fatal\"]}";
                _context.BxAttribTemplate.Add(entity);
                _context.SaveChanges();
                this._attribTmpAlertLevel = entity;

                // Entities in list (ID not required for later reference)
                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "style", "Banner Alert Style", "Banner Alert Style");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "fullWidthLeft";
                entity.Options = "{\"options\": [\"fullWidthLeft\", \"fullWidthImage\", \"fullWidth\", \"popUp\", \"action\"]}";
                _context.BxAttribTemplate.Add(entity);

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                    "icon", "Alert Icon", "Banner Alert Icon");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "add.plus";
                _context.BxAttribTemplate.Add(entity);

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "icon_color", "Alert Icon Color", "Banner Alert Icon Color");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "FF00AA";
                _context.BxAttribTemplate.Add(entity);

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                        "text_style", "Banner Text Style", "Banner Text Style");
                entity.AttribValueTypeId = this._stringAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "none";
                entity.Options = "{\"options\": [\"bold\", \"italic\", \"none\"]}";
                _context.BxAttribTemplate.Add(entity);

                entity = (BxAttribTemplate)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxAttribTemplate(),
                    "text_color", "Banner Text Color", "Banner Text Color");
                entity.AttribValueTypeId = this._colorAttribValType.Id;
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.CanOverride = true;
                entity.Value = "000000";
                _context.BxAttribTemplate.Add(entity);

                // Save changes for all the above at once.
                _context.SaveChanges();
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxAttribTemplateData method failed.");
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxConfiguration.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfiguration data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxConfigurationType data..."); 

            try
            {
                BxConfiguration entity;

                entity = (BxConfiguration)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfiguration(),
                    "LPDBA", "Landing Page Alert Dashboard Banner", "Landing Page Alert Dashboard Banner");
                entity.ConfigurationTemplateId = this._defaultConfigurationTemplate.Id;
                entity.IsEnabled = true;

                _context.BxConfiguration.Add(entity);
                _context.SaveChanges();

                this._defaultComponent = entity; 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationAttribValData()
        {

            if (_bypassIfSeeded)
            {
                if (await _context.BxConfigurationAttribVal.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfigurationAttribVal data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxConfigurationAttribVal data...");
 

            try
            {
                BxConfigurationAttribVal entity;


                // Single Add to get ID for overrides 
                entity = (BxConfigurationAttribVal)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribVal(),
                    "", "Landing Page Dashboard now available", "");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.AttribTemplateId = this._attribTmpTitle.Id;
                entity.Value = "Landing Page Dashboard now available";
                _context.BxConfigurationAttribVal.Add(entity);
                _context.SaveChanges();
                this._compAttribValTitle = entity;

                entity = (BxConfigurationAttribVal)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribVal(),
                    "", "Information", "");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.AttribTemplateId = this._attribTmpSubtitle.Id;
                entity.Value = "Information";
                _context.BxConfigurationAttribVal.Add(entity);
                _context.SaveChanges();
                this._compAttribValSubtitle = entity;

                entity = (BxConfigurationAttribVal)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribVal(),
                    "", "Info", "");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.AttribTemplateId = this._attribTmpAlertLevel.Id;
                entity.Value = "info";
                entity.Options = ((BxAttribTemplate)this._attribTmpAlertLevel).Options;
                _context.BxConfigurationAttribVal.Add(entity);
                _context.SaveChanges();
                this._compAttribValAlertLevel = entity;

                // List Adds 
                entity = (BxConfigurationAttribVal)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribVal(),
                    "", "Image Title", "");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.AttribTemplateId = this._attribTmpImage.Id;
                entity.Value = "Image Title";
                //entity.ConfigurationMediaId = null;
                _context.BxConfigurationAttribVal.Add(entity);

                entity = (BxConfigurationAttribVal)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribVal(),
                    "", "Landing Page Alert", "");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.AttribTemplateId = this._attribTmpAlertTitle.Id;
                entity.Value = "Landing Page Alert";
                _context.BxConfigurationAttribVal.Add(entity);

                // Save all changes
                _context.SaveChanges();
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationAttribValData method failed.");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationAttribValOverrideData()
        {
            if (_bypassIfSeeded)
            {
                if (await _context.BxConfigurationAttribValOverride.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfigurationAttribValOverride data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxConfigurationAttribValOverride data..."); 

            try
            {
                BxConfigurationAttribValOverride entity;

                // Single Add to get ID for overrides
                // Region Override
                entity = (BxConfigurationAttribValOverride)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribValOverride(),
                    "", "DASHBOARD 1", "");
                entity.ConfigurationAttribValId = this._compAttribValTitle.Id;
                entity.AttribAccessPriorityId = this._attribSanFranciscoAccessPriorityRegion.Id;
                entity.Value = "Bay Area Landing Page Dashboard now available";
                entity.Options = ((BxConfigurationAttribVal)this._compAttribValTitle).Options;
                _context.BxConfigurationAttribValOverride.Add(entity);

                entity = (BxConfigurationAttribValOverride)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribValOverride(),
                  "", "DASHBOARD 2", "");
                entity.ConfigurationAttribValId = this._compAttribValAlertLevel.Id;
                entity.AttribAccessPriorityId = this._attribSanFranciscoAccessPriorityRegion.Id;
                entity.Value = "warn";
                entity.Options = ((BxConfigurationAttribVal)this._compAttribValAlertLevel).Options;
                _context.BxConfigurationAttribValOverride.Add(entity);

                entity = (BxConfigurationAttribValOverride)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribValOverride(),
                  "", "DASHBOARD 3", "");
                entity.ConfigurationAttribValId = this._compAttribValTitle.Id;
                entity.AttribAccessPriorityId = this._attribSacramentoAccessPriorityRegion.Id;
                entity.Value = "Sacramento Landing Page Dashboard now available";
                entity.Options = ((BxConfigurationAttribVal)this._compAttribValTitle).Options;
                _context.BxConfigurationAttribValOverride.Add(entity);

                // Facilty Override
                entity = (BxConfigurationAttribValOverride)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribValOverride(),
                  "", "DASHBOARD 4", "");
                entity.ConfigurationAttribValId = this._compAttribValTitle.Id;
                entity.AttribAccessPriorityId = this._attribLocationAccessPriorityLocation.Id;
                entity.Value = "IceBlocks Location Landing Page Dashboard now available";
                entity.Options = ((BxConfigurationAttribVal)this._compAttribValTitle).Options;
                _context.BxConfigurationAttribValOverride.Add(entity);

                // User Override
                entity = (BxConfigurationAttribValOverride)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAttribValOverride(),
                   "", "DASHBOARD 5", "");
                entity.ConfigurationAttribValId = this._compAttribValTitle.Id;
                entity.AttribAccessPriorityId = this._attribUser1AccessPriorityUser.Id;
                entity.Value = "Tony's Landing Page Dashboard now available";
                entity.Options = ((BxConfigurationAttribVal)this._compAttribValTitle).Options;
                _context.BxConfigurationAttribValOverride.Add(entity);

                // Save all changes
                _context.SaveChanges(); 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationAttribValOverrideData method failed.");
                return;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task CreateBxConfigurationAccessPriorityData()
        {
            if (_bypassIfSeeded)
            {
                if (await _context.BxConfigurationAccessPriority.AnyAsync())
                {
                    Log.Information($"Bypass:  BxConfigurationAccessPriority data already exists...");
                    return;
                }
            }

            Log.Information($"Seeding: Creating BxConfigurationAccessPriority data..."); 

            try
            {
                BxConfigurationAccessPriority entity;

                entity = (BxConfigurationAccessPriority)this.UpdateXpEntityTypeData((IBxEntityType<Guid>)new BxConfigurationAccessPriority(),
                    "reg_1", "Bay Area Access", "Access only for Bay Area Region");
                entity.ConfigurationId = this._defaultComponent.Id;
                entity.PriorityRankId = this._regionalPriorityRank.Id;
                _context.BxConfigurationAccessPriority.Add(entity);
                _context.SaveChanges();
 

            }
            catch (System.Exception error)
            {
                Log.Error(error, "CreateBxConfigurationAccessPriorityData method failed.");
                return;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        protected IBxEntityType<Guid> UpdateXpEntityTypeData(IBxEntityType<Guid> entity, String code, String name, String description)
        {
            entity.Code = code;
            entity.Name = name;
            entity.Description = description;

            return entity;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param> 
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        protected IBxEntityContent<Guid> UpdateXpEntityContentData(IBxEntityContent<Guid> entity, String name, String description)
        {

            entity.Name = name;
            entity.Description = description;

            return entity;
        }
    }
}
#pragma warning restore 1591