namespace StelexarasApp.Library.Dtos.People.Children;

public class KataskinotisDtoBase : PaidiDtoBase { }

public class CreateKataskinotisRequest : KataskinotisDtoBase { }

public class UpdateKataskinotisRequest : KataskinotisDtoBase
{
    public int Id { get; set; }
}

public class DeleteKataskinotisRequest
{
    public int Id { get; set; }
}

public class KataskinotisResponse : KataskinotisDtoBase
{
    public int Id { get; set; }
}