namespace bpmn_dotnet_core6.Domain;

public abstract class Entity<T>
{
    public T Id { get; protected set; }
}