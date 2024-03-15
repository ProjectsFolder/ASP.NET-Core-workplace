using Serilog;
using WebTest.Attributes;

namespace WebTest;

[Service(type: ServiceType.Singleton)]
public sealed class LogService()
{
    public void Info(string message, params object?[]? propertyValues)
    {
        Log.Information(message, propertyValues);
    }

    public void Error(string message, params object?[]? propertyValues)
    {
        Log.Error(message, propertyValues);
    }

    public void Debug(string message, params object?[]? propertyValues)
    {
        Log.Debug(message, propertyValues);
    }
}
