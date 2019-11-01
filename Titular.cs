using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerParcial3k22019.Modelos
{
    public class Titular
    {
        public string DniTitular { get; set; }

        public string nombre { get; set; }

        public bool DNIValido()
        {
            int number;
            if (!string.IsNullOrEmpty(DniTitular) && DniTitular.Length == 8 && int.TryParse(DniTitular, out number))
                return true;
            return false;
        }



    }

   
}
