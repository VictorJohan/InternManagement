using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            AsignarTareas = new HashSet<AsignarTarea>();
            RealizarTareas = new HashSet<RealizarTarea>();
        }

        public int TareaId { get; set; }
        public string Descripcion { get; set; }
        public string Requerimiento { get; set; }
        public string TiempoAproximado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<AsignarTarea> AsignarTareas { get; set; }
        public virtual ICollection<RealizarTarea> RealizarTareas { get; set; }
    }
}
