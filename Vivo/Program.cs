using Vivo;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var scope = app.Services.CreateScope();
var vivoContext = scope.ServiceProvider.GetRequiredService<VivoContext>();

startup.Configure(app, app.Environment, provider, vivoContext);

app.Run();
