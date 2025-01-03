﻿using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.Library.Dtos.Atoma
{
    public class PaidiDto
    {
        public string FullName { get; set; } = default!;
        public int Age { get; set; }
        public bool SeAdeia { get; set; }
        public int Id { get; set; }
        public int SkiniId { get; set; }
        public string SkiniName { get; set; } = default!;

        public Sex Sex { get; set; }
        public PaidiType PaidiType { get; set; }
    }
}
