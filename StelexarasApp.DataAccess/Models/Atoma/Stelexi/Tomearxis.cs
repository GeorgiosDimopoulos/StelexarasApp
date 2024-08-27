using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess.Models.Atoma.Stelexi
{
    public class Tomearxis : Stelexos
    {
        public string FullName { get; set; } = null!;
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Tomeas Tomeas { get; set; }  = new Tomeas();
        public Thesi Thesi { get; set; } = Thesi.Tomearxis;
        // public int TomeasId { get; set; }
        public IEnumerable<Koinotarxis> Koinotarxes { get; set; } = null!;

        public Tomearxis()
        {
            Tomeas = new Tomeas();
            Thesi = Thesi.Tomearxis;
        }

        public Tomearxis(string fullName, int id, int age, Sex sex, Tomeas tomeas, IEnumerable<Koinotarxis> koinotarxes)
        {
            FullName = fullName;
            Id = id;
            Age = age;
            Sex = sex;
            Tomeas = tomeas;
            Thesi = Thesi.Tomearxis;
            Koinotarxes = koinotarxes;
        }
    }
}
