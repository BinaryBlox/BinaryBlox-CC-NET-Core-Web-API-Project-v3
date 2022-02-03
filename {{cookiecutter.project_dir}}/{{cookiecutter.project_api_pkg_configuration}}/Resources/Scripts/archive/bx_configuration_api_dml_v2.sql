
-- DROP TABLE IF EXISTS dbo.bx_attrib_access_priority; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_attrib_template; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_attrib_val_type;  
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_access_priority;
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_attrib_val; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_attrib_val_override
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_media; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_template; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_configuration_type; 
-- GO  
-- DROP TABLE IF EXISTS dbo.bx_priority_rank; 
-- GO  

/******************************
** File: xpComponentMgd_DML.sql
** Name: XpComponent Managment DML Script
** Desc: XpComponent Managment Programmability scripts(sprocs) 
** Auth: Tony Henderson
** Date: 5/22/2020
**************************
** Change History
**************************
** PR   Date        Author  Description 
** --   --------   -------   ------------------------------------
** 1    05/22/2020  Tony   Created
*******************************/

/******************************
** Test Data (Keep)
*******************************/
-- select xp.id, xp.name, xp.description from bx_configuration_template xp Where id =1
-- -- select * from bx_attrib_template x where x.component_template_id = 1
-- select x.code, x.name, x.description, x.value, x.options from bx_attrib_template x where x.component_template_id = 1

-- select xa.value  from bx_configuration_attrib_val xa where xa.component_id = 1
-- select xao.value, xao.component_Attrib_val_id  from bx_configuration_attrib_val_override xao 

/******************************
** Preparation
*******************************/
-- DROP PROCEDURE IF EXISTS bx_execute_sp.sp_getBxConfigurationData;  
-- GO  

-- DROP SCHEMA IF EXISTS bx_execute_sp; 
-- GO  

-- /******************************
-- ** Create
-- *******************************/
-- CREATE SCHEMA bx_execute_sp;
-- GO

DROP PROCEDURE IF EXISTS sp_getBxConfigurationData;  
GO  

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--// execute [bx_execute_sp].[sp_getBxConfigurationData] @option='GetAllBxConfigurationData', @regionId='scal', @locationId='cud', @userId='E283476'
--// execute bx_execute_sp.sp_getBxConfigurationData @option='GetBxConfigurationDataById', @regionId='ncal', @compId=NULL, @groupId= '00000000-0000-0000-0000-000000000000'

-- execute  sp_getBxConfigurationData @option='GetAllBxConfigurationData' 
-- ,@groupId= '00000000-0000-0000-0000-000000000000'
-- ,@regionId='reg_1'  
-- ,@locationId='loc_1'
-- ,@userId='ahhenderson'
 
-- CREATE PROCEDURE  bx_execute_sp.sp_getBxConfigurationData (
-- 			@option varchar(100) = 'GetAllBxConfigurationData', 
--             @regionId varchar(10) =  NULL,
--             @locationId varchar(10) =  NULL,
-- 			@userId varchar(10) = NULL,
--             @compId UNIQUEIDENTIFIER = NULL,
--             @groupId  UNIQUEIDENTIFIER = NULL
-- )

CREATE PROCEDURE  sp_getBxConfigurationData
        (
        @option varchar(100) = 'GetAllBxConfigurationData',
        @regionId varchar(20) =  NULL,
        @locationId varchar(20) =  NULL,
        @userId varchar(20) = NULL,
        @compId UNIQUEIDENTIFIER = NULL,
        @groupId  UNIQUEIDENTIFIER = NULL
)

AS

SET NOCOUNT ON


