using Expenzio.Common.Helpers;
using Expenzio.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
DataAccessHelper.SetConfiguration(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Add services for dependency injection to container.
// builder.Services.ConfigureSettings(builder.Configuration);
builder.Services
    .ConfigureSettings(builder.Configuration)
    .ConfigureServices();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureGraphQL();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerWithVersioning();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUIWithVersioning();
}

app.UseCors("ExpenzioWebClient");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();
DataAccessHelper.EnsureMigration(AppDomain.CurrentDomain.FriendlyName);
app.Run();

