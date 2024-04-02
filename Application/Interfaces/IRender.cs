namespace Application.Interfaces;

public interface IRender
{
    string Render(string template, object parameters);
}
