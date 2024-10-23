using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("estudiantes")]
	public class Estudiante
	{
		[Key]
		public int id { get; set; }
		public string nombre { get; set; }
		public int edad { get; set; }
		public string correo { get; set; }
	}
}

