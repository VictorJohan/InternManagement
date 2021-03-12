using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class RealizarTarea
    {
        public int Id { get; set; }
        public int? TareaId { get; set; }
        public int? PasanteId { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }
        public bool Estado { get; set; }

        public virtual Pasante Pasante { get; set; }
        public virtual Tarea Tarea { get; set; }
    }
}
