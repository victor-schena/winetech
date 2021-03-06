﻿# Projeto Base com .NET e Entity Framework (MVC4 + Razor)

## 
Softwares Necessários


Você precisa ter instalado os seguintes softwares:


1 - *MS Visual Studio 2013 ou superior*. Caso não tenha, baixe [aqui](https://www.visualstudio.com/vs/)


2 - *MS SQL Server 2012 ou superior*. Caso não tenha, baixe [aqui](https://www.microsoft.com/en-us/cloud-platform/sql-server-editions)

## 

Passo a Passo


1 - Clonar o repositório ou baixar o .zip (Ref: **Bitbucket**: Downloads > Download repository)


2 - Abrir o projeto no Visual Studio (*Projects.sln*)


3 - Compilar o projeto para que o *Nuget Package Manager* baixe as dependências (Atalho: **CTRL + Shift + B**)


4 - Abrir os arquivos **Web.config** (Admin, Web) e **App.config** (Entities) para alterar a connectionString (conexão com o banco de dados)


5 - Abrir o Package Manager Console (Atalho: **Tools > Nuget Package Manager > Package Manager Console**), setando como *Default project* o projeto `Admin`


6 - Executar o comando `Update-Database` para a criação do banco e das tabelas base para o gerenciamento de *Roles* e *Users*


7 - O banco estará criado. Setar o projeto `Admin` como *"Startup Project"* e rodar o projeto (play)


8 - Para logar, será necessário criar um usuário


9 - Inicialmente, vamos criar uma role (Admin) na rota: `/roles/create` (que está configurada, inicialmente, para acesso anônimo *[AllowAnonymous]* no controller *RolesController*)


10 - Em seguida, vamos criar um usuário (admin@admin.com) na rota: `/users/create`
OBS: Dica de senha: **Senha@12**


11 - Logar com o usuário criado


12 - Lembrar de remover a Annotation *[AllowAnonymous]*


13 - Para configurar as permissões de gerenciamento de *Roles* e *Users*, localizar a query que está na pasta `App_Data` (**initial-credentials-config.sql**)


14 - Alterar o parâmetro `RoleId` pelo ID da role (Admin, tabela **AspNetRoles**) criada no passo 9, para todas as `Credentials`


15 - Executar a query. Pronto, o projeto está rodando e o usuário criado tem permissão para o gerenciamento de *Roles* e *Users*


16 - Para verificar e alterar as permissões, acessar: **Perfis > Credenciais do Perfil**


## Dicas Importantes

**
Como criar uma nova tabela no banco de dados?**


1 - Criar uma classe (tabela) na pasta `Tables` no projeto Entities


2 - Adicionar a referência da tabela criada no arquivo **EntitiesDb**, localizado na pasta `Contexts`
```

Ex: public DbSet<TABELA> VARIAVEL { get; set; }
```


3 - Caso a tabela tenha relacionamentos (one-to-many, many-to-many) com outras tabelas, descrevê-los no método *OnModelCreating*


4 - Criar uma migration para criar a tabela no banco de dados, abrindo o Package Manager Console (Atalho: **Tools > Nuget Package Manager > Package Manager Console**),
setando como *Default project* o projeto `Entities`


5 - Executar o comando `Add-Migration`, dando um nome para a mesma


6 - Será criada, automaticamente, uma classe dentro da pasta `Migrations`, com a "query" que será executada no banco de dados


7 - Executar o comando `Update-Database`


8 - Pronto, a tabela está criada no banco de dados

 
OBS: Todas as alterações em tabelas serão realizadas nas classes, nunca diretamente no banco de dados


**Permissões do Admin**


1) As permissões para as várias seções do Admin deverão ser criadas manualmente na tabela `Credentials` no banco de dados


2) Para associá-las a uma Role, acessar: **Perfis > Credenciais do Perfil** e tickar, conforme necessário

3) 
Lembrar de controlar o acesso também no menu do Admin: **Views\Shared\_Menu.cshtml
**

**Padrões e Boas Práticas**


1 - Procurar sempre separar os controllers por seção (utilizar scaffolding no projeto Admin)


2 - Criar páginas de 404 amigáveis no projeto Web


3 - Lembrar de executar um **Exclude From Project** antes de remover um arquivo de qualquer projeto


4 - Ao incluir um novo arquivo, executar um **Include In Project**

...#   w i n e t e c h  
 #   w i n e t e c h  
 