using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Tbl_Colaboradores")]
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Birthday { get; set; }
        public bool EsProfesor { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Profesor Profesor { get; set; }
        public Admin Admin { get; set; }
    }
}
