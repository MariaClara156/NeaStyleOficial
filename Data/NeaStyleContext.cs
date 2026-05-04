using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Collections;

namespace NeaStyleOficial.Data
{
    public class NeaStyleContext : DbContext
    {
        // Uma tabela pra cada tipo concreto de usuário
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        // Uma tabela pra produtos
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoVariacao> ProdutoVariacoes { get; set; }

        //Uma tabela pra pedidos
        public DbSet<Pedido> Pedidos { get; set; }

        //Uma tabela para carrinho de compras e favoritos, usando TPC pra herança
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }        
        public DbSet<ItemConjunto> ItensConjunto { get; set; }
        //Uma tabela pra pagamentos
        public DbSet<Pagamento> Pagamentos { get; set; }
        //Uma tabela para reembolsos        
        public DbSet<Reembolso> Reembolsos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        public NeaStyleContext(DbContextOptions<NeaStyleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Diz pro EF Core usar TPC pra hierarquia de Usuario, e pra ConjuntoProduto (Carrinho e Favorito)
            modelBuilder.Entity<Usuario>().UseTpcMappingStrategy();
            // Mapeia cada filha pra sua própria tabela
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");
            
            modelBuilder.Entity<ConjuntoProduto>().UseTpcMappingStrategy();
            modelBuilder.Entity<Carrinho>().ToTable("Carrinhos");
            modelBuilder.Entity<Favorito>().ToTable("Favoritos");

            // Relacionamento ItemConjunto → ProdutoVariacao
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.ProdutoVariacao)
                .WithMany()
                .HasForeignKey(i => i.ProdutoVariacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemConjunto → ConjuntoProduto
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.Conjunto)
                .WithMany(c => c.Itens)
                .HasForeignKey(i => i.ConjuntoProdutoId);
            
            // Relacionamento Produto → ProdutoVariacao
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Variacoes)
                .WithOne(v => v.Produto)
                .HasForeignKey(v => v.ProdutoId);

            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Cor)
                .HasConversion<string>();

            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Tamanho)
                .HasConversion<string>();
            
            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.ImagemUrl)
                .HasConversion<string>();
            
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<ProdutoVariacao>().ToTable("ProdutoVariacoes");

            modelBuilder.Entity<Administrador>().HasData(
                new Administrador
                {
                    UsuarioId = 1,
                    Nome = "Admin Master",
                    Email = "mariaclara4290@gmail.com",
                    Senha = "admin123",
                    Cargo = "Gerente"
                }
            );
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<ItemConjunto>().ToTable("ItensConjunto");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");
            modelBuilder.Entity<Reembolso>().ToTable("Reembolsos");
        }
    }
}