namespace StelexarasApp.API.QueryParameters
{
    public class BasePersonQueryParameters
    {
        public int Id { get; set; } // Guid
        public string FullName { get; set; } = default!;
        public int Age { get; set; }
    }
}
