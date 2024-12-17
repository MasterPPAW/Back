using AutoMapper;

using LibrarieModele;
using LibrarieModele.DTOs;

using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;

namespace NivelService
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersAccessor _usersAccessor;

        public UsersService(IMapper mapper, IUsersAccessor usersAccessor)
        {
            _mapper = mapper;
            _usersAccessor = usersAccessor;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _usersAccessor.GetUsers();

            return users.Select(ent => _mapper.Map<UserDTO>(ent)).ToList();
        }

        public async Task<List<UserDTO>> GetUsersDeleted()
        {
            var users = await _usersAccessor.GetUsersDeleted();

            return users.Select(ent => _mapper.Map<UserDTO>(ent)).ToList();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _usersAccessor.GetUser(id));
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            /*if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "User data is required.");
            }
            if (string.IsNullOrWhiteSpace(userDTO.Email))
            {
                throw new ArgumentException("Email is required.", nameof(userDTO.Email));
            }
            if (string.IsNullOrWhiteSpace(userDTO.Name))
            {
                throw new ArgumentException("Name is required.", nameof(userDTO.Name));
            }*/

            var toEntity = _mapper.Map<User>(userDTO);

            await _usersAccessor.CreateUser(toEntity);
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

        public async Task<bool> UserExists(int id)
        {
            return await _usersAccessor.UserExists(id);
        }
    }
}
