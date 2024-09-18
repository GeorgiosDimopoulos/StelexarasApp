using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels
{
    public class PaidiDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool SeAdeia { get; set; }
        public Sex Sex { get; set; }

        public int Id { get; set; }

        public string SkiniName { get; set; }
        public PaidiType PaidiType { get; set; }
        public int SkiniId { get; set; }
    }
}
