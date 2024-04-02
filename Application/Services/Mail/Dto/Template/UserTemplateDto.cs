using Application.Common.Mappings;
using Domain;

namespace Application.Services.Mail.Dto.Template;

public class UserTemplateDto : BaseMappingFrom<User>, ITemplateDto
{
    public required string Login { get; set; }
}
