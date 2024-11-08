using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NivelAccesDate.Accessors;

using Repository_CodeFirst;

using System.Configuration;

namespace NivelAccesDate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<FitnessDBContext>(options =>
                options.UseNpgsql(ConfigurationManager.ConnectionStrings["FitnessDBConnection"].ConnectionString));

            serviceCollection.AddScoped<UsersAccessor>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var usersAccessor = serviceProvider.GetService<UsersAccessor>();

            if (usersAccessor != null)
            {
                //await usersAccessor.GetUsers_EagerLoading();
                await usersAccessor.GetUsers_LazyLoading();
            }

            Console.ReadLine();
        }
    }
}
