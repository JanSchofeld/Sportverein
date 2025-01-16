using System;
using Microsoft.EntityFrameworkCore;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Database;

public class SportvereinDbContext : DbContext
{
    private DbConnectionFactory dbConnectionFactory;

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseMembership> CourseMemberships { get; set; }
    public DbSet<CourseEvent> courseEvents { get; set; }

    public SportvereinDbContext(DbConnectionFactory dbConnectionFactory)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        this.dbConnectionFactory = dbConnectionFactory;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connection = dbConnectionFactory.GetConnection();
            optionsBuilder.UseNpgsql(connection);
            optionsBuilder.UseLowerCaseNamingConvention();
        }
        
        
        base.OnConfiguring(optionsBuilder);
    }
}
