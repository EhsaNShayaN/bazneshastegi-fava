namespace SRH.MediatRMessaging2;

public interface IValidatableRequest2<T>
{
    ValueTask<PrimitiveResult2<T>> Validate();
}
