using StelexarasApp.Library.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma
{
    public class Paidi : IPerson
    {
        public required string FullName { get; set; }

        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public bool SeAdeia { get; set; }
        public Sex Sex { get; set; }
        public PaidiType PaidiType { get; set; }
        public int SkiniId { get; set; }

        public Skini Skini { get; set; } = new Skini();
    }

    public enum PaidiType
    {
        Ekpaideuomenos = 0,
        Kataskinotis = 1,
        Unknown = 2,
    }
}
