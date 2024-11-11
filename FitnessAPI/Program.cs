using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors;
using NivelAccesDate.Accessors.Abstraction;
using NivelAccesDate.Configs;

using NivelService;
using NivelService.Abstraction;

using Repository_CodeFirst;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FitnessDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("fitnessDB"))
           .UseLazyLoadingProxies());
           /*.EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Warning));*/

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IUsersAccessor, UsersAccessor>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
