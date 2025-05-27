

using Application.Interfaces.IUser;
using Domain.Entities;

public class AuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public User GetUserByUsername(string username)
    {
        return _userRepository.GetUserByUsername(username);
    }
    public List<User> GetUsers()
    {
        return _userRepository.GetUsers().ToList();
    }

    public bool RegisterUser(string username, string password)
    {
        // Tjek om brugeren allerede eksisterer
        if (_userRepository.GetUserByUsername(username) != null)
        {
            return false;
        }

        var hashedPassword = HashPassword(password);
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Password = hashedPassword
        };

        _userRepository.AddUser(newUser);
        return true;
    }
}
