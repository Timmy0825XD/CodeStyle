using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using BLL.Implementaciones;
using BLL.Interfaces;
using DAL.Implementaciones;
using DAL.Interfaces;
using JOSSAN.Components;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar Wallet PRIMERO, antes de cualquier conexión
var walletLocation = builder.Configuration["OracleWallet:Location"]!;
OracleConfiguration.TnsAdmin = walletLocation;
OracleConfiguration.WalletLocation = walletLocation;

// Obtener la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("OracleConnection")!;

// ===== REGISTRAR SERVICIOS DE LA CAPA DAL =====
builder.Services.AddScoped<IUsuarioDAO>(sp => new UsuarioDAO(connectionString));
builder.Services.AddScoped<IRolDAO>(sp => new RolDAO(connectionString));

// ===== REGISTRAR SERVICIOS DE LA CAPA BLL =====
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();