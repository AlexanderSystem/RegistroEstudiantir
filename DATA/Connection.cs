using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /*
     Esta clase Connection se utiliza para establecer una conexión a la base de datos y proporciona una
     propiedad _Estudiante que ofrece acceso a una tabla de la base de datos que contiene registros de
     tipo Estudiante
    */
    public class Connection:DataConnection
    {

        public Connection() : base("conexionSql") { }

        public ITable<Estudiante> _Estudiante

        {
            get { return GetTable<Estudiante>(); }
        }

    }
}
