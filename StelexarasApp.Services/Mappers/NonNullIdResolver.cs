using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullIdResolver : IValueResolver<Tomearxis, StelexosBase, int>
{
    public int Resolve(Tomearxis source, StelexosBase destination, int destMember, ResolutionContext context)
    {
        return source.Id > 0 ? source.Id : destination.Id;
    }
}