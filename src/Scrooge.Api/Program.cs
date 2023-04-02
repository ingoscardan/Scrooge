using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scrooge.Api.config.services;
using Scrooge.DbServices.DBContext;
using AutoMapper;
using Scrooge.DbServices.Entities;
using Scrooge.Services.Models;
using Scrooge.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ScroogeDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("ScroogeConnection"), ServerVersion.Parse("8.0.32-mysql"), x => x.MigrationsAssembly("Scrooge.DbServices"));
});

builder.Services.ConfigureDbService();
builder.Services.AddScoped<IService<NotificationModel, NotificationEntity>, Service<NotificationModel, NotificationEntity>>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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