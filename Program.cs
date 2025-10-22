using EquipamentosApi.Data;
using EquipamentosApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=equipamentos.db"));

// Adicionar serviços
builder.Services.AddScoped<EquipamentoService>();

// Adicionar Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém PascalCase
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddControllersWithViews();

// Configuração do CORS (IMPORTANTE!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Inicializar o banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Cria o banco se não existir
    // Opcional: dbContext.Database.Migrate(); // Se estiver usando migrations
}

// ORDEM CORRETA DOS MIDDLEWARES (MUITO IMPORTANTE!)

// 1. CORS deve vir ANTES de tudo
app.UseCors("AllowAll");

// 2. Arquivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

// 3. Roteamento
app.UseRouting();

// 4. Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

// 5. Mapear os endpoints

// API Controllers (DEVE VIR PRIMEIRO!)
app.MapControllers();

// Rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EquipamentosView}/{action=Index}/{id?}");

// NÃO use MapFallbackToFile se você tem rotas MVC e API juntas
// app.MapFallbackToFile("index.html"); // REMOVA ou COMENTE esta linha!

app.Run();