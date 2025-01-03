﻿using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;

namespace StelexarasApp.Library.Dtos.Atoma;

public class KoinotarxisDto : IStelexosDto
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public int Id { get; set; } = default!;
    public Thesi Thesi { get; set; } = default!;
    public string? DtoXwrosName { get; set; } = default!;
    public string? Tel { get; set; } = default!;
}