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
    public class FamiliaresRepositorio
    {
        private AccesoBD _BD;

        public string VerificarConexion()
        {
            var sql = "select count (*) as total from Titulares;";
            var filas = AccesoBD.Singleton().consulta(sql);
            foreach (DataRow fila in filas.Rows)
            {
                return fila.ItemArray[0]?.ToString();
            }
            return null;
        }

        public IList<Familiar> ObtenerTodos()
        {
            
            var familiares = new List<Familiar>();            

            return familiares;
        }

        public DataTable ObtenerFamiliares()
        {
            string sqltxt = "SELECT * FROM Familiares";
            return _BD.consulta(sqltxt);
        }


        public DataTable ObtenerFamiliaresTitular(string dni)
        {
           
            string sqltxt1 = $"SELECT f.Id, f.DniTitular, f.DniFamiliar as Dni, f.NombreApellido, " +
                $"f.ParentescoId, p.Nombre as TipoParentesco FROM Familiares F, Parentescos P " +
                $"WHERE f.ParentescoId = p.Id AND f.DniTitular = {dni}";
            return _BD.consulta(sqltxt1);
        }

        public Familiar ObtenerFamiliar(string familiarId)
        {
            string sqltxt = $"SELECT * FROM [dbo].[Familiares] WHERE Id = {familiarId}";
            var tablaTemporal = _BD.consulta(sqltxt);

            if (tablaTemporal.Rows.Count == 0)
                return null;

            var familiar = new Familiar();
            foreach (DataRow fila in tablaTemporal.Rows)
            {
                if (fila.HasErrors)
                    continue; // no corto el ciclo

                familiar.Id = fila.ItemArray[0].ToString(); // Codigo
                familiar.DniTitular = fila.ItemArray[1].ToString(); 
                familiar.DniFamiliar = fila.ItemArray[2].ToString(); 
                familiar.Nombre = fila.ItemArray[3].ToString();
                familiar.ParentescoId = fila.ItemArray[4].ToString();
            }

            return familiar;
        }


        public FamiliaresRepositorio()
        {
            _BD = new AccesoBD();
        }

        public bool Guardar(Familiar familiar)
        {
            string sqltxt = $"INSERT[dbo].[Familiares]([DniTitular],[DniFamiliar],[NombreApellido]," +
                $"[ParentescoId])" +
                $"VALUES " +
                $"('{familiar.DniTitular}', " +
                $"'{familiar.DniFamiliar}', " +
                $"'{familiar.Nombre}'," +
                $"'{familiar.ParentescoId}')";
            return _BD.EjecutarSQL(sqltxt);
        }

        public bool Eliminar(string familiarId)
        {
            string sqltxt = $"DELETE FROM [dbo].[Familiares] WHERE id ={familiarId}";
            return _BD.EjecutarSQL(sqltxt);
        }

        public bool Editar(Familiar familiar)
        {
            string sqltxt = $"UPDATE [dbo].[Familiares] SET NombreApellido ='{familiar.Nombre }'" +
                $", ParentescoId = { familiar.ParentescoId }" +
                $", DniFamiliar = { familiar.DniFamiliar }" +
                $" WHERE Id = {familiar.Id}";

            return _BD.EjecutarSQL(sqltxt);
        }
    }
}
