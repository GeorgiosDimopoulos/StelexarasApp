﻿
using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Domi
{
    public interface Xwros
    {
        [Key]
        int Id { get; set; }
        string Name { get; set; }
    }

    public enum EidosXwrou
    {
        Skini = 0,
        Koinotita = 1,
        Tomeas = 2
    }
}