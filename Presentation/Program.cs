using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NinthProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NinthProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NinthProjectContext") ?? throw new InvalidOperationException("Connection string 'NinthProjectContext' not found.")));
//builder.Services.AddDbContext<NinthProjectContext>(options =>
//    options.UseInMemoryDatabase("NinthProjectTestDb"));
builder.Services.AddTransient<NinthProject.IUnitOfWork, UnitOfWork>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public partial class Program { }
