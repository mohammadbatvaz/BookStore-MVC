using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);


// Database Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=MBATVAZ-PC\SQLEXPRESS;Database=W17_BookStore_DB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;")
);

// Dependency Injection for Services
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IFileService, FileService>();


// Dependency Injection for Repositories
builder.Services.AddScoped<IBookRepositories, BookRepositories>();
builder.Services.AddScoped<ICategoryRepositories, CategoryRepositories>();
builder.Services.AddScoped<IUserRepositories, UserRepositories>();

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
