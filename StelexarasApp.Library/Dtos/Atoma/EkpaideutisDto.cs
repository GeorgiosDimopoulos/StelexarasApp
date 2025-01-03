﻿using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Library.Dtos.Atoma
{
    public class EkpaideutisDto : IStelexosDto
    {
        public string? FullName { get; set; }
        public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
        public int Age { get; set; }
        public int? Id { get; set; }
        public Sex Sex { get; set; }
        public string? DtoXwrosName { get; set; }
        public string? Tel { get; set; }
        int IStelexosDto.Id { get; set; }
    }
}
