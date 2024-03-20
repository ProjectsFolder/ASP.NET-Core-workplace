using AutoMapper;

namespace Application.Common.Mappings;

public abstract class BaseMappingTo<TTo> : IMapping where TTo : class
{
    public virtual void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(TTo));
}
