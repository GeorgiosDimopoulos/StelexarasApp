using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullSexResolver : IValueResolver<Tomearxis, StelexosBase, Sex>
{
    public Sex Resolve(Tomearxis source, StelexosBase destination, Sex destMember, ResolutionContext context)
    {
        return source.Sex != 0 ? source.Sex : Sex.Male;
    }
}