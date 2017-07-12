# challenge-dev-senior

## Alterações
Feito por Jônatas Piazzi

### Sobre o Projeto
 - Utilizado: Visual Studio 2017 Community
 - .Net 4.5.2
 - C# 6

## **Arquitetura**

### **DriverMgr.Api**
 
Este projeto é o WebAPI que esporta o serviço como rest.
> Destaque ao arquivo: ~/App_Start/DataFactoryProvider.cs

### **DriverMgr.Business**
 
Este projeto é o coração do sistema, aqui devem ser postas todas as regras de negócio necessárias. 
Foi feito um módulo de validação de objetos por DataAnnotation o que simplifica reduz grande parte de código.
> Olhar método: `DriverBL.ValidateDriver`
> E classe: `ValidatorBase`.

### **DriverMgr.DataAccess**
 
Para garantir baixo acoplamento e escalabilidade e IoC foi utilizado Factory Pattern.
Sendo assim a classe DataFactory serve como injetora de dependência para qualquer classe dentro da Business.

## **Sobre Google Maps API.**

O Google Maps API é uma API baseada em javascript. Portanto a utilização desta API seria implementada na aplicação que consumir o WebAPI. (talvez uma Web App Single Page, ex: AngularJS). Tudo que seria transmitido para o WebAPI seriam os valores de longitude e latitude (o objeto DriverTO já tem essas propriedades).

Como a implementação dessa API é rasoavelmente simples (Eu já utilizei a mesma em 2 projetos anteriores) e não foi exigido uma aplicação Front-End, eu não implementei essa funcionalidade.

## **Sobre o Tempo Consumido.**

Eu iniciei o projeto as 20:40 e terminei as 24:03, portanto foram gastos 3:23 min. Caso fosse ser criado uma aplicação de apresentação com o Google Maps API eu calcularia que demoraria mais 1:20 minutos.