using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZapateriaNextBackend.Models
{
    public class Cuota
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NroCuota { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public string Estado { get; set; }
        public int IdPago { get; set; }
        [ForeignKey("IdPago")]
        public Pago Pago { get; set; }
    }
}
