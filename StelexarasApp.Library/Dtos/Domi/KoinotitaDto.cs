namespace StelexarasApp.Library.Dtos.Domi;

public class KoinotitaDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
    public string TomeasName { get; set; } = string.Empty;
}

public class CreateKoinotitaRequest : KoinotitaDtoBase { }

public class UpdateKoinotitaRequest : KoinotitaDtoBase { }

public class DeleteKoinotitaRequest
{
    public string Name { get; set; } = string.Empty;
}

public class KoinotitaResponse : KoinotitaDtoBase
{
    public int SkinesNumber { get; set; }
}
