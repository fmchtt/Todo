﻿namespace Todo.Domain.Utils;

public interface IHasher
{
    string Hash(string key);
    bool Verify(string key, string hashedKey);
}
