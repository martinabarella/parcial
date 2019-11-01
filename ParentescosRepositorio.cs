using PrimerParcial3k22019.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial3k22019.Repositorios
{
    class ParentescosRepositorio
    {
        private AccesoBD _BD;

        public ParentescosRepositorio()
        {
            _BD = new AccesoBD();
        }

        public DataTable ObtenerParentescos()
        {
            //string sqltxt = "SELECT Id,Nombre,Apellido,Calle,NumDoc,id_TipoDocumento FROM Cliente";
            string sqltxt = "SELECT * From Parentescos";
            return _BD.consulta(sqltxt);
        }


    }
}
