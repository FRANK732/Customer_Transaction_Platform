using Customer_Balance_Paltform.CBP.Infrastractures;
using Customer_Balance_Paltform.Repositories;
using Customer_Balance_Paltform.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CTP_Clcient",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL Server
builder.Services.AddDbContext<DbCTPContest>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("server")));

//Service Extensions
builder.Services.AddCfpServices();

//Repo Registrattions
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
// builder.Services.AddScoped<IContactRepo, ContactRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CTP_Clcient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();