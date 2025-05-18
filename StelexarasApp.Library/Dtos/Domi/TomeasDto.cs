namespace StelexarasApp.Library.Dtos.Domi;

public class TomeasDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
}

public class CreateTomeasRequest : TomeasDtoBase { }

public class UpdateTomeasRequest : TomeasDtoBase { }

public class DeleteTomeasRequest : TomeasDtoBase { }

public class TomeasResponse : TomeasDtoBase
{
    public int KoinotitesNumber { get; set; }
}
