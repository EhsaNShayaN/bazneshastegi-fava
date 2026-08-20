namespace Bazneshastegi.Domain.Events;
public abstract record DomainEventBase(DateTimeOffset OccuredOn) : IDomainEvent
{
    public bool IsIntegrationEvent => false;
}
