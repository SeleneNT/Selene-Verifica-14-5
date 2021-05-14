using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementManager
{
    public class CashMovement: Movement
    {
        #region Proprietà e Costruttore

        public string Esecutore { get; set; }
        public CashMovement() { }
        public CashMovement(decimal importo, DateTime dataMovimento,string esecutore)
            :base(importo, dataMovimento)
        {
            Esecutore = esecutore;
        }
        #endregion

        #region Override ToString()
        public override string ToString()
        {
            return base.ToString()+  $"-Esecutore:{ this.Esecutore}";
                
        }
        #endregion
    }
}
