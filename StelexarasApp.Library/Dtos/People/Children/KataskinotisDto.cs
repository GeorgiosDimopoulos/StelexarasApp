namespace StelexarasApp.Library.Dtos.People.Children;

public class KataskinotisDtoBase : IPaidiDto
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public bool SeAdeia { get; set; }
    public string XwrosName { get; set; } = default!;
    public Sex Sex { get; set; }
}

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