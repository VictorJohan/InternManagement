using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class PasanteHabilidade
    {
        public int? PasanteId { get; set; }
        public int? HabilidadId { get; set; }

        public virtual Habilidade Habilidad { get; set; }
        public virtual Pasante Pasante { get; set; }
    }
}
