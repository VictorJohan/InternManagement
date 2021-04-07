using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "El Campo Descripcion no puede estar vacio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Campo Requerimiento no puede estar vacio")]
        public string Requerimiento { get; set; }
        [Required(ErrorMessage = "El Campo Tiempo Aproximado no puede estar vacio")]
        public string TiempoAproximado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual ICollection<AsignarTarea> AsignarTareas { get; set; }
        public virtual ICollection<RealizarTarea> RealizarTareas { get; set; }
    }
}
