using PrimerParcial3k22019.Helpers;
using PrimerParcial3k22019.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial3k22019.Repositorios
{
    class TitularesRepositorio
    {
        private AccesoBD _BD;

        public DataTable ObtenerTitular()
        {
            //string sqltxt = "SELECT Id,Nombre,Apellido,Calle,NumDoc,id_TipoDocumento FROM Cliente";
            string sqltxt = "SELECT * From Titulares";
            return _BD.consulta(sqltxt);
        }

        public Titular ObtenerTitular(string dni)
        {
            string sqltxt = $"SELECT * FROM Titulares WHERE DniTitular = {dni}";
            var tablaTemporal = _BD.consulta(sqltxt);
            if (tablaTemporal.Rows.Count == 0)
                return null;
            var titular = new Titular();
            foreach (DataRow fila in tablaTemporal.Rows)
            {
                if (fila.HasErrors)
                    continue;
                titular.DniTitular = fila.ItemArray[0].ToString();
                titular.nombre = fila.ItemArray[1].ToString();
               
            }
            return titular;
        }

        public TitularesRepositorio()
        {
            _BD = new AccesoBD();
        }
    }
}
