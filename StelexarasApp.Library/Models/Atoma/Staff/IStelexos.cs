using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma.Staff;

public interface IStelexos : IPerson
{
    string Tel { get; set; }
    Thesi Thesi { get; set; }
    string XwrosName { get; set; }
}

public enum Thesi
{
    None = 0,

    [Display(Name = "Ομαδάρχης")]
    Omadarxis = 1,
    
    [Display(Name = "Κοινοτάρχης")]
    Koinotarxis = 2,
    
    [Display(Name = "Τομεάρχης")]
    Tomearxis = 3,
    
    [Display(Name = "Εκπαιδευτής")] 
    Ekpaideutis = 4,
}
