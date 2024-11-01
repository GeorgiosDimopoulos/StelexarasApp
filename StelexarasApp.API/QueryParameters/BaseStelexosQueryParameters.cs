using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.API.QueryParameters
{
    public class BaseStelexosQueryParameters : BasePersonQueryParameters
    {
        public Thesi? Thesi { get; set; }
        public string DtoXwrosName { get; internal set; } = string.Empty;
        public string Tel { get; internal set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
