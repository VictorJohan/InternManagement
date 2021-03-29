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
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "El Campo Direccion no puede estar vacio")]
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual ICollection<Pasante> Pasantes { get; set; }
    }
}
