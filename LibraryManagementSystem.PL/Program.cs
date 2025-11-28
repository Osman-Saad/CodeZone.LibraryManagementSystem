using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.BLL.Services;
using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Data.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDbContext>(opt =>
    opt.UseInMemoryDatabase("LibraryDb"));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowTransactionService, BorrowTransactionService>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
try
{
    var dbContext = serviceProvider.GetRequiredService<LibraryDbContext>();
    await SeedContext.SeedAsync(dbContext);
}
catch(Exception ex)
{
    logger.LogError(ex.Message, ex);
}

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
