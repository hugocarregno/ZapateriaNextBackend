﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZapateriaNextBackend.Models
{
    public class Zapatilla
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int Talle { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public Byte Estado { get; set; }
    }
}
