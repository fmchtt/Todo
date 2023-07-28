using Todo.Domain.Entities;

namespace Todo.Application.Results;

public class ExpandedColumnResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public int ItemCount { get; set; }
    public EColumnType Type { get; set; }
    public List<ResumedItemResult> Itens { get; set; } = new List<ResumedItemResult>();
}
