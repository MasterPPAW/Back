using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface IUsersService
    {
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(int id);
        Task CreateUser(UserDTO userDTO);
        Task<UserDTO> UpdateUser(UserDTO userDTO, int id);
        Task DeleteUser(int id);
        Task<bool> UserExists(int id);
    }
}
