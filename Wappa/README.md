# challenge-dev

## Descrição
Para concluir o desafio fiz uso das bibliotecas do Asp.Net Core como AutoMapper, Dapper, MediatR, Swagger, NSubstitute entre outras.
E o armazenamento de dados ficou a cargo do SQL Server.

## Instruções
 - Executar o script challenge-dev\Wappa\Wappa.sql em uma instância do SQL Server
 - Configurar a connectionString em challenge-dev/Wappa/src/Wappa.Api/appsettings.Development.json
 - Compilar e Executar a solution challenge-dev/Wappa/Wappa.sln
 - Fazer uso do Swagger ou Postman para testar as API's

### Dados do Projeto
 - 20hrs em 2 dias. (Sofri muito com o notebook)

### Observações
 - Não fiz uso de ORM como Entity Framework ou Dapper Contrib para demostrar os conhecimentos de T-SQL
As procedures foram criadas para ter performance principalmente a pc_SelectDriver com SQL dinâmico para fazer melhor uso dos planos de execução.

 - Não fiz uso de Docker meu processador não suporta virtualização
