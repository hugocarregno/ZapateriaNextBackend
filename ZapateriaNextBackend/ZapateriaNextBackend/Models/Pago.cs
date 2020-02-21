using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZapateriaNextBackend.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CantidadCuotas { get; set; }
        [Required]
        public string Estado { get; set; }
        public int IdVenta { get; set; }
        [ForeignKey("IdVenta")]
        public Venta Venta { get; set; }
    }
}
