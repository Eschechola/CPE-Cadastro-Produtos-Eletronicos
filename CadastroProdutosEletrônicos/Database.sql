USE CADASTRO_PRODUTOS_ELETRONICOS;

CREATE SCHEMA cpe;

CREATE TABLE [cpe].[produtos](
	id int PRIMARY KEY IDENTITY(1, 1),
	nome_produto varchar(1024),
	codigo_barras varchar(300),
	valor_unitario int
);


select * from cpe.produtos;


insert into cpe.produtos values ('produto1', '000 000 000', 20);


