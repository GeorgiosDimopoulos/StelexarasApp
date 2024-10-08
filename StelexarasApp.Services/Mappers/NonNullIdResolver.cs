using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullIdResolver : IValueResolver<Tomearxis, IStelexos, int>
{
    public int Resolve(Tomearxis source, IStelexos destination, int destMember, ResolutionContext context)
    {
        return source.Id > 0 ? source.Id : destination.Id;
    }
}