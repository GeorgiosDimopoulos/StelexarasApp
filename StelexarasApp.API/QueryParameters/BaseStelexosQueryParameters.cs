using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.API.QueryParameters
{
    public class BaseStelexosQueryParameters : BasePersonQueryParameters
    {
        public Thesi? Thesi { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
