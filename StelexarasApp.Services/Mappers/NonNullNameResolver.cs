using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullNameResolver : IValueResolver<Tomearxis, IStelexos, string>
{
    public string? Resolve(Tomearxis source, IStelexos destination, string? destMember, ResolutionContext context)
    {
        // return source.FullName ?? string.Empty;
        return !string.IsNullOrWhiteSpace(source.FullName) ? source.FullName : destination.FullName;
    }
}