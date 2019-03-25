Sr. Avaliador segue o resumo de execução do meu teste:

Tempo total de desenvolvimento: 7h10min.

Para testar a criação de motoristas é necessário configurar uma chave de API válida para o serviço do GoogleMaps, isso deve ser feito editando o arquivo `appsettings.Development.json` se for ser executado em modo de desenvolvimento e `appsettings.json` se for ser executado em modo de produção:

```json
"GoogleGeocoder": {
    "ApiKey": "<<please insert a valid API KEY here>>"
}
```

Para testar a solução basta abrir no Visual Studio 2017+ e uma vez restaurado todas as dependências, executar o projeto. A API está documentada via Swagger e via XML comments.

### Organização da solução

A solução foi codificada com nomenclatura em Inglês para permitir o desenvolvimento/manutenção por times multinacionais e foi dividida em vários projetos respeitando o padrão DDD (*Domain Driven Design*):

 - **`WappaMobile.Domain`**: Entidades e exceções que são comuns a todo domínio: `Driver`, `Address`, `Car` e `Coordinates`.
 - **`WappaMobile.Persistence`**: Camada de persistência, implementada utilizando EntityFramework Core utilizando um banco de dados em memória.
 - **`WappaMobile.Application`**: Lógica de negócio estruturada de acordo com o respeitando CQRS (*Command Query Responsibility Segregation*) utilizando o Mediator Pattern.
 - **`WappaMobile.WebAPI`**: Camada de apresentação utilizando Web API incluindo documentação via Swagger.
 - **`WappaMobile.Infrastructure.GoogleGeocoding`**: Serviço de geocoding injetável utilizando GoogleMaps APIs.
 - **`WappaMobile.Application.Tests`** e **`WappaMobile.WebAPI.Tests`**: Testes unitários para camada de lógica de negócio e de apresentação.

### Ferramentas utilizadas:

 - AspNetCore 2.2;
 - EntityFramework Core 2.2;
 - AutoMapper;
 - MediatR;
 - FluentValidation;
 - Geocoding.Google;
 - Moq;
 - Health checks;
 - ProblemDetails ([RFC 7807](https://tools.ietf.org/html/rfc7807));
 - Swashbucle.AspNetCore; e
 - xUnit;

### Screenshots

 ![Screenshot 1](/images/screenshot1.png)
 ![Screenshot 2](/images/screenshot2.png)
 ![Screenshot 3](/images/screenshot3.png)
