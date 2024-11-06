using AutoMapper;
using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullAgeResolver : IValueResolver<Tomearxis, IStelexos, int>
{
    public int Resolve(Tomearxis source, IStelexos destination, int destMember, ResolutionContext context)
    {
        return source.Age != 0 ? source.Age : destination.Age;
    }
}