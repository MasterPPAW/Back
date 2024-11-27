using LibrarieModele.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface IAuthService
    {
        Task<UserDTO> GetUserByEmail(string email);
        Task<AuthResponse> Register(UserDTO userDTO);
        Task<AuthResponse> Login(UserDTO userDTO);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
