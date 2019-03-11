# Documentação

## Tecnologias
Esta aplicação utiliza as seguintes tecnologias:
- .net core 2.2
- SQL Server
- xUnit

## Estrutura do projeto
A solution é divida em 3 projetos:

| Projeto | Descrição |
| ------ | ------ |
| Driver.Api | Projeto que concentra as regras da api |
| Driver.Application | Projeto que concentra os serviços e instruções de comunicação com banco |
| Driver.Api.Test | Projeto de teste |

## Estrutura da api
A api utiliza os métodos HTTP como decisão do que será abortado. Assim, tem somente a variação de de utilizar o id ou não, todos com raiz /api/Driver/:

| Método | Parametro | Descrição |
| ------ | ------ | ------ |
| GET | |Retorna todos os motoristas ordenados por nome e/ou sobrenome. |
| GET | id | Retorna uma versão mais completa, mas de um único motorista. |
| POST | | Inclui um novo motorista. Esta operação utiliza dados do corpo da mensagem. |
| PUT | id | Atualiza um novo motorista. Esta operação utiliza dados do corpo da mensagem. |
| PUT | id | Apaga lógicamente um motorista existente. |

Para mais inforções, a aplicação foi feita com o swagger e rodando a aplicação na url /swagger é possível ter detalhadamente o que se passar e suas respectivas respostas.

## Instruções

A api foi feita exclusivamente para o desafio. Logo, toda vez que ela iniciar, vai refazer a base. Basta que o usuário tenha acesso na master e permissão suficiente para criar, apagar base e derrumar sessions. No caso, o exemplo utiliza uma base base local, a qual pode ser alterada a qualquer momento, desde que as duas connections sejam da mesma instancia SQL.

A connection MasterConnection é uma connection padrão para permitir recriar a base.

Vale lembrar que esta estrutura de sempre recriar é condição sine qua non para que os testes funcionem adequadamente.

Para rodar a aplicação, entre na pasta da api e rode o seguinte comando:
```sh
dotnet run
```
Para rodar os testes por comando, na raíz da solution, rode o seguinte comando:
```sh
dotnet test -v n
```
