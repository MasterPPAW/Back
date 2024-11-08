﻿using AutoMapper;

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

        public async Task<UserDTO> GetUser(int id)
        {
            return _mapper.Map<UserDTO>(await _usersAccessor.GetUser(id));
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            var toEntity = _mapper.Map<User>(userDTO);

            await _usersAccessor.CreateUser(toEntity);
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var toEntity = _mapper.Map<User>(userDTO);

            await _usersAccessor.UpdateUser(toEntity);
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