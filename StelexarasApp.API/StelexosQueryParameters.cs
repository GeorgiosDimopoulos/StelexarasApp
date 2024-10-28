using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.API
{
    public class StelexosQueryParameters
    {
        public Guid? Id { get; set; }               // Filter by unique identifier
        public string? FullName { get; set; }        // Filter by name
        public Thesi? Thesi { get; set; }            // Filter by position enum
        public Guid? KoinotitaId { get; set; }       // Filter by associated Koinotita
        public Guid? TomeasId { get; set; }          // Filter by associated Tomeas

        // Pagination
        public int PageNumber { get; set; } = 1;     // Page number
        public int PageSize { get; set; } = 10;      // Results per page
    }

}
