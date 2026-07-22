using Microsoft.Extensions.Options;
using TMDBAPI.Services;
using DotNetEnv;
if (File.Exists(".env"))//se existir o .env carrega.
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddScoped<MovieService>();

builder.Services.AddCors(options =>
{
   options.AddPolicy("frontend", policy =>
   {
       policy.WithOrigins("http://localhost:5500",
                          "http://127.0.0.1:5500",
                          "https://integration-tmdbapi.vercel.app")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
   });
});

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
