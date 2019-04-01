# challenge-dev - Desafio para backend Software Engineer

Segue o resumo de execução do meu teste:

Tempo de desenvolvimento: 12h00min.

Executar o migrator para criacao do banco de dados.

Para testar o serviço do GoogleMaps, e necessario configurar uma chave valida no arquivo `appsettings.Development.json` ou `appsettings.json`:

```
"GoogleMapsConfigurations": {
    "BaseUrl": "https://maps.google.com/maps/",
    "ApiUrlAddress": "api/geocode/json",
    "Key": "Insira a chave aqui"
  }
```

A API está documentada via Swagger.

### Arquitetura da solução

A aruitetura utlizada para solucao porposta foi o design pattern DDD e TDD baseada em micro servicos:

 - **`Wappa.Middleware.Domain`**: Entidades e objetos comuns.
 - **`Wappa.Middleware.EntityFrameworkCore`**: EntityFramework Core com SqlServer.
 - **`Wappa.Middleware.Core`**: Regras de negocio e consome o repositorio.
 - **`Wappa.Middleware.Application`**: Regras de validacao e consome o servico(core).
 - **`Wappa.Middleware.Host`**: Web API e documentação via Swagger.
 - **`Wappa.Middleware.Application.Tests`**: Testes unitários.

### Ferramentas utilizadas:

 - AspNetCore 2.2;
 - EntityFramework Core 2.2;
 - AutoMapper;
 - FluentValidation;
 - Geocoding.Google;
 - Swashbucle.AspNetCore;
 - xUnit;
