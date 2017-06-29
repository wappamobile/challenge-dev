# challenge-dev-senior
Objetivo deste teste é avaliar como você irá considerar questões como arquitetura e design de software, modelagem e aplicação de técnicas e conceitos de programação, e não simplesmente resolver o problema proposto, visto que o mesmo não oferece dificuldades reais para implementação.  
Faça um fork deste projeto e ao concluir envie um pull request com sua implementação. Ao enviar o pull request nos informe quanto tempo você levou para desenvolver a solução.

## Escopo
Precisamos de uma biblioteca para gerenciar o cadastro de motoristas.  
Front-end não é necessário e não será avaliado, mas precisamos de uma API que permita criar, editar e excluir um motorista.  
Um cadastro de motorista possui os campos: Nome (primeiro e último), Carro (marca, modelo e placa) e endereço completo. Adicionamente, ao cadastrar um endereço deve ser buscada as coordenadas utilizando a [API do Google Maps](https://developers.google.com/maps/documentation/geocoding) e elas devem ser armazenadas junto com o cadastro.  
Além do cadastro também será necessário disponibilizar uma listagem dos cadastros em ordem alfabética por nome ou sobrenome.

### Obrigatório
 - .NET Core
 - WebAPI
 - IoC
 - Código limpo

### Desejável
 - Documentação da API
 - Testes
 - Instruções de setup para execução do projeto
