using AddressStandardizationService;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Добавьте свои сервисы и компоненты здесь
builder.Services.AddScoped<IDadataService, DadataService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<DadataApiSettings>(builder.Configuration.GetSection("DadataApiSettings"));
builder.Services.AddHttpClient<IDadataService, DadataService>(client =>
{
    client.BaseAddress = new Uri("https://cleaner.dadata.ru/api/v1/clean/address");
    client.DefaultRequestHeaders.Add("Authorization", $"Token {builder.Configuration["DadataApiSettings:ApiKey"]}");
    client.DefaultRequestHeaders.Add("X-Secret", builder.Configuration["DadataApiSettings:SecretKey"]);
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address Standardization Service", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();