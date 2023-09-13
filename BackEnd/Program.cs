using BackEnd.Data;


var builder = WebApplication.CreateBuilder(args);
ConfigureMVC(builder);
ConfigureServices(builder);

var app = builder.Build();


app.MapControllers();
app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApiDataContext>();
}
void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
}
