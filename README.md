# challenge-dev

## Como executar?

### Via Docker com a imagem remota (Recomendado - Testes incluidos no build)
1 - Abra o Terminal:
``` 
docker run --env GEOCODING_API_KEY={Sua chave de acesso da GeocodingAPI do Google} -p 3000:80 -it danilooliveira28/challenge-dev:latest
```

2 - http://localhost:3000/swagger/index.html


### Via Docker com a imagem construida localmente (Não Recomendado - Alto volume de download - Testes incluidos no build)
1 - Abra o Terminal

2 - Vá até a raiz do projeto:
```
docker image build .
docker run --env GEOCODING_API_KEY={Sua chave de acesso da GeocodingAPI do Google} -p 3000:80 -it {id da imagem gerada}
```

3 - http://localhost:3000/swagger/index.html


### Via .NET Core SDK 2.1 (Não Recomendado - Complexidade de ambiente - Testes executados a parte)
Testes:

1 - Abra o Terminal

2 - Vá até a raiz do projeto:
```
cd Domain.Test
dotnet test
```

Execução:

1 - Popular a variavel de ambiete GEOCODING_API_KEY com a sua chave de acesso da GeocodingAPI do Google

2 - Abra o Terminal

3 - Vá até a raiz do projeto:
```
cd ChallengeDev
dotnet ef migrations add InitialCreate
dotnet ef database update
cd ..
dotnet publish -c Release -o out
cd ChallengeDev
cp ChallengeDev.db out/ChallengeDev.db
cd out
dotnet ChallengeDev.dll
```

4 - http://localhost:5000/swagger/index.html


## Objetivo
Objetivo deste teste é avaliar como você irá considerar questões como arquitetura e design de software, modelagem e aplicação de técnicas e conceitos de programação, e não simplesmente resolver o problema proposto, visto que o mesmo não oferece dificuldades reais para implementação.  
Faça um fork deste projeto e ao concluir envie um pull request com sua implementação. Ao enviar o pull request nos informe quanto tempo você levou para desenvolver a solução.

## Escopo
Precisamos de uma biblioteca para gerenciar o cadastro de motoristas.  
Front-end não é necessário e não será avaliado, mas precisamos de uma API que permita criar, editar e excluir um motorista.  
Um cadastro de motorista possui os campos: Nome (primeiro e último), Carro (marca, modelo e placa) e endereço completo. Adicionamente, ao cadastrar um endereço deve ser buscada as coordenadas utilizando a [API do Google Maps](https://developers.google.com/maps/documentation/geocoding) e elas devem ser armazenadas junto com o cadastro.  
Além do cadastro também será necessário disponibilizar uma listagem dos cadastros em ordem alfabética por nome ou sobrenome.

### Obrigatório
 - .NET Core - C#
 - WebAPI
 - IoC
 - Código limpo

### Desejável
 - Documentação da API
 - Testes
 - Instruções de setup para execução do projeto
