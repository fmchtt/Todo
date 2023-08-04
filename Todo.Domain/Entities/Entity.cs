namespace Todo.Domain.Entities;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public bool Equals(Entity? other)
    {
        return other != null && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
