using Customer.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mask.Application;
using Mask.Application.Behaviors;
using Mask.Application.Commands;
using Mask.Application.Interfaces;
using Mask.Application.Services;
using Mask.Domain.Interfaces;
using Mask.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext
builder.Services.AddDbContext<MaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Add Meditor
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

// Add Modules
builder.Services.RegisterApplicationModule(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddTransient(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MaskDbContext>();
    await context.Database.MigrateAsync();
}

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
