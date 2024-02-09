using Microsoft.OpenApi.Models;
using MyRestaurant.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Using Memory Database
builder.Services.AddSingleton<OrderDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    //Code to document the api
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyRestaurant.Api",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Joao Jesus",
            Email = "joaojesusvictor7@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/joaojesusvictor/")
        }
    });

    var xmlFile = "MyRestaurant.Api.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    x.IncludeXmlComments(xmlPath);
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
