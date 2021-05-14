using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementManager
{
    public class Account 
    {
       

        #region Proprietà

        //Scelta di intendere il numero conto come un valore che si autoincrementa

        private static int _numeroConto;

        public int NumeroConto { get; set; }
        public string NomeBanca { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataUltimaOperazione { get; set; }

        //Lista contenente tutti i movimenti
        public static List<Movement> ListaMovimenti { get; set; } = new List<Movement>();


        #endregion


        #region ctr
        public Account(string nomeBanca, decimal saldo, DateTime dataUltimaOp)
        {
            NumeroConto = ++_numeroConto;
            NomeBanca = nomeBanca;
            Saldo = saldo;
            DataUltimaOperazione = dataUltimaOp;
        }


        #endregion

        #region Overload Operatori
        public static Account operator +(Account a, Movement m)
        {
            //Aggiungo all'account a il movimento m
            ListaMovimenti.Add(m);

            //Aggiorno saldo e data
            a.Saldo += m.Importo;
            a.DataUltimaOperazione = m.DataMovimento;

            //restituisco l'account modificato
            return a;

        }

        public static Account operator -(Account a, Movement m) 
        {
            //E' uguale allìaggiunta ma l'importo deve essere sottratto quindi imposto importo = -importo
            m.Importo = -m.Importo;
            return (a + m);
        }




        #endregion

        #region Metodi
        public override string ToString()
        {
            return $"Prospetto del conto numero: {this.NumeroConto}: \n" +
                   $"-Nome della Banca: {this.NomeBanca}\n" +
                   $"-Saldo conto: {this.Saldo}\n" +
                   $"-Data Ultima Operazione: {this.DataUltimaOperazione.ToShortDateString()}\n";
        }


        public string Statement() 
        {
            Console.WriteLine("------Lista movimenti-------");
            string lista = "";
            foreach (Movement m in ListaMovimenti) 
            {
                lista += $"Data Operazione: {m.DataMovimento} - Importo corrispondente: {m.Importo} \n";
            }

            return this.ToString() + lista;
        }

        #endregion
    }
}
