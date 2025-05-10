namespace StelexarasApp.Library.Dtos.People.Children;

public class EkpaideuomenosDtoBase : IPaidiDto
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public bool SeAdeia { get; set; }
    public string XwrosName { get; set; } = default!;
    public Sex Sex { get; set; }
}

public class CreateEkpaideuomenosRequest : EkpaideuomenosDtoBase { }

public class UpdateEkpaideuomenosRequest : EkpaideuomenosDtoBase
{
    public int Id { get; set; }
}

public class DeleteEkpaideuomenosRequest
{
    public int Id { get; set; }
}

public class EkpaideuomenosResponse : EkpaideuomenosDtoBase
{
    public int Id { get; set; }
}
