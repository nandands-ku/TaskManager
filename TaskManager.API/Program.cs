using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4f4658e05855c7ee06f88bed8f2b4f6634433923a426d8ee9e9048863d1ab1a6"))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(hanlder =>
hanlder.Run(async (context) => { await context.Response.WriteAsync("Internel Error"); }));

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
