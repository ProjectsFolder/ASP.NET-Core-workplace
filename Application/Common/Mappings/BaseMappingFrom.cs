using AutoMapper;

namespace Application.Common.Mappings;

public abstract class BaseMappingFrom<TFrom> : IMapping where TFrom : class
{
    public virtual void Mapping(Profile profile) => profile.CreateMap(typeof(TFrom), GetType());
}
