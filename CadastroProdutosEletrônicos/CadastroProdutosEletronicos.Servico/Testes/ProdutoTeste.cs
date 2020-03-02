using CadastroProdutosEletronicos.Dominio.Entidades.Entidades;
using CadastroProdutosEletronicos.Servico.Servicos;
using Microsoft.EntityFrameworkCore;
using System;

namespace CadastroProdutosEletronicos.Servico.Testes
{
    public class ProdutoTeste
    {
        public void Teste()
        {
            TesteInsert();
            TesteUpdate();
            TesteDelete();
            TestePegarProduto();
            TestePegarTodosProdutos();
            TesteBuscarPorNome();
            TesteBuscarPorCodigoDeBarras();
        }

        private void TesteInsert()
        {
            try
            {
                var produto1 = new Produtos
                {
                    NomeProduto = "Produto 1",
                    CodigoBarras = "111 111 111",
                    ValorUnitario = 1
                };

                var produto2 = new Produtos
                {
                    NomeProduto = "Produto 2",
                    CodigoBarras = "222 222 222",
                    ValorUnitario = 2
                };

                var produto3 = new Produtos
                {
                    NomeProduto = "Produto 3",
                    CodigoBarras = "333 333 333",
                    ValorUnitario = 3
                };

                new ProdutoServico().InserirProduto(produto1);
                new ProdutoServico().InserirProduto(produto2);
                new ProdutoServico().InserirProduto(produto3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TesteUpdate()
        {
            try
            {
                var produto1 = new Produtos
                {
                    Id = 3,
                    NomeProduto = "Produto Atualizado",
                    CodigoBarras = "123 123 123",
                    ValorUnitario = 4
                };

                new ProdutoServico().AtualizarProduto(produto1);
            }
            catch (DbUpdateConcurrencyException) { }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TesteDelete()
        {
            try
            {
                int id = 2;
                new ProdutoServico().DeletarProduto(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TestePegarProduto()
        {
            try
            {
                int id = 3;
                var produto = new ProdutoServico().PegarDadosProduto(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TestePegarTodosProdutos()
        {
            try
            {
                var produtos = new ProdutoServico().PegarTodosProdutos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        private void TesteBuscarPorNome()
        {
            string nome = "produto 1";
            var produtos = new ProdutoServico().BuscarPorNome(nome);
        }

        private void TesteBuscarPorCodigoDeBarras()
        {
            string codigoDeBarras = "111 111 111";
            var produtos = new ProdutoServico().BuscarPorCodigoDeBarras(codigoDeBarras);
        }
    }
}
