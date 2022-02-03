SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*********************************
 Stored Procedures
**********************************/
DROP PROCEDURE IF EXISTS  [dbo].[sp_bx_get_configuration_data]
GO  
-- execute   sp_bx_get_configuration_data 
-- @option='GetAllBxConfigurationAttribData' --GetAllBxConfigurationData  
-- ,@groupId= '00000000-0000-0000-0000-000000000000'
-- ,@regionId='reg_1'  
-- ,@locationId=  'loc_2'
-- ,@userId  = 'ahhenderson'
-- -- ,@configId = 'e6d2423e-f36b-1410-8fdc-00f7ca0ad523'
-- ,@maxRows = 1000 

CREATE PROCEDURE  [dbo].[sp_bx_get_configuration_data]
        (
        @option varchar(100) = 'GetAllBxConfigurationData',
        @regionId varchar(20) =  NULL,
        @locationId varchar(20) =  NULL,
        @userId varchar(20) = NULL,
        @configId varchar(36) = NULL,
        @groupId  varchar(36) = NULL,
        @maxRows int = 2000 
)

AS

SET NOCOUNT ON
 
DECLARE @SqlQuery NVARCHAR(4000)   

DECLARE @SqlWhereClause NVARCHAR(2000)

--// Default Values if NULL *** SQL will mysteriously fail.
IF (@groupId IS NULL) 
BEGIN
    SET @groupId = '00000000-0000-0000-0000-000000000000'
END 

IF (@regionId IS NULL) 
BEGIN
    SET @regionId = ''
END 

IF (@locationId IS NULL) 
BEGIN
    SET @locationId = ''
END 

IF (@userId IS NULL) 
BEGIN
    SET @userId = ''
END 

 --// Where clause for config Id
IF (@configId IS NULL OR @configId = '00000000-0000-0000-0000-000000000000') 
    BEGIN
        SET @SqlWhereClause =  'WHERE  vbx_cfg.config_group_id  = ' + CHAR(39) + @groupId + CHAR(39)
    END 
ELSE
    BEGIN
        SET @SqlWhereClause = '
            WHERE  vbx_cfg.config_group_id  = ' + CHAR(39) + @groupId + CHAR(39) + ' 
            AND vbx_cfg.config_id = ' + CHAR(39) + @configId + CHAR(39)
    END

--// DEBUG
--// PRINT @SqlWhereClause
 
If @option = 'GetAllBxConfigurationData'

    BEGIN 
  
        --// Configuration Query
        SET @SqlQuery = N'

            DECLARE @BxConfiguration TABLE
            (
                configId UNIQUEIDENTIFIER,  
                configTemplateId UNIQUEIDENTIFIER, 
                groupId UNIQUEIDENTIFIER, 
                isEnabled varchar(20),
                name varchar(256),
                description varchar(2000),
                type varchar(256), 
                access nvarchar(20),
                lastModified datetime2(7) 
            )
            
            INSERT INTO @BxConfiguration
            SELECT DISTINCT TOP ' + CAST(@maxRows as varchar(12))  + '
                vbx_cfg.config_id as configId, 
                vbx_cfg.config_templ_id as configTemplateId,
                vbx_cfg.config_group_id as groupId, 
                vbx_cfg.config_is_enabled as isEnabled,
                vbx_cfg.config_name as name,
                vbx_cfg.config_desc as description,
                vbx_cfg.config_type as type, 
                CASE  WHEN LOWER(vbx_cfg.access_req) IS NOT NULL
                    AND LOWER(vbx_cfg.access_req) != LOWER(' + CHAR(39) + @regionId + CHAR(39) + ')
                    AND LOWER(vbx_cfg.access_req) != LOWER(' + CHAR(39) + @locationId + CHAR(39) + ') 
                THEN ' + CHAR(39) + 'false' + CHAR(39) + '
                ELSE ' + CHAR(39) + 'true' + CHAR(39) + ' END configAccess,
                vbx_cfg.config_updated_date as lastModified 
            FROM vw_bx_configuration_data vbx_cfg   
            '
            + @SqlWhereClause + 
            ' 
            ORDER BY vbx_cfg.config_updated_date DESC;
            
            SELECT * FROM @BxConfiguration 
            '

        --// DEBUG
        --// PRINT @SqlQuery
 
        EXECUTE sp_executesql @SqlQuery; 
 
    END
 
