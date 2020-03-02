using CadastroProdutosEletronicos.Dominio.Contexto.Contexto;
using CadastroProdutosEletronicos.Dominio.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CadastroProdutosEletronicos.Servico.Servicos
{
    public class ProdutoServico
    {
        private readonly SqlServerContext contexto = new SqlServerContext();

        public void InserirProduto(Produtos produto)
        {
            contexto.Add(produto);
            contexto.SaveChanges();
        }

        public void AtualizarProduto(Produtos produto)
        {
            contexto.Entry(produto).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void DeletarProduto(int id)
        {
            var produto = PegarDadosProduto(id);
            contexto.Produtos.Remove(produto);
            contexto.SaveChanges();
        }

        public Produtos PegarDadosProduto(int id)
        {
            var produto = contexto.Produtos.Where(x => x.Id == id).ToList();
            return produto.First();
        }

        public IList<Produtos> PegarTodosProdutos()
        {
            return contexto.Produtos.ToList();
        }

        public IList<Produtos> BuscarPorNome(string nome)
        {
            return contexto.Produtos.Where(x => x.NomeProduto.Contains(nome)).ToList();
        }

        public IList<Produtos> BuscarPorCodigoDeBarras(string codigoDeBarras)
        {
            return contexto.Produtos.Where(x => x.CodigoBarras.Contains(codigoDeBarras)).ToList();
        }
    }
}
