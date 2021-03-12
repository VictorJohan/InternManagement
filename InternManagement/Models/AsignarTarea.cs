using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class AsignarTarea
    {
        public int Id { get; set; }
        public int? TareaId { get; set; }
        public int? PasanteId { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Pasante Pasante { get; set; }
        public virtual Tarea Tarea { get; set; }
    }
}