IF @option ='GetAllBxConfigurationAttribData'

    BEGIN
 
        --// Configuration Attribute Query
        SET @SqlQuery = N'   

            DECLARE @BxConfigurationAttribute TABLE
            (
                configId UNIQUEIDENTIFIER,
                IsEnabled varchar(20),
                attribTemplateId UNIQUEIDENTIFIER,
                attribOverrideTemplateId UNIQUEIDENTIFIER, 
                name varchar(256),
                description varchar(2000),
                origin varchar(256),
                sortIdx int,
                value varchar(2000),
                valueType varchar(256),
                options varchar(2000),
                overrideEnabled bit,
                overrideStatus varchar(20),
                overrideFilter varchar(20),
                mediaId UNIQUEIDENTIFIER,
                lastModified datetime2(7) 
            )
 
            INSERT INTO @BxConfigurationAttribute
            SELECT DISTINCT 
                vbx_cfg.config_id as configId,
                vbx_cfg.config_is_enabled as isEnabled,
                vbx_cfg.attrib_templ_id as attribTemplateId,
                vbx_cfg.attrib_val_id as attribValId, 
                vbx_cfg.attrib_name as name,
                vbx_cfg.attrib_desc as description,
                CASE 
                WHEN vbx_cfg.attrib_val_id IS NULL THEN ' + CHAR(39) + 'temp_attrib' + CHAR(39) + '
                ELSE
                    CASE 
                        WHEN bx_ap.priority_rank IS NULL THEN ' + CHAR(39) + 'config_attrib' + CHAR(39) + '
                        ELSE ' + CHAR(39) + 'config_attrib_ovr' + CHAR(39) + ' END 
                END origin,
                vbx_cfg.attrib_sort_idx as sortIndex,
                vbx_cfg.attrib_val as value,
                vbx_cfg.attrib_val_type as valueType,
                vbx_cfg.attrib_options as options,
                vbx_cfg.attrib_ovr_enabled as overrideOn,
                LOWER(bx_ap.access) as overrideStatus,
                LOWER(bx_ap.code) as overrideFilter,
                vbx_cfg.attrib_media_id as mediaId,
                vbx_cfg.config_updated_date as lastModified

            FROM vw_bx_configuration_data vbx_cfg 
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
            WHERE LOWER(bx_1.code) = LOWER(' + CHAR(39) + @regionId + CHAR(39) + ')
                OR LOWER(bx_1.code) = LOWER(' + CHAR(39) + @locationId + CHAR(39) + ')
                OR LOWER(bx_1.code) = LOWER(' + CHAR(39) + @userId + CHAR(39) + ')
            ORDER BY bx_2.priority_rank ) bx_ap ON vbx_cfg.attrib_val_id = bx_ap.configuration_attrib_val_id 
            ' + @SqlWhereClause + '
            ORDER BY vbx_cfg.config_id,  vbx_cfg.attrib_name asc;

            --// Return table variable
            SELECT *
            FROM @BxConfigurationAttribute
            '     

            --// DEBUG
           PRINT @SqlQuery
 
        EXECUTE sp_executesql @SqlQuery; 

    END 

GO


/*********************************
 Views
**********************************/
IF EXISTS(SELECT 1
FROM sys.views
WHERE NAME='vw_bx_configuration_data' AND TYPE ='v')
DROP VIEW [dbo].[vw_bx_configuration_data]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_bx_configuration_data]
AS

    SELECT
        bx_c.name as config_name,
        bx_c.description as config_desc,
        bx_c.is_enabled as config_is_enabled,
        bx_c.configuration_template_id as config_templ_id,
        CASE 
            WHEN bx_cap.name IS NULL THEN 'Global Access'
            ELSE bx_cap.name END access_req, 
        CASE 
            WHEN bx_cav.id IS NULL THEN '00000000-0000-0000-0000-000000000000'
            ELSE bx_cav.id
            END attrib_val_id,
        bx_ctav.id as attrib_templ_id,
        CASE 
            WHEN bx_cav.sort_index IS NULL THEN 0
            ELSE bx_cav.sort_index
            END attrib_sort_idx,
        CASE 
            WHEN bx_cav.id IS NULL 
            THEN bx_ctav.[value]  
            ELSE bx_cav.[value] END attrib_val,
        bx_ctav.[name] as attrib_name,
        bx_ctav.[description] as attrib_desc,
        bx_vt.name as attrib_val_type,
        CASE 
            WHEN bx_cav.id IS NULL AND bx_cav.options IS NULL 
            THEN bx_ctav.[options] 
            ELSE bx_cav.[options] END attrib_options,
        bx_ctav.can_override as attrib_ovr_enabled,
        bx_c.id as config_id,
        bx_ctp.name as config_type,
        bx_ctav.configuration_media_id as attrib_media_id,
        bx_c.configuration_group_id as config_group_id,
        CASE 
            WHEN bx_c.configuration_group_id = NULL THEN 'default group'
            ELSE 'custom group' END config_group_type,
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
  
GO
