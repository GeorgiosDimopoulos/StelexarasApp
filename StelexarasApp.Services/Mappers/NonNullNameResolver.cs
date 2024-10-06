using AutoMapper;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Services.Mappers;

public class NonNullNameResolver : IValueResolver<Tomearxis, StelexosBase, string> // TomearxisDto
{
    public string? Resolve(Tomearxis source, StelexosBase destination, string? destMember, ResolutionContext context)
    {
        // return source.FullName ?? string.Empty;
        return !string.IsNullOrWhiteSpace(source.FullName) ? source.FullName : destination.FullName;
    }
}