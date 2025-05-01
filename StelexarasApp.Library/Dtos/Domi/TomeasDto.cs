namespace StelexarasApp.Library.Dtos.Domi;

public class TomeasDto
{
    public string Name { get; set; } = string.Empty;
    public int KoinotitesNumber { get; set; }    
}

public class UpdateTomeasDto
{
    public string Name { get; set; } = string.Empty;
}

public class CreateTomeasDto
{
    public string Name { get; set; } = string.Empty;
}
