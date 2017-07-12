# challenge-dev-senior

## Altera��es
Feito por J�natas Piazzi

### Sobre o Projeto
 - Utilizado: Visual Studio 2017 Community
 - .Net 4.5.2
 - C# 6

## **Arquitetura**

### **DriverMgr.Api**
 
Este projeto � o WebAPI que esporta o servi�o como rest.
> Destaque ao arquivo: ~/App_Start/DataFactoryProvider.cs

### **DriverMgr.Business**
 
Este projeto � o cora��o do sistema, aqui devem ser postas todas as regras de neg�cio necess�rias. 
Foi feito um m�dulo de valida��o de objetos por DataAnnotation o que simplifica reduz grande parte de c�digo.
> Olhar m�todo: `DriverBL.ValidateDriver`
> E classe: `ValidatorBase`.

### **DriverMgr.DataAccess**
 
Para garantir baixo acoplamento e escalabilidade e IoC foi utilizado Factory Pattern.
Sendo assim a classe DataFactory serve como injetora de depend�ncia para qualquer classe dentro da Business.

## **Sobre Google Maps API.**

O Google Maps API � uma API baseada em javascript. Portanto a utiliza��o desta API seria implementada na aplica��o que consumir o WebAPI. (talvez uma Web App Single Page, ex: AngularJS). Tudo que seria transmitido para o WebAPI seriam os valores de longitude e latitude (o objeto DriverTO j� tem essas propriedades).

Como a implementa��o dessa API � rasoavelmente simples (Eu j� utilizei a mesma em 2 projetos anteriores) e n�o foi exigido uma aplica��o Front-End, eu n�o implementei essa funcionalidade.

## **Sobre o Tempo Consumido.**

Eu iniciei o projeto as 20:40 e terminei as 24:03, portanto foram gastos 3:23 min. Caso fosse ser criado uma aplica��o de apresenta��o com o Google Maps API eu calcularia que demoraria mais 1:20 minutos.