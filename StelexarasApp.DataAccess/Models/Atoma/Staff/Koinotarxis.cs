﻿using StelexarasApp.DataAccess.Models.Domi;
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.DataAccess.Models.Atoma.Staff
{
    public class Koinotarxis : Stelexos
    {
        public string FullName { get; set; } = null!;

        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Koinotita Koinotita { get; set; } = null!;
        public string Tel { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Koinotarxis;
        public IEnumerable<Omadarxis> Omadarxes { get; set; } = null!;
    }
}