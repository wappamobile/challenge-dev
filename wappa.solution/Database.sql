USE master
GO

CREATE DATABASE Wappa
GO

USE Wappa
GO

create table Marca (
	ID int identity not null primary key,
	Descricao varchar(100) not null
)
go

create table Modelo (
	ID int identity not null primary key,
	Descricao varchar(100) not null,
	MarcaID int not null references Marca(ID)
)
go

create table Carro (
	ID int identity not null primary key,
	ModeloID int not null references Modelo(ID),
	Placa varchar(8) not null
)
go


create table Endereco (
	ID int identity not null primary key,
	CEP varchar(9) not null,
	Logradouro varchar(100) not null,
	Numero int not null,
	Complemento varchar(20),
	Bairro varchar(50) not null,
	Cidade varchar(50) not null,
	Estado varchar(50) not null,
	Latitude float not null,
	Longitude float not null
)
go

create table Motorista (
	ID int identity not null primary key,
	PrimeiroNome varchar(20),
	UltimoNome varchar(100),
	CarroID int not null references Carro(ID),
	EnderecoID int not null references Endereco(ID)
)
go

