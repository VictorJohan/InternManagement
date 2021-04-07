using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "El Campo Nombres no puede estar vacio")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El Campo Apellidos no puede estar vacio")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El Campo Edad no puede estar vacio")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "El Campo Cedula no puede estar vacio")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El Campo Telefono no puede estar vacio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El Campo Email no puede estar vacio")]
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
