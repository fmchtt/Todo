using Todo.Domain.Entities;

namespace Todo.Domain.Results;

public class ExpandedColumnResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public int ItemCount { get; set; }
    public List<ResumedItemResult> Itens { get; set; } = new List<ResumedItemResult>();
    
    public ExpandedColumnResult() {}
}
