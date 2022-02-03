
/******************************
** File: bx_configuration_api_dml.sql
** Name: BxConfiguration Managment DML Script
** Desc: BxConfiguration Managment Programmability scripts(sprocs) 
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
-- select bx.id, bx.name, bx.description from BX_CONFIGURATION_TEMPLATE bx Where id =1
-- -- select * from BX_ATTRIB_TEMPLATE x where x.component_template_id = 1
-- select x.code, x.name, x.description, x.value, x.options from BX_ATTRIB_TEMPLATE x where x.component_template_id = 1

-- select xa.value  from BX_CONFIGURATION_ATTRIB_VAL xa where xa.component_id = 1
-- select xao.value, xao.component_Attrib_val_id  from BX_CONFIGURATION_ATTRIB_VAL_OVERRIDE xao 

/******************************
** Preparation
*******************************/
DROP PROCEDURE IF EXISTS bx_execute_sp.sp_getBxConfigurationData;  
GO  
 
DROP SCHEMA IF EXISTS bx_execute_sp; 
GO  

/******************************
** Create
*******************************/
CREATE SCHEMA bx_execute_sp;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--// execute [bx_execute_sp].[sp_getBxConfigurationData] @option='GetAllBxConfigurationData', @regionId='scal', @locationId='cud', @userId='E283476'
--// execute bx_execute_sp.sp_getBxConfigurationData @option='GetBxConfigurationDataById', @regionId='ncal', @cfgId=1

-- CREATE PROCEDURE  bx_execute_sp.sp_getBxConfigurationData (
-- 			@option varchar(100) = 'GetAllBxConfigurationData', 
--             @regionId varchar(10) =  NULL,
--             @locationId varchar(10) =  NULL,
-- 			@userId varchar(10) = NULL,
--             @cfgId INT = -1,
--             @groupId INT = 0
-- )

-- AS

-- SET NOCOUNT ON

CREATE PROCEDURE  sp_getBxConfigurationData (
			@option varchar(100) = 'GetAllBxConfigurationData', 
            @regionId varchar(10) =  NULL,
            @locationId varchar(10) =  NULL,
			@userId varchar(10) = NULL,
            @cfgId INT = -1,
            @groupId INT = 0
)

AS

