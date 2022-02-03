-- DELETE FROM dbo.bx_perm_claim
-- DELETE FROM dbo.bx_perm_entity
-- DELETE FROM dbo.bx_perm_entity_group 
-- DELETE FROM dbo.bx_perm_module
-- DELETE FROM dbo.bx_perm_module_role
-- DELETE FROM dbo.bx_perm_user

-- DELETE FROM dbo.bx_perm_claim_type
-- DELETE FROM dbo.bx_perm_entity_type
-- DELETE FROM dbo.bx_perm_entity_group_type
-- DELETE FROM dbo.bx_perm_role_type
-- DELETE FROM dbo.bx_perm_module_type
-- DELETE FROM dbo.bx_perm_user_type


-- DELETE FROM dbo.bx_perm_application_role_xref
-- DELETE FROM dbo.bx_perm_entity_application_xref
-- DELETE FROM dbo.bx_perm_entity_group_role_user_xref
-- DELETE FROM dbo.bx_perm_entity_group_role_xref
-- DELETE FROM dbo.bx_perm_entity_group_user_xref
-- DELETE FROM dbo.bx_perm_entity_user_xref
-- DELETE FROM dbo.bx_perm_module_claim_xref
-- DELETE FROM dbo.bx_perm_role_module_claim_xref
-- DELETE FROM dbo.bx_perm_role_module_xref

 
-- GO

DROP TABLE IF EXISTS  [dbo].[bx_perm_claim]
GO  

 SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bx_perm_claim](
	[id] [uniqueidentifier] NOT NULL,
	[claim_type_id] [uniqueidentifier] NOT NULL,
	[is_archived] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[created_by] [nvarchar](256) NULL,
	[updated_by] [nvarchar](256) NULL,
	[updated_date] [datetime2](7) NOT NULL,
	[created_date] [datetime2](7) NOT NULL,
	[name] [nvarchar](256) NOT NULL,
	[description] [nvarchar](2000) NULL,
	[icon_type_id] [uniqueidentifier] NOT NULL,
	[key_index] [int] NOT NULL,
	[code] [nvarchar](40) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bx_perm_claim] ADD  CONSTRAINT [pk_bx_perm_claim] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bx_perm_claim_code] ON [dbo].[bx_perm_claim]
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_bx_perm_claim_key_index] ON [dbo].[bx_perm_claim]
(
	[key_index] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bx_perm_claim] ADD  DEFAULT (newsequentialid()) FOR [id]
GO
