namespace Bazneshastegi.Domain.Abstractions;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
}
