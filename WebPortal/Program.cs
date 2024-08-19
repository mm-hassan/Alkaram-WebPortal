using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebPortal;
using WebPortal.Data;
using WebPortal.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Register the DocumentViewModelFilter
builder.Services.AddScoped<DocumentViewModelFilter>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PortalContext>(options =>
    options.UseSqlServer(connectionString));




// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<DocumentViewModelFilter>(); // Apply the filter globally if desired
}).AddRazorRuntimeCompilation();

// Optional: Add additional services (e.g., for dependency injection, logging, etc.)
// builder.Services.AddDbContext<YourDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddLogging();
// builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<YourDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure this is added if using authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Services}/{id?}");

// Optional: Map additional routes or endpoints if needed
app.MapRazorPages();
app.MapControllers();

app.Run();
