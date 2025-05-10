namespace StelexarasApp.Library.Dtos.People.Children;

public class EkpaideuomenosDtoBase : PaidiDtoBase { }

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
