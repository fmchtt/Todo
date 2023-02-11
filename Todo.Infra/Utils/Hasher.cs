using Todo.Domain.Utils;

namespace Todo.Infra.Utils;

public class Hasher : IHasher
{
    private string Secret;

    public Hasher(string secret)
    {
        Secret = secret;
    }

    public string Hash(string key)
    {
        return BCrypt.Net.BCrypt.HashPassword(key);
    }

    public bool Verify(string key, string hashedKey)
    {
        return BCrypt.Net.BCrypt.Verify(key, hashedKey);
    }
}
