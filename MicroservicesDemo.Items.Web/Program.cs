using MicroservicesDemo.Items.DataAccess;
using MicroservicesDemo.Items.Web.DependencyInjection;
using MicroservicesDemo.Queries.DependencyInjection;
using MicroservicesDemo.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MainContext");

builder.Services
    .AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDatabase<ItemsContext>(connectionString);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterQueries("MicroservicesDemo.Items.Queries");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
