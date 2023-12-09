using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TASK_MOCK_MVC.Entities;
using System;
namespace TASK_MOCK_MVC.Data;
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider services) : base(options) => this.Services = services;
    public IServiceProvider Services { get; set; }
    public DbSet<AuditLog> AuditLog { get; set; }
    public DbSet<TaskModel> TaskModels { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration(Services));
    }

}
