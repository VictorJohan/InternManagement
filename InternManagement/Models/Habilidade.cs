using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InternManagement.Models
{
    public partial class Habilidade
    {
        public int HabilidadId { get; set; }
        [Required(ErrorMessage ="Debe escribir una habilidad.")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
