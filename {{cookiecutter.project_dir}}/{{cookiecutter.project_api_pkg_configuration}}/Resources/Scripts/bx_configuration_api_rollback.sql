
/*********************************
 Tables
**********************************/

DROP TABLE IF EXISTS dbo.BX_ATTRIB_ACCESS_PRIORITY; 
GO  
DROP TABLE IF EXISTS dbo.BX_ATTRIB_TEMPLATE; 
GO  
DROP TABLE IF EXISTS dbo.BX_ATTRIB_VAL_TYPE;  
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION; 
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_ACCESS_PRIORITY;
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_ATTRIB_VAL; 
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_ATTRIB_VAL_OVERRIDE
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_MEDIA; 
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_TEMPLATE; 
GO  
DROP TABLE IF EXISTS dbo.BX_CONFIGURATION_TYPE; 
GO  
DROP TABLE IF EXISTS dbo.BX_PRIORITY_RANK; 
GO  

/*********************************
 Stored Procedures
**********************************/

-- DROP PROCEDURE IF EXISTS bx_execute_sp.sp_bx_get_configuration_data;  
-- GO  
 
-- DROP SCHEMA IF EXISTS bx_execute_sp; 
-- GO  

DROP PROCEDURE IF EXISTS  sp_bx_get_configuration_data;  
GO  

/*********************************
 Views
**********************************/
DROP VIEW [dbo].[vw_bx_configuration_data]
GO
