using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class Pasante
    {
        public Pasante()
        {
            AsignarTareas = new HashSet<AsignarTarea>();
            RealizarTareas = new HashSet<RealizarTarea>();
        }

        public int PasanteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? InstitucionId { get; set; }
        public int? NacionalidadId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual Institucione Institucion { get; set; }
        public virtual Nacionalidade Nacionalidad { get; set; }
        public virtual ICollection<AsignarTarea> AsignarTareas { get; set; }
        public virtual ICollection<RealizarTarea> RealizarTareas { get; set; }
    }
}