SET NOCOUNT ON

  
If @option ='GetAllBxConfigurationData' 
	BEGIN	
	SELECT TOP 2000   
        bx_cfg.NAME as CFG_NAME,  
        bx_cfg.DESCRIPTION as CFG_DESC,
        bx_cfg.IS_ENABLED as CFG_IS_ENABLED, 
        bx_cfg.CONFIGURATION_TEMPLATE_ID as CFG_TEMPL_ID, 
        CASE 
            WHEN bx_cfg_ap.NAME IS NULL THEN 'Global Access'
            ELSE bx_cfg_ap.NAME END ACCESS_REQ,
        CASE 
            WHEN LOWER(bx_cfg_ap.NAME) IS NOT NULL 
            AND LOWER(bx_cfg_ap.CODE) != LOWER(@regionId) 
            AND LOWER(bx_cfg_ap.CODE) != LOWER(@locationId) THEN 'FALSE'
            ELSE 'TRUE' END ACCESS,
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN 'TEMP_ATTRIB'
            ELSE
                CASE 
                    WHEN bx_ap.PRIORITY_RANK IS NULL THEN 'CFG_ATTRIB'
                    ELSE 'CFG_ATTRIB_OVR' END 
            END ATTRIB_ORIGIN,
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN -1
            ELSE bx_cfg_av.ID
            END ATTRIB_ID,   
        bx_cfg_av.GUID_ID as ATTRIB_GUID_ID,
        bx_cfg_tav.ID as ATTRIB_TEMPL_ID,
        bx_cfg_tav.GUID_ID as ATTRIB_TEMPL_GUID_ID,  
        CASE 
            WHEN bx_cfg_av.SORT_INDEX IS NULL THEN 0
            ELSE bx_cfg_av.SORT_INDEX
            END ATTRIB_SORT_IDX,     
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN bx_cfg_tav.[VALUE] 
            ELSE
                CASE 
                    WHEN bx_ap.PRIORITY_RANK IS NULL THEN bx_cfg_av.[VALUE]
                    ELSE bx_ap.VALUE END 
            END ATTRIB_VAL,     
        bx_cfg_tav.[NAME] as ATTRIB_NAME,
        bx_cfg_tav.[DESCRIPTION] as ATTRIB_DESC, 
        bx_vt.NAME as ATTRIB_VAL_TYPE,  
        CASE 
            WHEN bx_cfg_av.GUID_ID IS NULL AND bx_cfg_av.OPTIONS IS NULL 
            THEN bx_cfg_tav.[OPTIONS] 
            ELSE bx_cfg_av.[OPTIONS] END ATTRIB_OPTIONS,
        bx_cfg_tav.CAN_OVERRIDE as ATTRIB_OVR_ENABLED,
        LOWER(bx_ap.ACCESS) as ATTRIB_OVR_STATUS,
        LOWER(bx_ap.CODE) as ATTRIB_OVR_FILTER, 
        bx_cfg.ID as CFG_ID,   
        bx_cfg_tp.NAME as CFG_TYPE,  
        bx_cfg_tav.CONFIGURATION_MEDIA_ID as ATTRIB_MEDIA_ID,
        CASE 
            WHEN bx_cfg.CONFIGURATION_GROUP_ID = 0 THEN 'DEFAULT GROUP'
            ELSE 'CUSTOM GROUP' END CFG_GROUP_ID,
        bx_cfg.GUID_ID as CFG_GUID_ID, 
        bx_cfg.LAST_MODIFIED_DATE as CFG_LAST_MODIFIED_DATE
		FROM [dbo].[BX_CONFIGURATION] bx_cfg
        INNER JOIN [dbo].[BX_CONFIGURATION_TEMPLATE] bx_cfg_t ON bx_cfg.CONFIGURATION_TEMPLATE_ID = bx_cfg_t.ID
        INNER JOIN [dbo].[BX_CONFIGURATION_TYPE] bx_cfg_tp ON bx_cfg_t.CONFIGURATION_TYPE_ID = bx_cfg_tp.ID 
        --/ Subquery for Component Access Priority
        LEFT JOIN (SELECT TOP 1 ID, 
                    CODE, 
                    NAME, 
                    CONFIGURATION_ID, 
                    PRIORITY_RANK_ID 
                    FROM [dbo].[BX_CONFIGURATION_ACCESS_PRIORITY]) bx_cfg_ap ON bx_cfg.ID = bx_cfg_ap.CONFIGURATION_ID 

        LEFT JOIN [dbo].[BX_ATTRIB_TEMPLATE] bx_cfg_tav ON bx_cfg.CONFIGURATION_TEMPLATE_ID = bx_cfg_tav.CONFIGURATION_TEMPLATE_ID
        INNER JOIN [dbo].[BX_ATTRIB_VAL_TYPE] bx_vt ON bx_cfg_tav.ATTRIB_VALUE_TYPE_ID = bx_vt.ID
        LEFT JOIN [dbo].[BX_CONFIGURATION_ATTRIB_VAL] bx_cfg_av ON bx_cfg_tav.ID = bx_cfg_av.ATTRIB_TEMPLATE_ID 

        --/ Subquery for Attribute Display Priority
        LEFT JOIN (SELECT TOP 1 bx.VALUE, 
                    bx.CONFIGURATION_ATTRIB_VAL_ID, 
                    bx.ATTRIB_ACCESS_PRIORITY_ID, 
                    bx_ap.ID, 
                    bx_pr.PRIORITY_RANK, 
                    LOWER(bx_ap.CODE) as CODE, 
                    LOWER(bx_pr.CODE) as ACCESS  
                    FROM  [dbo].[BX_CONFIGURATION_ATTRIB_VAL_OVERRIDE] bx
                    INNER JOIN  [dbo].[BX_ATTRIB_ACCESS_PRIORITY] bx_ap ON bx.ATTRIB_ACCESS_PRIORITY_ID = bx_ap.ID
                    INNER JOIN [dbo].[BX_PRIORITY_RANK] bx_pr ON bx_ap.PRIORITY_RANK_ID = bx_pr.ID
                    WHERE LOWER(bx_ap.CODE) = LOWER(@regionId) 
                    OR LOWER(bx_ap.CODE) = LOWER(@locationId) 
                    OR LOWER(bx_ap.CODE) = LOWER(@userId)  

                    ORDER BY bx_pr.PRIORITY_RANK
                   ) bx_ap ON bx_cfg_av.ID = bx_ap.CONFIGURATION_ATTRIB_VAL_ID

        --/ Filter component group; can be used as appID
        WHERE  bx_cfg.CONFIGURATION_GROUP_ID = @groupId 

		ORDER BY CFG_ID, ATTRIB_NAME ASC; 
	END  

    If @option ='GetBxConfigurationDataById' 
	BEGIN	
	SELECT TOP 2000   
        bx_cfg.NAME as CFG_NAME,  
        bx_cfg.DESCRIPTION as CFG_DESC,
        bx_cfg.IS_ENABLED as CFG_IS_ENABLED, 
        bx_cfg.CONFIGURATION_TEMPLATE_ID as CFG_TEMPL_ID, 
        CASE 
            WHEN bx_cfg_ap.NAME IS NULL THEN 'Global Access'
            ELSE bx_cfg_ap.NAME END ACCESS_REQ,
        CASE 
            WHEN LOWER(bx_cfg_ap.NAME) IS NOT NULL 
            AND LOWER(bx_cfg_ap.CODE) != LOWER(@regionId) 
            AND LOWER(bx_cfg_ap.CODE) != LOWER(@locationId) THEN 'FALSE'
            ELSE 'TRUE' END ACCESS,
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN 'TEMP_ATTRIB'
            ELSE
                CASE 
                    WHEN bx_ap.PRIORITY_RANK IS NULL THEN 'CFG_ATTRIB'
                    ELSE 'CFG_ATTRIB_OVR' END 
            END ATTRIB_ORIGIN,
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN -1
            ELSE bx_cfg_av.ID
            END ATTRIB_ID,  
        bx_cfg_av.GUID_ID as ATTRIB_GUID_ID,
        bx_cfg_tav.ID as ATTRIB_TEMPL_ID,
        bx_cfg_tav.GUID_ID as ATTRIB_TEMPL_GUID_ID,  
        CASE 
            WHEN bx_cfg_av.SORT_INDEX IS NULL THEN 0
            ELSE bx_cfg_av.SORT_INDEX
            END ATTRIB_SORT_IDX,       
        CASE 
            WHEN bx_cfg_av.ID IS NULL THEN bx_cfg_tav.[VALUE] 
            ELSE
                CASE 
                    WHEN bx_ap.PRIORITY_RANK IS NULL THEN bx_cfg_av.[VALUE]
                    ELSE bx_ap.VALUE END 
            END ATTRIB_VAL,     
        bx_cfg_tav.[NAME] as ATTRIB_NAME,
        bx_cfg_tav.[DESCRIPTION] as ATTRIB_DESC, 
        bx_vt.NAME as ATTRIB_VAL_TYPE,  
        CASE 
            WHEN bx_cfg_av.GUID_ID IS NULL AND bx_cfg_av.OPTIONS IS NULL 
            THEN bx_cfg_tav.[OPTIONS] 
            ELSE bx_cfg_av.[OPTIONS] END ATTRIB_OPTIONS,
        bx_cfg_tav.CAN_OVERRIDE as ATTRIB_OVR_ENABLED,
        LOWER(bx_ap.ACCESS) as ATTRIB_OVR_STATUS,
        LOWER(bx_ap.CODE) as ATTRIB_OVR_FILTER,  
        bx_cfg.ID as CFG_ID,   
        bx_cfg_tp.NAME as CFG_TYPE,  
        bx_cfg_tav.CONFIGURATION_MEDIA_ID as ATTRIB_MEDIA_ID,
        CASE 
            WHEN bx_cfg.CONFIGURATION_GROUP_ID = 0 THEN 'DEFAULT GROUP'
            ELSE 'CUSTOM GROUP' END CFG_GROUP_ID,
        bx_cfg.GUID_ID as CFG_GUID_ID, 
        bx_cfg_tav.GUID_ID as ATTRIB_GUID_ID, 
        bx_cfg.LAST_MODIFIED_DATE as CFG_LAST_MODIFIED_DATE
		FROM [dbo].[BX_CONFIGURATION] bx_cfg
        INNER JOIN [dbo].[BX_CONFIGURATION_TEMPLATE] bx_cfg_t ON bx_cfg.CONFIGURATION_TEMPLATE_ID = bx_cfg_t.ID
        INNER JOIN [dbo].[BX_CONFIGURATION_TYPE] bx_cfg_tp ON bx_cfg_t.CONFIGURATION_TYPE_ID = bx_cfg_tp.ID 
        --/ Subquery for Component Access Priority
        LEFT JOIN (SELECT TOP 1 ID, 
                    CODE, 
                    NAME, 
                    CONFIGURATION_ID, 
                    PRIORITY_RANK_ID 
                    FROM [dbo].[BX_CONFIGURATION_ACCESS_PRIORITY]) bx_cfg_ap ON bx_cfg.ID = bx_cfg_ap.CONFIGURATION_ID 

        LEFT JOIN [dbo].[BX_ATTRIB_TEMPLATE] bx_cfg_tav ON bx_cfg.CONFIGURATION_TEMPLATE_ID = bx_cfg_tav.CONFIGURATION_TEMPLATE_ID
        INNER JOIN [dbo].[BX_ATTRIB_VAL_TYPE] bx_vt ON bx_cfg_tav.ATTRIB_VALUE_TYPE_ID = bx_vt.ID
        LEFT JOIN [dbo].[BX_CONFIGURATION_ATTRIB_VAL] bx_cfg_av ON bx_cfg_tav.ID = bx_cfg_av.ATTRIB_TEMPLATE_ID 

        --/ Subquery for Attribute Display Priority
        LEFT JOIN (SELECT TOP 1 bx.VALUE, 
                    bx.CONFIGURATION_ATTRIB_VAL_ID, 
                    bx.ATTRIB_ACCESS_PRIORITY_ID, 
                    bx_ap.ID, 
                    bx_pr.PRIORITY_RANK, 
                    LOWER(bx_ap.CODE) as CODE, 
                    LOWER(bx_pr.CODE) as ACCESS  
                    FROM  [dbo].[BX_CONFIGURATION_ATTRIB_VAL_OVERRIDE] bx
                    INNER JOIN  [dbo].[BX_ATTRIB_ACCESS_PRIORITY] bx_ap ON bx.ATTRIB_ACCESS_PRIORITY_ID = bx_ap.ID
                    INNER JOIN [dbo].[BX_PRIORITY_RANK] bx_pr ON bx_ap.PRIORITY_RANK_ID = bx_pr.ID
                    WHERE LOWER(bx_ap.CODE) = LOWER(@regionId) 
                    OR LOWER(bx_ap.CODE) = LOWER(@locationId) 
                    OR LOWER(bx_ap.CODE) = LOWER(@userId)  

                    ORDER BY bx_pr.PRIORITY_RANK
                   ) bx_ap ON bx_cfg_av.ID = bx_ap.CONFIGURATION_ATTRIB_VAL_ID

        --/ Filter component group; can be used as appID
        WHERE  bx_cfg.CONFIGURATION_GROUP_ID = @groupId 

         --/ Filter by component ID
		AND  bx_cfg.Id = @cfgId  

		ORDER BY CFG_ID, ATTRIB_NAME ASC; 
	END  


GO