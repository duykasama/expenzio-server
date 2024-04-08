using Expenzio.Controllers.GraphQLApi;
using Expenzio.DAL.Implementations;
using Expenzio.DAL.Interfaces;
using Expenzio.Service;
using Expenzio.Service.Interfaces;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Data;

var builder = WebApplication.CreateBuilder(args);
DataAccessHelper.SetConfiguration(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DI
builder.Services.AddScoped<ExpenzioDbContext>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

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

