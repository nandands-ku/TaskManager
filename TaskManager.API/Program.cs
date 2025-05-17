using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TaskManager.Application.Handlers.CommadHandlers;
using TaskManager.Core.Command;
using TaskManager.Core.Command.Base;
using TaskManager.Core.Query;
using TaskManager.Core.Query.Base;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Command;
using TaskManager.Infrastructure.Repository.Command.Base;
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
//builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
