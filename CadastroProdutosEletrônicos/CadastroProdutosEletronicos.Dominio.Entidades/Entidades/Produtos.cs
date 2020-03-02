namespace CadastroProdutosEletronicos.Dominio.Entidades.Entidades
{
    public partial class Produtos
    {
        public int Id { get; set; }
        public string? NomeProduto { get; set; }
        public string? CodigoBarras { get; set; }
        public int? ValorUnitario { get; set; }
        public string UrlImagem { get; set; }
    }
}
