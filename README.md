# Visão Geral
Api para gerenciamento de cadastro de motoristas.

Essa aplicação foi desenvolvida com propósito exclusivo de estudo. Ela utiliza .NET CORE 2.1, .NET Standard e .NET Framework 4.7 e os dados são armazenados em memória. Ou seja, são voláteis e não irão persistir após o aplicativo ser parado ou reiniciado no servidor.

A arquitetura tem como base o estilo proposto pelo Domain-Driven Design tendo bastante foco no DomainModel, Entidade, Objeto de Valor e Linguagem Ubíqua.

Diversos design patterns foram utilizados em vários trechos dos códigos, no entanto sem exageros. É fácil perceber princípios SOLID principalmente no DomainModel e Infrastructure.

Todo o codigo em sí, preza pela simplicidade e legibilidade e se orienta por métricas de baixo acoplamento e complexidade ciclomática.

# Get Start

1. Clone o código para seu computador.
2. Abra a Solution usando Visual Studio 2017+
3. No menu do Visual Studio, clique em Build / Clean Solution.
4. Defina o projeto WappaMobile.ChallengeDev.WebApi como Startup (clique sobre o projeto com o botão direito em seguida escolha Set as Startup Project)
5. No projeto WappaMobile.ChallengeDev.GoogleMaps, altere o valor da constant API_KEY da classe Settings informando a sua Chave de Api fornecida pelo Google.6. Execute a Solution apertando F5.

# End Points

### POST /api/motoristas
Inclusão de motoristas no banco de dados.

Body (application/json)
```javascript
{
	nome:
	{
		primeiro:"",
		ultimo:""
	},
	carro:
	{
		marca:"",
		modelo:"",
		placa:
		{
			letras:"AAA",
			numeros:"0000"
		}
	},
	endereco:
	{
		tipo:"Rua",
		logradouro:"",
		numero:"",
		bairro:"",
		cidade:"",
		estado:"",
		uf:"",
		cep:""
	}
}
```

### PUT /api/motoristas/{id}
Atualização dos dados de um motorista existente através do seu ID.

Body (application/json)
```javascripty
{
	nome:
	{
		primeiro:"",
		ultimo:""
	},
	carro:
	{
		marca:"",
		modelo:"",
		placa:
		{
			letras:"AAA",
			numeros:"0000"
		}
	},
	endereco:
	{
		tipo:"Rua",
		logradouro:"",
		numero:"",
		bairro:"",
		cidade:"",
		estado:"",
		uf:"",
		cep:"
	}
}
```

### GET /api/motoristas
Listagem de todos os motoristas cadastrados.

### GET /api/motoristas?orderby={campo}
Listagem de todos os motoristas de forma ordenada.

<table>
<tr>
<th>Parametro</th>
<th>Descrição</th>
<th>Valores esperados</th>
</tr>

<tr>
<td>orderby</td>
<td>Define qual o campo será utilizado como referência para a ordenação.</td>
<td>nome / sobrenome</td>
</tr>
</table>

### DELETE /api/motoristas/{id}
Exclusão de um motorista através de seu ID.