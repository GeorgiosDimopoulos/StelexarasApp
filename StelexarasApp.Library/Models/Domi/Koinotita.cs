﻿using StelexarasApp.Library.Models.Atoma.Staff;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Domi;

public class Koinotita : Xwros
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Koinotarxis? Koinotarxis { get; set; }
    public int? KoinotarxisId { get; set; }
    public Tomeas Tomeas { get; set; } = null!;
    public IEnumerable<Skini>? Skines { get; set; }
}
