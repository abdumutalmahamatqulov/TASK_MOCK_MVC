using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NToastNotify;
using TASK_MOCK_MVC.Data;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.Services.Interfaces;
using TASK_MOCK_MVC.Services.Repositories;
using TASK_MOCK_MVC.Data;
using FluentValidation.AspNetCore;
using TASK_MOCK_MVC.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Task_MVC API",
		Version = "v1"
	});
});
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
	options.EnableSensitiveDataLogging();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterModelValidator>());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped <IAuditRepository, AuditRepository>();

builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
	ProgressBar = true,
	Timeout = 5000
});
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
	await Seed.SeedUsersAndRolesAsync(serviceScope.ServiceProvider);
}
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseNToastNotify();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
	if (context.Request.Path.StartsWithSegments("/swagger"))
	{
		if (context.User.Identity.IsAuthenticated)
		{
			if (!context.User.IsInRole("ADMIN"))
			{
				context.Response.StatusCode = 403;
				return;
			}
		}
		else
		{
			context.Response.Redirect("/Account/Login");
			return;
		}
	}
	 await next.Invoke();
});
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Main}/{id?}");

app.Run();
