using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullSexResolver : IValueResolver<Tomearxis, IStelexos, Sex>
{
    public Sex Resolve(Tomearxis source, IStelexos destination, Sex destMember, ResolutionContext context)
    {
        return source.Sex != 0 ? source.Sex : Sex.Male;
    }
}