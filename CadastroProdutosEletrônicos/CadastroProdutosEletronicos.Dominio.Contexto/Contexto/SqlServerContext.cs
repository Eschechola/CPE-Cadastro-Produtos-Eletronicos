using System;
using CadastroProdutosEletronicos.Dominio.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CadastroProdutosEletronicos.Dominio.Contexto.Contexto 
{
    public partial class SqlServerContext : DbContext
    {
        public SqlServerContext()
        {
        }

        public SqlServerContext(DbContextOptions<SqlServerContext> options): base(options)
        {
        }

        public virtual DbSet<Produtos> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-652APCE\\SQLEXPRESS;Initial Catalog=CADASTRO_PRODUTOS_ELETRONICOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.ToTable("produtos", "cpe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoBarras)
                    .HasColumnName("codigo_barras")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.NomeProduto)
                    .HasColumnName("nome_produto")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UrlImagem)
                    .HasColumnName("url_imagem")
                    .HasMaxLength(200)
                    .IsUnicode(false);


                entity.Property(e => e.ValorUnitario).HasColumnName("valor_unitario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
