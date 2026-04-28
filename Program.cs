using Microsoft.AspNetCore.Authentication.Cookies;
using NeaStyleOficial.Data;
using NeaStyleOficial.Services;
using NeaStyleOficial.Repositories;
using NeaStyleOficial.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registra o banco
builder.Services.AddDbContext<NeaStyleContext>();

// Registra os Repositories
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<AdministradorRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<CarrinhoRepository>();
builder.Services.AddScoped<FavoritoRepository>();
builder.Services.AddScoped<PagamentoRepository>();
builder.Services.AddScoped<ReembolsoRepository>();

// Registra os Services
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<AdministradorService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<FavoritoService>();
builder.Services.AddScoped<PagamentoService>();
builder.Services.AddScoped<ReembolsoService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/Index";
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NeaStyleContext>();
    if (!context.Produtos.Any())
    {
        context.Produtos.AddRange(
            new Produto { Nome = "Camiseta Gótica", Preco = 89.90m, CategoriaProduto.Feminino, TipoProduto.Camiseta, Tamanho.Produto.M, Cor = "Preto", EstoqueAtual = 10 },
            new Produto { Nome = "Calça Skate", Preco = 129.90m, CategoriaProduto.Masculino, TipoProduto.Calca, Tamanho.Produto.G, Cor = "Cinza", EstoqueAtual = 5 },
            new Produto { Nome = "Moletom Hip Hop", Preco = 159.90m, CategoriaProduto.Masculino, TipoProduto.Moletom, Tamanho.Produto.GG, Cor = "Preto", EstoqueAtual = 8 }
        );
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
