use wine_db
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (1, N'Users', N'Index', NULL, N'Visualizar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (2, N'Users', N'Create', NULL, N'Adicionar Novo')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (3, N'Users', N'Edit,EditPost', NULL, N'Editar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (4, N'Users', N'Details', NULL, N'Verificar perfis do usuário')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (5, N'Users', N'Delete', NULL, N'Deletar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (6, N'Roles', N'Index', NULL, N'Visualizar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (7, N'Roles', N'Create', NULL, N'Adicionar Nova')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (8, N'Roles', N'Edit,EditPost', NULL, N'Editar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (9, N'Roles', N'Details', NULL, N'Verificar usuários de um perfil')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (10, N'Roles', N'Delete', NULL, N'Deletar')
INSERT [dbo].[Credential] ([Id], [Controller], [Action], [Param], [Descr]) VALUES (11, N'Roles', N'Credentials,CredentialsPost', NULL, N'Configurar as credenciais de um perfil')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (1, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (2, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (3, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (4, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (5, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (6, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (7, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (8, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (9, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (10, N'RoleId')
INSERT [dbo].[CredentialRole] ([CredentialId], [RoleId]) VALUES (11, N'RoleId')
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\DefaultAppPool')
BEGIN
    CREATE LOGIN [IIS APPPOOL\DefaultAppPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [WineDbUser] 
  FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO
EXEC sp_addrolemember 'db_owner', 'WineDbUser'
GO