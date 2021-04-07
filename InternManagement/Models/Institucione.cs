using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace InternManagement.Models
{
    public partial class Institucione
    {
        public Institucione()
        {
            Pasantes = new HashSet<Pasante>();
        }

        public int InstitucionId { get; set; }
        [Required(ErrorMessage ="El Campo Nombre no puede estar vacio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo Telefono no puede estar vacio")]
        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Teléfono no válido.")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El Campo Email no puede estar vacio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Campo Direccion no puede estar vacio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Campo Fecha de creacion no puede estar vacio")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual ICollection<Pasante> Pasantes { get; set; }
    }
}
