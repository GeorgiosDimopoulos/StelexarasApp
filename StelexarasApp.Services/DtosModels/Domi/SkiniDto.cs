﻿
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Services.DtosModels.Domi
{
    public class SkiniDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? KoinotitaName { get; set; }
        public int PaidiaNumber { get; set; }
        public OmadarxisDto? OmadarxisDto { get; set; }
    }
}