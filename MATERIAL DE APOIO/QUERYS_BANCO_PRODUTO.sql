CREATE DATABASE CADASTRO_PRODUTOS;

CREATE SCHEMA cpp;

CREATE TABLE [cpp].[produtos](
	id int PRIMARY KEY IDENTITY(1,1),
	nome_produto varchar(300),
	codigo_barras varchar(20),
	valor_unitario int,
	url_imagem varchar(200)
);

SELECT * FROM [cpp].[produtos];

INSERT INTO [cpp].[produtos] VALUES
('Produto 1', '111 111 111', 5, '');