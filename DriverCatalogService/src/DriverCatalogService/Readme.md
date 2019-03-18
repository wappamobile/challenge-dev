# Desafio de código
Esta é a implementação do projeto de testes proposto pela Wappa. Dentro do escopo descrito na [apresentação do desafio](https://github.com/wappamobile/challenge-dev), procurei implementar uma aplicação *serverless* utilizando os seguintes recursos e componentes:

 * **ASP.Net Core WebAPI** como plataforma de desenvolvimento dos serviços solicitados;
 * **AWS Lambda** como ambiente de execução da aplicação *serverless* implementada em WebAPI;
 * **AWS API Gateway** como porta de entrada REST dos serviços executados pelo Lambda;
 * **AWS CloudFormation** para o *deploy* por via declarativa dos recursos de nuvem requeridos pela aplicação;
 * **DynamoDB** como repositório NoSQL de informações;
 * **xUnit** para a composição dos testes de integração utilizados em todo o ciclo de desenvolvimento;
 * **RestSharp** para a chamada de funcionalidades do Google Maps API.

## Organização do projeto

A solução é dividida em dois projetos de C#: **DriverCatalogService** um contém a WebAPI requerida, e **DriverCatalogService.Tests** contém os testes de integração.

### DriverCatalogService
A estrutura deste projeto contém:
* _Controllers_: inclui os controllers que implementam as funcionalidades de catálogo de motoristas (DriverCatalogController) e listagem do catálogo (DriverCatalogQueryController).
* _Infrastructure_: inclui as definições/interfaces dos serviços consumidos pela aplicação, como o repositório de dados (IRepository/DynamoDBRepository) e a API de geolocalização (IGeoLocator/GoogleGeoLocator).
* _Models_: inclui as definições de estruturas de dados de Motorista (Driver), Endereço (Address), Carro (Car), entre outras.
* _Classes de bootstrapping_: código de inicialização, incluindo definições de IOC e carga de configuração são localizados nas classes Startup, LocalEntryPoint e LambdaEntryPoint.
* _Scripts do CloudFormation_: para automatização do provisionamento de recursos da AWS, todos os requisitos de nuvem da aplicação são declarados no arquivo serverless.template.


## Requisitos para desenvolvimento

É necessário instalar as ferramentas [Amazon.Lambda.Tools](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) e [AWS Toolkit for Visual Studio](https://aws.amazon.com/pt/visualstudio/). O primeiro extende as capacidades da CLI do .Net Core, e o segundo adiciona funcionalidades de gerenciamento do *stack* AWS ao VisualStudio.

Para instalar o AWS Lambda Tools, basta utilizar o seguinte comando Powershell:
```
    dotnet tool install -g Amazon.Lambda.Tools
```

Uma vez instaladas as ferramentas, já é possível executar os testes integrados. Uma forma prática é iniciar os testes a partir de linha de comando:

```
    cd "DriverCatalogService/test/DriverCatalogService.Tests"
    dotnet test
```


## Publicação no ambiente AWS

Basta acionar o botão *Publish to AWS* no menu de contexto do Solution Explorer, diretamente no projeto **DriverCatalogService**. Será aberta uma janela "Stacj View" com o andamento da publicação.

Após o *deploy* bem-sucedido, o API Gateway terá publicado um *endpoint* de produção a partir do qual podem ser acionadas as chamadas de API.


## Perguntas que talvez sejam feitas
No início do desenvolvimento, e também durante o projeto, algumas decisões importantes foram tomadas. Aqui está a razão por trás de algumas delas:

#### Por que AWS?
É o provedor de serviços com o qual eu tenho maior familiaridade. Tenho experiências recentes bem sucedidas com Lambda e CloudFormation, e quis re-aplicar esse conhecimento.

#### Por que serverless?
O desafio pareceu ser uma boa prova de conceito para a construção e deploy de serviços serverless.

#### Por que nomear identificadores em inglês?
Na minha opinião, a língua inglesa facilita a tarefa de "dar nomes" a partir de verbos e prefixos consolidados, como "Get", "Find", "enumeration", "index" etc. Escrevendo em português podem ser gerados nomes esdrúxulos como "GetMotorista", "FindEndereco", "carroCollection" etc, que me soam estranhos.