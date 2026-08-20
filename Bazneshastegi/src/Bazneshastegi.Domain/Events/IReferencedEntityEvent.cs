using Bazneshastegi.Domain.Abstractions;
using Bazneshastegi.Domain.Types;

namespace Bazneshastegi.Domain.Events;

public interface IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>, IDbType<TIdValue>
    where TEvent : IDomainEvent
{
    TEntity SourceObject { get; }
}
