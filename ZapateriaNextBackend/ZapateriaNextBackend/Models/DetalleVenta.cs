using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZapateriaNextBackend.Models
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public int IdVenta { get; set; }
        public int IdZapatilla { get; set; }
        [ForeignKey("IdVenta")]
        public Venta Venta { get; set; }
        [ForeignKey("IdZapatilla")]
        public Zapatilla Zapatilla { get; set; }
    }
}
