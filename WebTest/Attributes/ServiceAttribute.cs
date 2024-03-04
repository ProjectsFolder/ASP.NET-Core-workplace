namespace WebTest.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute(ServiceType type = ServiceType.Scoped) : Attribute
    {
        public ServiceType Type { get; set; } = type;
    }

    public enum ServiceType
    {
        Scoped,
        Singleton,
        Transient
    }
}