If @option ='GetAllBxConfigurationData' 
	BEGIN

        SELECT TOP 2000
                bx_c.id,
                bx_c.name as config_name,
                bx_c.description as config_desc,
                bx_c.is_enabled as config_is_enabled,
                bx_c.configuration_template_id as config_templ_id,
                CASE 
                WHEN bx_cap.name IS NULL THEN 'Global Access'
                ELSE bx_cap.name END access_req,
                CASE 
                WHEN LOWER(bx_cap.name) IS NOT NULL
                        AND LOWER(bx_cap.code) != LOWER(@regionId)
                        AND LOWER(bx_cap.code) != LOWER(@locationId) THEN 'false'
                ELSE 'true' END access,
                CASE 
                WHEN bx_cav.id IS NULL THEN 'temp_attrib'
                ELSE
                    CASE 
                        WHEN bx_ap.priority_rank IS NULL THEN 'config_attrib'
                        ELSE 'config_attrib_ovr' END 
                END attrib_origin,
                CASE 
                WHEN bx_cav.id IS NULL THEN '00000000-0000-0000-0000-000000000000'
                ELSE bx_cav.id
                END attrib_id,
                bx_ctav.id as attrib_templ_id,
                CASE 
                        WHEN bx_cav.sort_index IS NULL THEN 0
                        ELSE bx_cav.sort_index
                        END attrib_sort_idx,
                CASE 
                WHEN bx_cav.id IS NULL THEN bx_ctav.[value] 
                ELSE
                    CASE 
                        WHEN bx_ap.priority_rank IS NULL THEN bx_cav.[value]
                        ELSE bx_ap.value END 
                END attrib_val,
                bx_ctav.[name] as attrib_name,
                bx_ctav.[description] as attrib_desc,
                bx_vt.name as attrib_val_type,
                CASE 
                WHEN bx_cav.id IS NULL AND bx_cav.options IS NULL 
                THEN bx_ctav.[options] 
                ELSE bx_cav.[options] END attrib_options,
                bx_ctav.can_override as attrib_ovr_enabled,
                LOWER(bx_ap.access) as attrib_ovr_status,
                LOWER(bx_ap.code) as attrib_ovr_filter,
                bx_c.id as config_id,
                bx_ctp.name as config_type,
                bx_ctav.configuration_media_id as attrib_media_id,
                CASE 
                WHEN bx_c.configuration_group_id = NULL THEN 'default group'
                ELSE 'custom group' END config_group_id,
                bx_c.updated_date as config_updated_date

        FROM [dbo].[bx_configuration] bx_c

                --// Joins
                INNER JOIN [dbo].[bx_configuration_template] bxct ON bx_c.configuration_template_id = bxct.id
                INNER JOIN [dbo].[bx_configuration_type] bx_ctp ON bxct.configuration_type_id = bx_ctp.id

                --/ Subquery for Configuration Access Priority
                LEFT JOIN (SELECT TOP 1
                        id,
                        code,
                        name,
                        configuration_id,
                        priority_rank_id
                FROM [dbo].[bx_configuration_access_priority]) bx_cap ON bx_c.id = bx_cap.configuration_id
                LEFT JOIN [dbo].[bx_attrib_template] bx_ctav ON bx_c.configuration_template_id = bx_ctav.configuration_template_id
                INNER JOIN [dbo].[bx_attrib_val_type] bx_vt ON bx_ctav.attrib_value_type_id = bx_vt.id
                LEFT JOIN [dbo].[bx_configuration_attrib_val] bx_cav ON bx_ctav.id = bx_cav.attrib_template_id

                --/ Subquery for Attribute Display Priority
                LEFT JOIN (SELECT TOP 1
                        bx.value,
                        bx.configuration_attrib_val_id,
                        bx.attrib_access_priority_id,
                        bx_1.id,
                        bx_2.priority_rank,
                        LOWER(bx_1.code) as code,
                        LOWER(bx_2.code) as access
                FROM [dbo].[bx_configuration_attrib_val_override] bx
                        INNER JOIN [dbo].[bx_attrib_access_priority] bx_1 ON bx.attrib_access_priority_id = bx_1.id
                        INNER JOIN [dbo].[bx_priority_rank] bx_2 ON bx_1.priority_rank_id = bx_2.id
                WHERE LOWER(bx_1.code) = LOWER(@regionId)
                        OR LOWER(bx_1.code) = LOWER(@locationId)
                        OR LOWER(bx_1.code) = LOWER(@userId)
                ORDER BY bx_2.priority_rank ) bx_ap ON bx_cav.id = bx_ap.configuration_attrib_val_id

        --/ Filter Configuration group; can be used as appID
        WHERE  bx_c.configuration_group_id = @groupId

        ORDER BY config_id, attrib_name ASC;

END

