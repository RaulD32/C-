using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class ColaboradorCreateDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool EsProfesor { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }  // Solo si es profesor
        public string Puesto { get; set; }        // Solo si es administrativo
        public decimal? Nomina { get; set; }
    }
}
