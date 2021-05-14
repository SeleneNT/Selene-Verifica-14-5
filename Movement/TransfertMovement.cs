using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementManager
{
    public class TransfertMovement : Movement
    {
        #region Proprietà e Costruttore
        public string BancaOrigine { get; set; }
        public string BancaDestinazione { get; set; }

        public TransfertMovement() { }
        public TransfertMovement(decimal importo, DateTime dataMovimento, string bancaOrigine, string bancaDest)
          : base(importo, dataMovimento)
        {
            BancaOrigine = bancaOrigine;
            BancaDestinazione = bancaDest;
        }

        #endregion

        #region Override ToString()
        public override string ToString()
        {
            return base.ToString() +
                $"-Banca di Origine: {this.BancaOrigine}\n" +
                $"-Banca di Destinazione: {this.BancaDestinazione}";
        }

        #endregion

    }
}
