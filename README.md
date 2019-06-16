# Code Challenge Grupo Zap

Para realizar o desafio utilizei o Visual Studio 2019 e criada uma API em ASP.NET Core 2.0 utilizando MediatR.

# Sobre o código

O projeto está basicamente dividido em três componentes principais:

- Motoristas: contém a API que será exposta
- Motorista.Core: contém as entidades de negócio, mapeamentos e as interfaces de Repositório e Serviço de Geolocalização
- Motoristas.Handlers: contém os commands da API, validações e os handlers de implementação dos comandos
- Interface com API de Geolocalização do Google implementada no componentes GoogleGeolocationService
- Projeto MicroservicesPlatform e Motoristas.Helpers dão suporte a infra estrutura necessária para implementação do microserviço e classes úteis sem implementação de regra de negócio
- Motoristas.Core.Data.MongoDB: contém a implementação do DAO (Utilizando MongoDB)

# Sobre os testes

- Foram implementados dois testes unitários utilizando XUnit: para testar os handlers e para testar o componente responsável pela interface com o Google Geocode API.
- Na pasta Postman os testes das API's com Json de entrada.

# Sobre o tempo de desenvolvimento:

Início do desenvolvimento: 11/06 (3 horas)
Dia 2: 13/06 (3 horas)
Dia 3: 14/06 (3 horas)
Dia 4: 15/06 (4 horas)
Último dia de entrega: 16/06 (3 horas)

