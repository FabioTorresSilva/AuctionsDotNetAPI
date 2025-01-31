
using AuctionProject.Data;
using AuctionProject.Services;
using AuctionProject.Services.Interfaces;
using AuctionProject.Services.Stats.Interfaces;
using AuctionProject.Services.Stats;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuctionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionsDB")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IAuctionStatusUpdateService, AuctionStatusUpdateService>();
builder.Services.AddScoped<IAuctionStats, AuctionStatsService>();
builder.Services.AddScoped<ICategoryStats, CategoryStatsService>();
builder.Services.AddScoped<IMainStats, MainStatsService>();

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
