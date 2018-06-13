# Intro

Queria agradecer ao pessoal da Wappa pela oportunidade, espero que gostem do projeto e espero poder me juntar ao time :metal:!

[Aqui](https://documenter.getpostman.com/view/1543073/RWEcRhAw) tem a documentacao da api e ela tambem esta [Live](https://wappaapirodrigoalencar.azurewebsites.net) neste site.

Espero o contato de voces! =)


# Como fazer o projeto funcionar

Assumindo que o git clone ja tenha sido feito basta seguir esses passos:

## Antes de tudo...
Voce vai precisar criar um user secret...
Pra que isso? Assim voce nao precisa ficar preocupado de alguem usar sua chave de API do Google ou de subir por engano no GitHub
E so seguir essa receita que voce ficara bem...rs

### Metodo 1
Se voce estiver usando Visual Studio vai ter que seguir esta liga de passos enorme:

1.  click direito sobre o projeto `Wappa.Api`
2.  click na opcao `Manage User Secrets`
3.  o VS vai abrir um arquivo `json`
4.  coloque o seguinte conteudo neste arquivo 
`{
  "APIs": {
    "Google_Geocoding_API": {
      "Key": "SUA CHAVE DO GOOGLE"
    }
  }
}`
5. salve e pronto

### Metodo 2
Aqui voce vai suar um pouco mas vamos la:

1. va ate a pasta onde se encontra o `Wappa.Api.csproj` abra ele com seu editor de texto favorido ~~VSCode~~ :smile:
2. crie um guid com qualquer ferramenta que quiser
3. adicione isso `<UserSecretsId>Seu Guid</UserSecretsId>` ao `csproj` logo abaixo da tag `TargetFramework`
4. abra um shell no diretorio do `csproj` e execute `dotnet user-secrets APIs:Google_Geocoding_API:Key = "SUA CHAVE DA API DO GOOGLE"`
5. Esse e so pra lista parecer grande, parabens.

## Criando o database da aplicacao

Antes verifique a `connection string` da aplicacao, tenha certeza de que a senha o usuario e a instancia do SQL sao validas

### Metodo 1
1. va ate a pasta onde se encontra o `Wappa.Api.sln`
2. execute `dotnet restore`
3. entre no diretorio onde se encontra o `Wappa.Api.csproj`
4. excute `dotnet ef migrations list` a saida desse comando deve trazer por ultimo algo igual a isso `20180612212614_DatabaseBootstrap`
5. Se chegou ate aqui nao desista... =)
6. execute `dotnet ef database update`

### Metodo 2
1. va ate a pasta do projeto `Wappa.Api.DataLayer`
2. dentro voce encontrara uma pasta `SQL`
3. execute o script via `SQL Management` e ele vai fazer tudo

## Build & Run :running: :see_no_evil:

Agora a parte boa:

1. no diretorio `Wappa.Api`
2. execute `dotnet build -o app`
3. e agora execute `dotnet app\Wappa.Api.dll`
