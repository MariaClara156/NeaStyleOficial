using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Models.Catalog;
using NeaStyleOficial.Models.Collections;
using NeaStyleOficial.Models.Sales;
using NeaStyleOficial.Models.Users;

namespace NeaStyleOficial.Data
{
    // Representa a sessão com o banco de dados, usado para consultar e salvar dados
    public class NeaStyleContext : DbContext
    {
        // Uma tabela para cada tipo concreto de usuário
        public DbSet<Cliente>       Clientes       { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        // Uma tabela para produtos
        public DbSet<Produto>         Produtos         { get; set; }
        public DbSet<ProdutoVariacao> ProdutoVariacoes { get; set; }

        // Uma tabela para pedidos
        public DbSet<Pedido>     Pedidos     { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        // Uma tabela para carrinho e favoritos, usando TPC para herança
        public DbSet<Carrinho>      Carrinhos    { get; set; }
        public DbSet<Favorito>      Favoritos    { get; set; }
        public DbSet<ItemConjunto>  ItensConjunto { get; set; }

        // Uma tabela para pagamentos e reembolsos
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Reembolso> Reembolsos { get; set; }

        // Construtor que recebe as opções de configuração do contexto
        public NeaStyleContext(DbContextOptions<NeaStyleContext> options) : base(options) { }

        // Configuração do banco de dados (ex: SQL Server, SQLite) deve ser feita no Program.cs
        protected override void OnConfiguring(DbContextOptionsBuilder options) { }

        // Configurações adicionais do modelo: mapeamento de herança, chaves estrangeiras, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ==================== RELACIONAMENTOS ====================

            // ItemConjunto → ProdutoVariacao
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.ProdutoVariacao)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.ProdutoVariacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemConjunto → Carrinho
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.Carrinho)
                .WithMany(c => c.Itens)
                .HasForeignKey(i => i.CarrinhoId);

            // ItemConjunto → Favorito
            modelBuilder.Entity<ItemConjunto>()
                .HasOne(i => i.Favorito)
                .WithMany(f => f.Itens)
                .HasForeignKey(i => i.FavoritoId);

            // ItemPedido → Pedido (cascade: itens órfãos são deletados junto com o pedido)
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pedido → Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId);

            // Pagamento → Pedido
            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.Pagamentos)
                .HasForeignKey(p => p.PedidoId);

            // Reembolso → Pedido
            modelBuilder.Entity<Reembolso>()
                .HasOne(r => r.Pedido)
                .WithMany(p => p.Reembolsos)
                .HasForeignKey(r => r.PedidoId);

            // Produto → ProdutoVariacao
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Variacoes)
                .WithOne(v => v.Produto)
                .HasForeignKey(v => v.ProdutoId);

            // ==================== CONVERSÕES DE ENUM ====================

            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Cor)
                .HasConversion<string>();

            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.Tamanho)
                .HasConversion<string>();

            modelBuilder.Entity<ProdutoVariacao>()
                .Property(v => v.ImagemUrl)
                .HasConversion<string>();

            // ==================== MAPEAMENTO TPC E NOMES DE TABELAS ====================

            // TPC para hierarquia de Usuario — cada filha mapeia para sua própria tabela
            modelBuilder.Entity<Usuario>().UseTpcMappingStrategy();
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");

            // TPC para hierarquia de ConjuntoProduto
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