namespace Domain.Entities;

public class BaseEntity
{
    protected BaseEntity()
    {
        Key = Guid.NewGuid();
    }

    public int Id { get; set; }
    public Guid Key { get; set; }
}
