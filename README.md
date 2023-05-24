
# Prova prático de back-end LBCA.

Api Restful feita em .Net SDK 5.0.406, em camadas, usando Dapper para persistencia e acesso ao banco de dados,
Swagger para documentação da Api e SQLServer como SGBD.

## Pré requisitos
 
1. [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/vs/)

## Como baixar o código

git clone https://github.com/JucelioAmaral/ControleDeNoticias.git

## Como configurar a api(Backend)?

1. Abrir a Visual Code ou Studio;
2. Configurar o arquivo "appsettings.json" com a connectionString, apontando para o banco SQL server;
3. Instalar o pacote do sql server: "Install-Package Microsoft.EntityFrameworkCore.SqlServer";
4. Abrir o Package Manager Console, alterar o "Default project" (que fica na parte superior do console) para o Class Library que encontra-se os arquivos de persistência para "WebApiLBCA.Infraestrutura"
5. Executar o comando: Add-Migration InitialCreate;
6. Executar o comando: Update-Database;
7. Executar o script "ScriptInsertUsuario&Classific.sql";
8. Executar a api (WebApiLBCA).

**API roda na porta 5001 e pode ser testada pelo link: https://localhost:5001/swagger/index.html**

