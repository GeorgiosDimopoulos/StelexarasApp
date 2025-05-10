namespace StelexarasApp.Library.Dtos.Domi;

public class BaseTomeasDto : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
}

public class CreateTomeasRequest : BaseTomeasDto { }

public class UpdateTomeasRequest : BaseTomeasDto { }

public class DeleteTomeasRequest : BaseTomeasDto { }

public class TomeasResponse : BaseTomeasDto
{
    public int KoinotitesNumber { get; set; }
}
