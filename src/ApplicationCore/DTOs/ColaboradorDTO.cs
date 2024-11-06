using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ColaboradorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool EsProfesor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public string Puesto { get; set; }
        public decimal? Nomina { get; set; }
    }
}
