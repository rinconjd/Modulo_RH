using BCrypt.Net;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public Usuario? Authenticate(string username, string password)
    {
        var user = _context.Usuarios.SingleOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }
}
