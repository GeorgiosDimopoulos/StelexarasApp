using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.API.QueryParameters
{
    public class BaseStelexosQueryParameters : BasePersonQueryParameters
    {
        public Thesi? Thesi { get; set; }
        public int Age { get; internal set; } = 0;
        public Sex Sex { get; internal set; } = default!;
        public string DtoXwrosName { get; internal set; } = string.Empty;
        public string Tel { get; internal set; } = string.Empty;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
