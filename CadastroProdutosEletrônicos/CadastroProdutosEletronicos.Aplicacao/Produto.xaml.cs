using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System;
using System.Windows.Media.Imaging;
using CadastroProdutosEletronicos.Dominio.Entidades.Entidades;
using CadastroProdutosEletronicos.Servico.Servicos;

namespace CadastroProdutosEletronicos.Aplicacao
{
    /// <summary>
    /// Interação lógica para AdicionarProduto.xam
    /// </summary>
    public partial class Produto : Page
    {
        private string UrlFoto;
        private int IdProduto;

        public Produto()
        {
            InitializeComponent();
            CarregarImagemPadrao();
        }

        public Produto(int idProduto)
        {
            InitializeComponent();

            var produto = new ProdutoServico().PegarDadosProduto(idProduto);

            input_nome.Text = produto.NomeProduto;
            input_nome.IsReadOnly = true;

            input_codigo.Text = produto.CodigoBarras;
            input_codigo.IsReadOnly = true;

            input_quantidade.Text = produto.ValorUnitario.ToString();
            input_quantidade.IsReadOnly = true;

            CarregarImagemPadrao(produto.UrlImagem);
            lb_imagem.Drop -= CarregarFoto;

            btn_salvar_produto.IsEnabled = false;
        }

        public Produto(int idProduto, bool alterar)
        {
            InitializeComponent();

            if (alterar)
            {
                IdProduto = idProduto;

                var produto = new ProdutoServico().PegarDadosProduto(idProduto);

                input_nome.Text = produto.NomeProduto;
                input_codigo.Text = produto.CodigoBarras;
                input_quantidade.Text = produto.ValorUnitario.ToString();
                CarregarImagemPadrao(produto.UrlImagem);
                btn_salvar_produto.Click -= SalvarProduto;
                btn_salvar_produto.Click += AtualizarProduto;
            }
        }

        private void ValidarQuantidade(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CarregarImagemPadrao(string urlImagem = "")
        {
            string urlImagemPadrao;

            //link do caminho fisico da imagem padrão
            if (urlImagem != "")
            {
                urlImagemPadrao = urlImagem;
            }
            else
            {
                 urlImagemPadrao = (Directory.GetCurrentDirectory() + @"\Conteudo\Imagens\Aplicacao\imagem-padrao.jpg");
            }

            //salva a url padrão
            UrlFoto = urlImagemPadrao;

            //cria uma bitmap imagem
            BitmapImage imagem = new BitmapImage();

            //inicia a imagem
            imagem.BeginInit();

            //define a URI da imagem (caminho)
            imagem.UriSource = new Uri(urlImagemPadrao);

            //fecha a imagem
            imagem.EndInit();

            //carrega imagem padrão de um produto no layout
            img_padrao.Source = imagem;
        }

        private void CarregarFoto(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileName"))
            {
                string diretorioFisicoArquivo = (e.Data.GetData("FileName") as string[])[0];
                UrlFoto = diretorioFisicoArquivo;
            }

            CarregarImagemPadrao(UrlFoto);
        }


        private string SalvarArquivo()
        {
            var nomeFotoSalvar = Guid.NewGuid().ToString().Substring(0,10);
            var diretorioSalvar = (Directory.GetCurrentDirectory() + @"\Conteudo\Imagens\Produtos\") + nomeFotoSalvar + ".png";
            File.Copy(UrlFoto, System.IO.Path.Combine(UrlFoto, diretorioSalvar));

            return diretorioSalvar;
        }

        private void AtualizarProduto(object sender, RoutedEventArgs e)
        {
            string nomeProduto = input_nome.Text;
            string codigoBarras = input_codigo.Text;
            string quantidadeUnitaria = input_quantidade.Text;


            if (string.IsNullOrEmpty(nomeProduto))
            {
                MessageBox.Show("Por favor insira o nome do produto", "AVISO!");
            }
            else if (string.IsNullOrEmpty(codigoBarras))
            {
                MessageBox.Show("Por favor insira o código de barras do produto", "AVISO!");
            }
            else if (string.IsNullOrEmpty(quantidadeUnitaria))
            {
                MessageBox.Show("Por favor insira a quantidade do produto", "AVISO!");
            }
            else
            {

                //salva a imagem
                var urlImagem = SalvarArquivo();

                //cria o produto que será salvo
                var produtoSalvar = new Produtos
                {
                    Id = IdProduto,
                    NomeProduto = nomeProduto,
                    CodigoBarras = codigoBarras,
                    ValorUnitario = int.Parse(quantidadeUnitaria),
                    UrlImagem = urlImagem
                };

                //salva o produto
                new ProdutoServico().AtualizarProduto(produtoSalvar);

                MessageBox.Show("Produto atualizado com sucesso", "SUCESSO!");

                //volta para a página inicial
                VoltarPaginaInicial(null, null);
            }
        }

        private void SalvarProduto(object sender, RoutedEventArgs e)
        {
            string nomeProduto = input_nome.Text;
            string codigoBarras = input_codigo.Text;
            string quantidadeUnitaria = input_quantidade.Text;


            if (string.IsNullOrEmpty(nomeProduto))
            {
                MessageBox.Show("Por favor insira o nome do produto", "AVISO!");
            }
            else if (string.IsNullOrEmpty(codigoBarras))
            {
                MessageBox.Show("Por favor insira o código de barras do produto", "AVISO!");
            }
            else if (string.IsNullOrEmpty(quantidadeUnitaria))
            {
                MessageBox.Show("Por favor insira a quantidade do produto", "AVISO!");
            }
            else
            {

                //salva a imagem
                var urlImagem = SalvarArquivo();

                //cria o produto que será salvo
                var produtoSalvar = new Produtos
                {
                    NomeProduto = nomeProduto,
                    CodigoBarras = codigoBarras,
                    ValorUnitario = int.Parse(quantidadeUnitaria),
                    UrlImagem = urlImagem
                };

                //salva o produto
                new ProdutoServico().InserirProduto(produtoSalvar);

                MessageBox.Show("Produto inserido com sucesso", "SUCESSO!");

                //volta para a página inicial
                VoltarPaginaInicial(null, null);
            }
        }

        private void VoltarPaginaInicial(object sender, RoutedEventArgs e)
        {
            Window janela = (Window)this.Parent;
            janela.Close();
        }
    }
}
