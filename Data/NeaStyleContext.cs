using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Models.Users;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;

namespace NeaStyleOficial.Data
{
    // O contexto do Entity Framework Core, que representa a sessão com o banco de dados e é usado pra consultar e salvar dados
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
        public DbSet<ItemPedido> ItensPedido { get; set; }

        //Uma tabela para carrinho de compras e favoritos, usando TPC pra herança
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }        
        public DbSet<ItemConjunto> ItensConjunto { get; set; }
        //Uma tabela pra pagamentos
        public DbSet<Pagamento> Pagamentos { get; set; }
        //Uma tabela para reembolsos        
        public DbSet<Reembolso> Reembolsos { get; set; }

        // Configuração do banco de dados (ex: SQL Server, SQLite, etc.) deve ser feita no Startup.cs ou Program.cs, então deixamos esse método vazio aqui
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }
        // Construtor que recebe as opções de configuração do contexto
        public NeaStyleContext(DbContextOptions<NeaStyleContext> options) : base(options)
        {
        }
        // Configurações adicionais do modelo, como mapeamento de herança, chaves estrangeiras, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*===============RELACIONAMENTOS ENTRE TABELAS====================== */
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
            // Relacionamento ItemPedido → Pedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                // Importante: quando um pedido for deletado, os itens relacionados também devem ser deletados, pra evitar itens órfãos
                .OnDelete(DeleteBehavior.Cascade);
            // Relacionamento de Pedido → Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId);
            // Relacionamento de Pagamento → Pedido
            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.Pagamentos)
                .HasForeignKey(p => p.PedidoId);
            // Relacionamento de Reembolso → Pedido
            modelBuilder.Entity<Reembolso>()
                .HasOne(r => r.Pedido)
                .WithMany(p => p.Reembolsos)
                .HasForeignKey(r => r.PedidoId);
            // Relacionamento Produto → ProdutoVariacao
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Variacoes)
                .WithOne(v => v.Produto)
                .HasForeignKey(v => v.ProdutoId);
            // Configurações específicas pra ProdutoVariacao, como conversão de enum pra string
            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Cor)
                .HasConversion<string>();
            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Tamanho)
                .HasConversion<string>();
            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.ImagemUrl)
                .HasConversion<string>();
            /*  ===============Nomes das tabelas no plural, que vai aparecer no banco de dados==================== */
            // Diz pro EF Core usar TPC pra hierarquia de Usuario, e pra ConjuntoProduto (Carrinho e Favorito)
            modelBuilder.Entity<Usuario>().UseTpcMappingStrategy();
            // Mapeia cada filha pra sua própria tabela
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");
            
            modelBuilder.Entity<ConjuntoProduto>().UseTpcMappingStrategy();
            modelBuilder.Entity<Carrinho>().ToTable("Carrinhos");
            modelBuilder.Entity<Favorito>().ToTable("Favoritos");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<ProdutoVariacao>().ToTable("ProdutoVariacoes");
            modelBuilder.Entity<ItemPedido>().ToTable("ItensPedido");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<ItemConjunto>().ToTable("ItensConjunto");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamentos");
            modelBuilder.Entity<Reembolso>().ToTable("Reembolsos");
        }
    }
}