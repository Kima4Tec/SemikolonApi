using Domain.Entities;

namespace Application.Interfaces.IUser
{
    public interface IUserRepository
    {
        void AddUser(User users);
        User GetUserByUsername(string username);
        List<User> GetUsers();
    }
}