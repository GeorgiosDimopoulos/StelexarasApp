﻿using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Services.DtosModels.Atoma
{
    public interface StelexosDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int? Id { get; set; }
        public Thesi Thesi { get; set; }
    }
}