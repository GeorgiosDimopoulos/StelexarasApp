using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullAgeResolver : IValueResolver<Tomearxis, StelexosBase, int>
{
    public int Resolve(Tomearxis source, StelexosBase destination, int destMember, ResolutionContext context)
    {
        return source.Age != 0 ? source.Age : destination.Age;
    }
}