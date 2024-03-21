namespace Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DependencyAttribute(
    ServiceType type = ServiceType.Scoped,
    Type? baseType = null) : Attribute
{
    public ServiceType Type { get; set; } = type;

    public Type? BaseType { get; set; } = baseType;
}

public enum ServiceType
{
    Scoped,
    Singleton,
    Transient
}
