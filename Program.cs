using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PS6.Areas.Data;
using PS6.Areas.YearDb;
using PS6.Data;
using PS6.Interfaces;
using PS6.Repositories;
using PS6.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<YearDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("YearDb")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ILeapYearInterface, LeapYearService>();
builder.Services.AddScoped<ILeapYearRepository, LeapYearRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.Run();
