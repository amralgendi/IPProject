namespace Pictionary.Domain.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : ValueObject
{
    protected AggregateRoot(TId id, long version)
        : base(id)
    {
        Version = version;
    }

    public long Version { get; protected set; }

    public void UpdateVersion()
    {
        Version += 1;
    }

    #pragma warning disable CS8618
        protected AggregateRoot()
        {
        }
    #pragma warning restore CS8618
}
