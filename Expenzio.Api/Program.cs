using Expenzio.Controllers.GraphQLApi;
using Expenzio.Common.Helpers;
using Expenzio.Api;

var builder = WebApplication.CreateBuilder(args);
DataAccessHelper.SetConfiguration(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DI
builder.Services.ConfigureServices();

// Configure GraphQL
builder.Services
		.AddGraphQLServer()
		.AddQueryType<ExpensesQuery>();

// Configure Rest API
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapGraphQL();
DataAccessHelper.EnsureMigration(AppDomain.CurrentDomain.FriendlyName);
app.Run();

