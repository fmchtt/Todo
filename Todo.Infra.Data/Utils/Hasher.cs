using Todo.Application.Utils;

namespace Todo.Infra.Data.Utils;

public class Hasher : IHasher
{
    public string Hash(string key)
    {
        return BCrypt.Net.BCrypt.HashPassword(key);
    }

    public bool Verify(string key, string hashedKey)
    {
        return BCrypt.Net.BCrypt.Verify(key, hashedKey);
    }
}
