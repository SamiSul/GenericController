using System.Reflection;
using GenericControllerDemo.Data;
using GenericControllerDemo.Tools;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<GenericDbContext>(options => options.UseInMemoryDatabase("GenericControllerDemo"));

var mvcBuilder = builder.Services.AddMvc(options => options.Conventions.Add(new GenericControllerRouteConvention()));

var extendedMvcBuilder = mvcBuilder.ConfigureApplicationPartManager(applicationPartManager =>
    applicationPartManager.FeatureProviders.Add(
        new GenericTypeControllerFeatureProvider(new[] { Assembly.GetEntryAssembly().FullName })));

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

app.UseAuthorization();

app.MapControllers();

app.Run();