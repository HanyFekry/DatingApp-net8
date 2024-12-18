using DatingApi.Data;
using DatingApi.Extensions;
using DatingApi.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddSwaggerService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(config =>
{
    config.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200", "https://localhost:4200");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;
app.UseRequestLocalization(localizeOptions.Value);

await app.ApplyMigrations();
await app.SeedUsers();

app.Run();
