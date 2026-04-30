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
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=NeaStyleDB;Integrated Security=True;
            Trust Server Certificate=True;");
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

            // Relacionamento ItemConjunto → Produto
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId);

            // Relacionamento ItemConjunto → ConjuntoProduto
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.Conjunto)
                .WithMany(c => c.Itens)
                .HasForeignKey(i => i.ConjuntoProdutoId);
            
            modelBuilder.Entity<Produto>().ToTable("Produtos");

            // SEEDING: Inserindo produtos de teste
            modelBuilder.Entity<Produto>().HasData(
                new Produto 
                { 
                    ProdutoId = 1, // Importante definir o ID manualmente no HasData
                    Nome = "Vestido Chique", 
                    PrecoCusto = 150.00m,
                    Preco = 299.90m, 
                    Descricao = "Vestido para formaturas e eventos.",
                    Cor = "Vermelho",
                    EstoqueAtual = 20,
                    ImagemUrl = "https://example.com/vestido-chique.jpg",
                    TamanhoProduto = TamanhoProduto.M,
                    TipoProduto = TipoProduto.Vestido,
                    CategoriaProduto = CategoriaProduto.Feminino
                },
                new Produto 
                { 
                    ProdutoId = 2,
                    Nome = "Calça Cargo", 
                    PrecoCusto = 160.00m,
                    Preco = 299.90m, 
                    Descricao = "Calça para o dia a dia.",
                    Cor = "Azul",
                    EstoqueAtual = 15,
                    ImagemUrl = "https://example.com/calca-cargo.jpg",
                    TamanhoProduto = TamanhoProduto.G,
                    TipoProduto = TipoProduto.Calca,
                    CategoriaProduto = CategoriaProduto.Masculino
                },
                new Produto 
                { 
                    ProdutoId = 3,
                    Nome = "Saia Jeans", 
                    PrecoCusto = 90.00m,
                    Preco = 120.00m, 
                    Descricao = "Saia estilosa e versátil.",
                    Cor = "Preto",
                    EstoqueAtual = 25,
                    ImagemUrl = "https://example.com/saia-jeans.jpg",
                    TamanhoProduto = TamanhoProduto.P,
                    TipoProduto = TipoProduto.Saia,
                    CategoriaProduto = CategoriaProduto.Feminino
                }
            );
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<ItemConjunto>().ToTable("ItensConjunto");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");
            modelBuilder.Entity<Reembolso>().ToTable("Reembolsos");
        }
    }
}