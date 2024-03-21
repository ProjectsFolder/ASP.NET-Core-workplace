namespace Application.Common.Exceptions;

public class NotFoundException(string name, object key)
    : Exception($"Entity \"{name}\" ({key}) not found.")
{
}
