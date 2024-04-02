using Application.Interfaces;
using DotLiquid;

namespace Infrastructure.Template;

public class TemplateService : IRender
{
    public string Render(string template, object parameters)
    {
        var tmpl = DotLiquid.Template.Parse(template);
        
        return tmpl.Render(Hash.FromAnonymousObject(parameters));
    }
}
