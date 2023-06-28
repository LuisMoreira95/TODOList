using Microsoft.EntityFrameworkCore;
using TODOList.API.Data;
using TODOList.API.Mappings;
using TODOList.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// Added CORS policy to consume backend locally
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Using DP Dependency Injection
// Using builder instead of instantiate object on the constructor
// Because we added the connectionString, the BD Context is now available on the project
builder.Services.AddDbContext<TodoListDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TodoListConnectionString")));

builder.Services.AddScoped<ITodoRepository, SQLTodoRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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

app.UseCors(MyAllowSpecificOrigins);

app.Run();
