using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementManager
{
    public class Movement
    {
        #region Proprietà e Costruttore

        public decimal Importo { get; set; }
        public DateTime DataMovimento { get; set; }

        //Per comodità di TEST ho inserito anche il costruttore base di default
        public Movement() { }

        //ctor con Parametri 
        public Movement (decimal importo, DateTime dataMovimento) 
        {
            Importo = importo;
            DataMovimento = dataMovimento;
        }

        #endregion

        #region Override ToString()
        public override string ToString()
        {
            return $"-Data Movimento: {this.DataMovimento.ToShortDateString()}\n" +
                  $"-Importo: {this.Importo}\n";
        }
        #endregion
    }
}
