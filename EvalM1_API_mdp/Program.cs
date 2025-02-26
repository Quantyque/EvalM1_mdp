using EvalM1_API_mdp;
using EvalM1_API_mdp.DAO;
using EvalM1_API_mdp.Data.DAO.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

// Ajouter Swagger à la collection de services
builder.Services.AddEndpointsApiExplorer(); // Pour Swagger UI
builder.Services.AddSwaggerGen(); // Générer la documentation Swagger

// Ajouter les autres services de l'application
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPasswordDAO, PasswordDao>();
builder.Services.AddScoped<IApplicationDAO, ApplicationDao>();
builder.Services.AddScoped<PasswordEncryptionService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ApiKeyAuthFilter("test"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Clé API requise. Exemple: 'test'",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "x-api-key", // Nom du header attendu
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Appliquer les migrations et mettre à jour la base de données
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Activer Swagger et Swagger UI pour visualiser et tester l'API
app.UseSwagger(); // Génére la documentation Swagger
app.UseSwaggerUI(); // Interface utilisateur Swagger pour interagir avec l'API

// Ajouter une redirection automatique vers Swagger
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Mapper les contrôleurs et les pages Razor
app.MapControllers();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Lancer l'application
app.Run();
