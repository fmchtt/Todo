namespace Todo.Domain.Results;

public class ResumedColumnResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<ResumedItemResult> Itens { get; set; } = new();
}