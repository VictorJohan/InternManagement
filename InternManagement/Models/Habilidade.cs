using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class Habilidade
    {
        public int HabilidadId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
