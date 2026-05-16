using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeaStyleOficial.Data;
using NeaStyleOficial.Services;
using NeaStyleOficial.Repositories;
using NeaStyleOficial.Models;
using NeaStyleOficial.Models.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NeaStyleContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<AdministradorRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<CarrinhoRepository>();
builder.Services.AddScoped<FavoritoRepository>();
builder.Services.AddScoped<PagamentoRepository>();
builder.Services.AddScoped<ReembolsoRepository>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<AdministradorService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<FavoritoService>();
builder.Services.AddScoped<PagamentoService>();
builder.Services.AddScoped<ReembolsoService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Login";
    });

var app = builder.Build();

await SeedAdminAsync(app);

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
}); 
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

async Task SeedAdminAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<NeaStyleContext>();

    if (!context.Administradores.Any())
    {
        var admin = new Administrador
        {
            Nome  = "Admin",
            Email = app.Configuration["Admin:Email"],
             Cargo = "Administrador" 
        };

        var hasher = new PasswordHasher<Administrador>();
        admin.Senha = hasher.HashPassword(admin, app.Configuration["Admin:Senha"]);

        context.Administradores.Add(admin);
        await context.SaveChangesAsync();
    }
}