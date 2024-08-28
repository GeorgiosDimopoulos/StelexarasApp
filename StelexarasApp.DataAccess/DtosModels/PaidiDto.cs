using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.DtosModels
{
    public class PaidiDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool SeAdeia { get; set; }
        public Sex Sex { get; set; }
        public PaidiType PaidiType { get; set; }
        public int SkiniId { get; set; }
    }
}
