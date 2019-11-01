using PrimerParcial3k22019.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial3k22019.Modelos
{
    public class Familiar
    {
        FamiliaresRepositorio familiaresRepositorio = new FamiliaresRepositorio();    
        public string Id { get; set; }

        public string DniFamiliar { get; set; }

        public string DniTitular { get; set; }

        public string Nombre { get; set; }

        public string ParentescoId { get; set; }

        public bool NombreValido()
        {
            if (!string.IsNullOrEmpty(Nombre) && Nombre.Length < 50)
                return true;
            return false;
        }
        public bool DNIValido()
        {
            int number;
            if (!string.IsNullOrEmpty(DniFamiliar) && DniFamiliar.Length == 8 && int.TryParse(DniFamiliar, out number))
                return true;
            return false;
        }

        public bool DniRepetido(string dni, string dnit)
        {
            var familiares = familiaresRepositorio.ObtenerFamiliaresTitular(dnit);
            ArrayList lista = new ArrayList();
            foreach (DataRow row in familiares.Rows)
            {
                lista.Add(Convert.ToString(row["Dni"]));
            }
            if (lista.Contains(dni))
            {
                return true;
            }
            return false;
        }

    }
}
