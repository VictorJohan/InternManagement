using System;
using System.Collections.Generic;

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
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Pasante> Pasantes { get; set; }
    }
}
