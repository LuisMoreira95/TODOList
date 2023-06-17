using Microsoft.EntityFrameworkCore;
using TODOList.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Estamos a usar o DP Dependency Injection
// Em vez de instanciar objectos no constructor usamos o Builder
// Ao adicionarmos a nossa connectionString aqui, o contexto da BD passa a estar disponível no projecto
builder.Services.AddDbContext<TodoListDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TodoListConnectionString")));

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
