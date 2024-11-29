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
builder.Services.AddScoped<ISubscriptionsService, SubscriptionsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWorkoutPlansService, WorkoutPlansService>();
builder.Services.AddScoped<IExercisesService, ExercisesService>();
builder.Services.AddScoped<IWorkoutPlanExercisesService, WorkoutPlanExercisesService>();

builder.Services.AddScoped<IUsersAccessor, UsersAccessor>();
builder.Services.AddScoped<ISubscriptionsAccessor, SubscriptionsAccessor>();
builder.Services.AddScoped<IWorkoutPlansAccessor, WorkoutPlansAccessor>();
builder.Services.AddScoped<IExercisesAccessor, ExercisesAccessor>();
builder.Services.AddScoped<IPaymentsAccessor, PaymentsAccessor>();
builder.Services.AddScoped<IWorkoutPlanExercisesAccessor, WorkoutPlanExercisesAccessor>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("https://fitnessppaw.netlify.app", "http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularDev");

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