If @option ='GetBxConfigurationDataById' 
	BEGIN
        SELECT TOP 2000
                bx_c.id,
                bx_c.name as config_name,
                bx_c.description as config_desc,
                bx_c.is_enabled as config_is_enabled,
                bx_c.configuration_template_id as config_templ_id,
                CASE 
                WHEN bx_cap.name IS NULL THEN 'Global Access'
                ELSE bx_cap.name END access_req,
                CASE 
                WHEN LOWER(bx_cap.name) IS NOT NULL
                        AND LOWER(bx_cap.code) != LOWER(@regionId)
                        AND LOWER(bx_cap.code) != LOWER(@locationId) THEN 'false'
                ELSE 'true' END access,
                CASE 
                WHEN bx_cav.id IS NULL THEN 'temp_attrib'
                ELSE
                    CASE 
                        WHEN bx_ap.priority_rank IS NULL THEN 'config_attrib'
                        ELSE 'config_attrib_ovr' END 
                END attrib_origin,
                CASE 
                WHEN bx_cav.id IS NULL THEN '00000000-0000-0000-0000-000000000000'
                ELSE bx_cav.id
                END attrib_id,
                bx_ctav.id as attrib_templ_id,
                CASE 
                        WHEN bx_cav.sort_index IS NULL THEN 0
                        ELSE bx_cav.sort_index
                        END attrib_sort_idx,
                CASE 
                WHEN bx_cav.id IS NULL THEN bx_ctav.[value] 
                ELSE
                    CASE 
                        WHEN bx_ap.priority_rank IS NULL THEN bx_cav.[value]
                        ELSE bx_ap.value END 
                END attrib_val,
                bx_ctav.[name] as attrib_name,
                bx_ctav.[description] as attrib_desc,
                bx_vt.name as attrib_val_type,
                CASE 
                WHEN bx_cav.id IS NULL AND bx_cav.options IS NULL 
                THEN bx_ctav.[options] 
                ELSE bx_cav.[options] END attrib_options,
                bx_ctav.can_override as attrib_ovr_enabled,
                LOWER(bx_ap.access) as attrib_ovr_status,
                LOWER(bx_ap.code) as attrib_ovr_filter,
                bx_c.id as config_id,
                bx_ctp.name as config_type,
                bx_ctav.configuration_media_id as attrib_media_id,
                CASE 
                WHEN bx_c.configuration_group_id = NULL THEN 'default group'
                ELSE 'custom group' END config_group_id,
                bx_c.updated_date as config_updated_date

        FROM [dbo].[bx_configuration] bx_c

                --// Joins
                INNER JOIN [dbo].[bx_configuration_template] bxct ON bx_c.configuration_template_id = bxct.id
                INNER JOIN [dbo].[bx_configuration_type] bx_ctp ON bxct.configuration_type_id = bx_ctp.id

                --/ Subquery for Configuration Access Priority
                LEFT JOIN (SELECT TOP 1
                        id,
                        code,
                        name,
                        configuration_id,
                        priority_rank_id
                FROM [dbo].[bx_configuration_access_priority]) bx_cap ON bx_c.id = bx_cap.configuration_id
                LEFT JOIN [dbo].[bx_attrib_template] bx_ctav ON bx_c.configuration_template_id = bx_ctav.configuration_template_id
                INNER JOIN [dbo].[bx_attrib_val_type] bx_vt ON bx_ctav.attrib_value_type_id = bx_vt.id
                LEFT JOIN [dbo].[bx_configuration_attrib_val] bx_cav ON bx_ctav.id = bx_cav.attrib_template_id

                --/ Subquery for Attribute Display Priority
                LEFT JOIN (SELECT TOP 1
                        bx.value,
                        bx.configuration_attrib_val_id,
                        bx.attrib_access_priority_id,
                        bx_1.id,
                        bx_2.priority_rank,
                        LOWER(bx_1.code) as code,
                        LOWER(bx_2.code) as access
                FROM [dbo].[bx_configuration_attrib_val_override] bx
                        INNER JOIN [dbo].[bx_attrib_access_priority] bx_1 ON bx.attrib_access_priority_id = bx_1.id
                        INNER JOIN [dbo].[bx_priority_rank] bx_2 ON bx_1.priority_rank_id = bx_2.id
                WHERE LOWER(bx_1.code) = LOWER(@regionId)
                        OR LOWER(bx_1.code) = LOWER(@locationId)
                        OR LOWER(bx_1.code) = LOWER(@userId)
                ORDER BY bx_2.priority_rank ) bx_ap ON bx_cav.id = bx_ap.configuration_attrib_val_id

        --/ Filter Configuration group; can be used as appID
        WHERE  bx_c.configuration_group_id = @groupId

                --/ Filter by component ID
                AND bx_c.Id = @compId

        ORDER BY config_id, attrib_name ASC;

END   

GO