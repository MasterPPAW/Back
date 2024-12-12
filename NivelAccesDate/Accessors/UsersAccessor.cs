using LibrarieModele;

using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors.Abstraction;

using Repository_CodeFirst;

using System.Diagnostics;

namespace NivelAccesDate.Accessors
{
    public class UsersAccessor : IUsersAccessor
    {
        #region API
        private readonly FitnessDBContext _context;

        public UsersAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Where(m => m.IsDeleted == false).ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Email == email && m.IsDeleted == false);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.UserId == id && m.IsDeleted == false);
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id && m.IsDeleted == false);
            if (user != null)
            {
                //_context.Users.Remove(user);
                user.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }
        #endregion

        #region MVC
        /*private readonly FitnessDBContext _context;
        private readonly IMapper _mapper;

        public UsersAccessor(FitnessDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(ent => _mapper.Map<UserDTO>(ent)).ToList();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _context.Users.FirstOrDefaultAsync(m => m.UserId == id));
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            var toEntity = _mapper.Map<User>(userDTO);

            await _context.Users.AddAsync(toEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var toEntity = _mapper.Map<User>(userDTO);

            _context.Users.Update(toEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }*/
        #endregion

        #region Consola
        public async Task PrintAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();  

            Console.WriteLine("All Users:");
            Console.WriteLine("{0,-25} {1,-25} {2,-25} {3,-25} {4,-25} {5,-25} {6,-25}",
                            "UserId", "Name", "Email", "Password", "RegistrationDate", "FitnessLevel", "TrialExpiration");
            foreach (var user in users)
            {
                Console.WriteLine("{0,-25} {1,-25} {2,-25} {3,-25} {4,-25} {5,-25} {6,-25}",
                    user.UserId, user.Name, user.Email, user.Password, user.RegistrationDate, user.FitnessLevel, user.TrialExpiration);
            }
        }

        public async Task GetUsers_EagerLoading()
        {
            var stopwatch = Stopwatch.StartNew();

            var usersWithSubscriptions = await _context.Users
                .Include(u => u.Subscriptions)
                .ToListAsync();

            stopwatch.Stop();
            Console.WriteLine($"Eager Loading Time: {stopwatch.ElapsedMilliseconds} ms");

            foreach (var user in usersWithSubscriptions)
            {
                Console.WriteLine($"User: {user.Name}");
                foreach (var subscription in user.Subscriptions)
                {
                    Console.WriteLine($"\tSubscription Type: {subscription.SubscriptionType}");
                }
            }
        }

        public async Task GetUsers_LazyLoading()
        {
            var stopwatch = Stopwatch.StartNew();

            var users = await _context.Users.ToListAsync();

            stopwatch.Stop();
            Console.WriteLine($"Lazy Loading Time (initial load): {stopwatch.ElapsedMilliseconds} ms");

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Name}");

                foreach (var subscription in user.Subscriptions)
                {
                    Console.WriteLine($"\tSubscription: {subscription.SubscriptionType}, Price: {subscription.Price}");
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Lazy Loading Time (total): {stopwatch.ElapsedMilliseconds} ms");
        }
        #endregion
    }
}
