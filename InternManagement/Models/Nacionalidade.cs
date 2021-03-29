using System;
using System.Collections.Generic;

#nullable disable

namespace InternManagement.Models
{
    public partial class Nacionalidade
    {
        public Nacionalidade()
        {
            Pasantes = new HashSet<Pasante>();
        }

        public int NacionalidadId { get; set; }
        public string Nacionalidad { get; set; } 
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual ICollection<Pasante> Pasantes { get; set; }
    }
}
