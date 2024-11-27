using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;

using Microsoft.AspNetCore.Identity;

using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;

namespace NivelService
{
    public class AuthService : IAuthService
    {
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IUsersAccessor _usersAccessor;

        public AuthService(IMapper mapper, IUsersAccessor usersAccessor)
        {
            _mapper = mapper;
            _usersAccessor = usersAccessor;
            _passwordHasher = new PasswordHasher<string>();
        }

        private string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<AuthResponse> Register(UserDTO userDTO)
        {
            var hashedPassword = HashPassword(userDTO.Password);

            userDTO.Password = hashedPassword;

            var toEntity = _mapper.Map<User>(userDTO);

            await _usersAccessor.CreateUser(toEntity);

            var response = new AuthResponse
            {
                Success = true,
                User = userDTO
            };

            return response;
        }

        public async Task<AuthResponse> Login(UserDTO userDTO)
        {
            var response = new AuthResponse
            {
                Success = true,
                User = userDTO
            };

            return response;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _usersAccessor.GetUsers();

            return users.Select(ent => _mapper.Map<UserDTO>(ent)).ToList();
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return _mapper.Map<UserDTO>(await _usersAccessor.GetUserByEmail(email));
        }

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _usersAccessor.GetUser(id));
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO, int id)
        {
            var foundUser = await _usersAccessor.GetUser(id);
            if (foundUser == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(userDTO, foundUser);

            await _usersAccessor.UpdateUser(foundUser);

            return await GetUser(id);
        }

        public async Task DeleteUser(int id)
        {
            await _usersAccessor.DeleteUser(id);
        }
    }
}
