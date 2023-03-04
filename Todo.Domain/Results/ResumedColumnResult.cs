using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ResumedColumnResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public List<ResumedItemResult> Itens { get; set; } = new List<ResumedItemResult>();

    public ResumedColumnResult()
    {
    }
}