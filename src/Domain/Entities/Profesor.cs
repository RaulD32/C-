using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Tbl_Profesor")]
    public class Profesor
    {
        public int Id { get; set; }
        public int FkColaborador { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }

        public Colaborador Colaborador { get; set; }
    }
}
