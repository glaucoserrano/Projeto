using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data;

public class ApiDataContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<ChooseRestaurant> ChooseRestaurants { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("DataSource=api.db;Cache=Shared");
}
