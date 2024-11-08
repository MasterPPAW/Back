using Microsoft.EntityFrameworkCore;
using NivelAccesDate.Accessors;
using NivelAccesDate.Configs;

using Repository_CodeFirst;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FitnessDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("fitnessDB"))
           .UseLazyLoadingProxies());

builder.Services.AddScoped<UsersAccessor>();
builder.Services.AddScoped<SubscriptionsAccessor>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
