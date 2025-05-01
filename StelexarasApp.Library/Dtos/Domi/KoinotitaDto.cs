namespace StelexarasApp.Library.Dtos.Domi;

public class KoinotitaDto
{
    public required string Name { get; set; }
    public required string TomeasName { get; set; }
    public int SkinesNumber { get; set; }
}

public class CreateKoinotitaDto
{
    public required string Name { get; set; }
    public required string TomeasName { get; set; }
}

public class UpdateKoinotitaDto
{
    public required string Name { get; set; }
    public required string TomeasName { get; set; }
}
