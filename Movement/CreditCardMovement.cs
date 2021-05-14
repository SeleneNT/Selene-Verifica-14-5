using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementManager
{

    public enum TipoCarta
    {
        AMEX,
        VISA,
        MASTERCARD,
        OTHER
    }

    public class CreditCardMovement : Movement
    {
        #region Proprietà e Costruttore

        public TipoCarta Tipo { get; set; }
        public string NumeroCarta { get; set; }

        public CreditCardMovement() { }
        public CreditCardMovement(decimal importo, DateTime dataMovimento, string numeroCarta, TipoCarta tipo)
             : base(importo, dataMovimento)
        {
            NumeroCarta = numeroCarta;
            Tipo = tipo;
        }

        #endregion

        #region Override ToString()
        public override string ToString()
        {
            return base.ToString() + 
                $"-Numero di Carta: {this.NumeroCarta}\n" +
                 $"-Tipo di Carta: {this.Tipo}";
        }

        #endregion
    }
}
