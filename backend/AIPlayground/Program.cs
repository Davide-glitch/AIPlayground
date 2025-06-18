using System.Reflection;
using AiPlayground.DataAccess;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using AIPlayground.BusinessLogic.Interfaces;
using AIPlayground.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AIPlaygroundContext");

builder.Services.AddDbContext<AiPlaygroundContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IRepository<Scope>, ScopeRepository>();
builder.Services.AddScoped<IRepository<Prompt>, BaseRepository<Prompt>>();
builder.Services.AddScoped<IRepository<Run>, BaseRepository<Run>>();
builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();
builder.Services.AddScoped<IRepository<Model>, ModelRepository>();

builder.Services.AddScoped<IScopeService, ScopeService>();
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IRunService, RunService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
