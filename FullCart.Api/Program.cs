using FullCart.Api.Middlewares;
using FullCart.Application.Common.Shared;

using Serilog;

using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
             .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext().Enrich.WithMachineName().Enrich.WithProcessId().Enrich.WithThreadId()
             .CreateLogger();

AppSetting _appSetting = new AppSetting();
builder.Configuration.GetSection("appSettings").Bind(_appSetting);
builder.Services.AddSingleton(_appSetting);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                          .WithOrigins(_appSetting.allowedCrossOrign)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
        });
});



// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, _appSetting);
builder.Services.AddWebApiServices(_appSetting);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.UseJwtMiddleware();

app.MapControllers();

app.Run();
