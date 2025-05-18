using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using System.Text;
using TaskManager.API.Providers;
using TaskManager.API.Validators;
using TaskManager.Application.Commands.User;
using TaskManager.Application.Handlers.CommadHandlers;
using TaskManager.Core.Command;
using TaskManager.Core.Command.Base;
using TaskManager.Core.Entities;
using TaskManager.Core.Query;
using TaskManager.Core.Query.Base;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Command;
using TaskManager.Infrastructure.Repository.Command.Base;
using TaskManager.Infrastructure.Repository.Query;
using TaskManager.Infrastructure.Repository.Query.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
builder.Host.UseSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskManagerDBContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerDBContext")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler)
    .GetTypeInfo().Assembly));

builder.Services.AddScoped(typeof(IQueryRepository<>), typeof(QueryBaseRepository<>));
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandBaseRepository<>));
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

builder.Services.AddScoped<IValidator<CreateUserCommand>, UserValidator>();
builder.Services.AddTransient<TokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();
app.UseExceptionHandler(hanlder =>
hanlder.Run(async (context) => { await context.Response.WriteAsync("Internel Error"); }));

// Auto-apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerDBContext>();
    dbContext.Database.Migrate();
}

app.Run();
