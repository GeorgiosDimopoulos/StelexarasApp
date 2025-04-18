﻿using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models;

public class Duty
{
    [Key]

    public int Id { get; set; }

    //#if !MAUI
    //    [SwaggerSchema(ReadOnly = true)]
    //#endif
    public string Name { get; set; }
    public DateTime Date { get; set; }
}
