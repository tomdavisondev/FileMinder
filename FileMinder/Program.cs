using FileMinder.Data;
using FileMinder.Repositories;
using FileMinder.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Document Tracker API", Version = "v1" });
});

// Access secrets in your application code
var serviceProvider = builder.Services.BuildServiceProvider();
var Configuration = serviceProvider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseSqlServer(Configuration.GetConnectionString("DocumentConnection"));
});

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Document Tracker API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
