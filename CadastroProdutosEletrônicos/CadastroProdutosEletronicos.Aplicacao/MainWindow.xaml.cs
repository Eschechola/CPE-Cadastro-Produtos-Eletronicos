using CadastroProdutosEletronicos.Servico.Servicos;
using System.Windows;
using System.Windows.Controls;

namespace CadastroProdutosEletronicos.Aplicacao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CarregarLista(null, null);
        }

        private void CarregarLista(object sender, RoutedEventArgs e)
        {
            var listaDeProdutos = new ProdutoServico().PegarTodosProdutos();

            //carrega a lista de produtos
            lv_produtos.ItemsSource = listaDeProdutos;

            //carrega a quantidade de produtos
            txt_quantidade_produtos.Text = "Qt. de Produtos: " + listaDeProdutos.Count;
        }

        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            //pega a opção selecionada no combo box (nome, cod de barras)
            var opcao = combo_opcao_de_pesquisa.Text;

            //pega o termo digitado no input
            var termo = input_termo.Text;

            //realiza a pesquisa de acordo com o termo e opçao selecionada
            switch (opcao)
            {
                case "Nome":
                    lv_produtos.ItemsSource = new ProdutoServico().BuscarPorNome(termo);
                    break;

                case "Código de barras":
                    lv_produtos.ItemsSource = new ProdutoServico().BuscarPorCodigoDeBarras(termo);
                    break;
            }
        }

        private void ChamarPaginaAdicionar(object sender, RoutedEventArgs e)
        {
            var janela = new Window();
            janela.Content = new Produto();
            janela.Show();
        }



        private void ExcluirProduto(object sender, RoutedEventArgs e)
        {
            //pega o id do produto
            string idProduto = ((Button)sender).Tag.ToString();

            //pega o produto de acordo com o ID
            var produto = new ProdutoServico().PegarDadosProduto(int.Parse(idProduto));

            //modal perguntando se realmente deseja excluir o produto
            var opcao = MessageBox.Show("Você realmente deseja excluir o produto: " + produto.NomeProduto, "AVISO!", MessageBoxButton.YesNo);

            //caso a resposta seja sim
            //exclui o produto
            switch (opcao)
            {
                case MessageBoxResult.Yes:
                    new ProdutoServico().DeletarProduto(int.Parse(idProduto));
                    break;
            }

            //recarrega a lista de produtos
            CarregarLista(null, null);
        }

        private void VisualizarProduto(object sender, RoutedEventArgs e)
        {
            //pega o id do produto
            string idProduto = ((Button)sender).Tag.ToString();

            var janela = new Window();
            janela.Content = new Produto(int.Parse(idProduto));
            janela.Show();
        }

        private void AlterarProduto(object sender, RoutedEventArgs e)
        {
            //pega o id do produto
            string idProduto = ((Button)sender).Tag.ToString();

            var janela = new Window();
            janela.Content = new Produto(int.Parse(idProduto), true);
            janela.Show();
        }
    }
}
