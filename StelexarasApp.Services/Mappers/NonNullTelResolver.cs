using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullTelResolver : IValueResolver<Tomearxis, StelexosBase, string>
{
    public string Resolve(Tomearxis source, StelexosBase destination, string destMember, ResolutionContext context)
    {
        return source.Tel ?? "N/A";
    }
}