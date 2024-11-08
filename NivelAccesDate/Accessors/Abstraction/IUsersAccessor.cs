using LibrarieModele;
using LibrarieModele.DTOs;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface IUsersAccessor
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<bool> UserExists(int id);
    }
}
