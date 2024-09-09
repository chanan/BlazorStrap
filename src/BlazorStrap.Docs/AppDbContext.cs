using BlazorStrap_Docs.Pages;
using BlazorStrap_Docs.SamplesHelpers.Content.Tables;
using Microsoft.EntityFrameworkCore;

namespace BlazorStrap_Docs;


public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("PeopleDb");
    }
}
