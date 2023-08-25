using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    /* 
     la clase Estudiante se utiliza para representar la información de un estudiante, incluyendo su 
     identificación, sección, nombre, apellido, correo electrónico, imagen y fecha de inicio. Esta 
     representación de datos es útil para interactuar con una base de datos o para mantener información
     de estudiantes en una aplicación.
    */
    public class Estudiante
    {
        [PrimaryKey, Identity] 
        public int ID { set; get; }
        public string Seccion { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get;}
        public string Email { set; get; }
        public byte[] Imagen { set; get; }
        public DateTime FechaInicio { set; get; }
    }
}
