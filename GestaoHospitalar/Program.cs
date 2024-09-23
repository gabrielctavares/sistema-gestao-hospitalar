using GestaoHospitalar.Components;
using GestaoHospitalar.Services;
using MudBlazor.Services;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
{
    throw new InvalidOperationException("Database credentials are not set in the environment variables.");
}

var fullConnectionString = $"{connectionString}User ID={dbUser};Password={dbPassword};";

builder.Services.AddTransient(x =>
  new MySqlConnection(fullConnectionString));

builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IInternacaoService, InternacaoService>();
builder.Services.AddScoped<IEstoqueMedicamentoService, EstoqueMedicamentoService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
