namespace Todo.Domain.Entities;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; init; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public bool Equals(Entity? other)
    {
        return other != null && Id == other.Id;
    }
}
