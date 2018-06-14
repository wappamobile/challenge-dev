Considerações:
- Foram dispensadas 9 horas no teste entre os dias 10/06 a 13/06.
- Documentação da API por Swagger 
- NSubstitute para Testes Unitários

PreRequisitos:
- Visual Studio 2017
- .NET Core 2.0
- (Opcional)Sql Server Express ou servidor Sql Server

- Setup
 Foi criada uma base de dados na GearHost, na qual o desenvolvimento foi realizado e o arquivo de configuração já está apontando.
 Caso seja optado por criar uma base de dados local, com o devido privilegio, executar os scripts da pasta Setup que encontram-se no projeto Teste.SQLServer na seguinte ordem:
 * 001_Criacao_Tabelas.sql
 * 002_Inserir_Marcas_Modelos.sql
 Em seguida atualizar a connectionString.

