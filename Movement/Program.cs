using System;
using System.Collections.Generic;

namespace MovementManager
{
    class Program
    {  //Lista contenente tutti i conti
        private static List<Account> Banca = new List<Account>();

        static void Main(string[] args)
        {


            #region Inizio Main vero e proprio
            Console.WriteLine("-------Welcome to Movement Manager System---------");
            do
            {
                Console.WriteLine("Scegli un'opzione:");
                Console.WriteLine("1) Crea un nuovo Account");
                Console.WriteLine("2) Ritira (-)");
                Console.WriteLine("3) Versa (+)");
                Console.WriteLine("4) Stampa dettagli Account");
                Console.WriteLine("5) Stampa Lista Movimenti");
                Console.WriteLine("0) Esci");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        CreateAccount();
                        break;
                    case '2':
                        Withdraw();
                        break;
                    case '3':
                        Deposit();
                        break;
                    case '4':
                        PrintAccountDetails();
                        break;
                    case '5':
                        PrintMovemDetails();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;

                }
            } while (true);

        }



        #endregion

            #region Metodi di recupero dati per la Console
      
        private static void CreateAccount()
        {
            //serve il nome della banca e il saldo iniziale.
            //Data ultima operazione decido di impostarla di default a OGGI 

            Console.WriteLine("\nNome della banca:");
            string nomeBanca = Console.ReadLine();

            Console.WriteLine("\nSaldo iniziale:");
            decimal.TryParse(Console.ReadLine(), out decimal saldo);

            DateTime dataOperazione = DateTime.Today;

            Account a = new Account(nomeBanca, saldo, dataOperazione);
            Banca.Add(a);

            Console.WriteLine($"Account creato con successo.\n" +
                             $"Riepilogo dettagli \n{a.ToString()}");
        }

        private static void Deposit()
        {
            //Seleziono l'importo prima così che io possa richiamare la funzione sia da versa che da deposita 
            //cambiando solo l'importo e non ripetendo tutto il codice
            Console.WriteLine("\nQuanto vuoi versare?:");
            decimal.TryParse(Console.ReadLine(), out decimal importo);

            //Richiamo la fuzione che gestisce le operazioni 
            SceltaMovement(importo);
            Console.WriteLine("\nOperazione Completata con successo\n");

        }

        private static void Withdraw()
        {
            Console.WriteLine("\nQuanto vuoi ritirare?:");
            decimal.TryParse(Console.ReadLine(), out decimal importo);

            //Richiamo la fuzione che gestisce le operazioni 
            SceltaMovement(-importo);
            Console.WriteLine("\nOperazione Completata con successo\n");
        } 

        public static void SceltaMovement(decimal importo)
        {

            //Considero come se il numero di conto l'utente lo conoscesse
            //Il mio id di prova è 1

            Console.WriteLine("\nScegliere il numero di Conto:");
            int.TryParse(Console.ReadLine(), out int numeroConto);

            Account a = RecuperaConto(numeroConto);
            if (a == null)
            {
                Console.WriteLine($"Il conto numero {a.NumeroConto} non esiste");
            }


            //Si potrebbe mettere di default il giorno di oggi ma poi non avremmo modo di verificare
            //l'aggiornamento dell'ultima Data Operazione
            Console.WriteLine("\nData dell' Operazione? (anno,mese,giorno):");
            DateTime.TryParse(Console.ReadLine(), out DateTime dataOp);

            Console.WriteLine("\nChe tipo di movimento vuoi effettuare?:");
            Console.WriteLine("1) Movimento Classico (Movement)");
            Console.WriteLine("2) Movimento di Denaro (Cash Movement)");
            Console.WriteLine("3) Movimento di Trasferimento (Transfert Movement)");
            Console.WriteLine("4) Movimento su Carta di Credito (Credit Card Movemen)");

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    Movement m = new Movement(importo, dataOp);
                    a = a + m;
                    break;
                case '2':
                    Console.WriteLine("\nChi è l'Esecutore?");
                    string esecutore = Console.ReadLine();
                    CashMovement cm = new CashMovement(importo, dataOp, esecutore);
                    a = a + cm;
                    break;
                case '3':
                    Console.WriteLine("\nQual'è la banca d'origine?");
                    string bancaOrigine = Console.ReadLine();
                    Console.WriteLine("\nQual'è la banca di Destinazione?");
                    string bancaDest = Console.ReadLine();
                    TransfertMovement tm = new TransfertMovement(importo, dataOp, bancaOrigine, bancaDest);
                    a = a + tm;
                    break;
                case '4':
                    Console.WriteLine("\nQual'è il numero di Carta da ricaricare? (Esempio: IT12U13)");
                    string numeroCarta = Console.ReadLine();
                    Console.WriteLine("\nQual'è il tipo di Carta da caricare?");
                    Console.WriteLine("1)AMEX 2)VISA 3)MASTERCARD 4)ALTRA\n");
                    TipoCarta tipo = TipoCarta.OTHER;

                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            tipo = TipoCarta.AMEX;
                            break;
                        case '2':
                            tipo = TipoCarta.VISA;
                            break;
                        case '3':
                            tipo = TipoCarta.MASTERCARD;
                            break;
                    }

                    CreditCardMovement ccm = new CreditCardMovement(importo, dataOp, numeroCarta, tipo);
                    a = a + ccm;
                    break;
                default:
                    Console.WriteLine("Scelta non valida");
                    break;
            }


        }

        private static void PrintAccountDetails()
        {

            Console.WriteLine("\nScegliere il numero di Conto:");
            int.TryParse(Console.ReadLine(), out int numeroConto);

            Account a = RecuperaConto(numeroConto);
            Console.WriteLine(a.ToString());

        }

        private static void PrintMovemDetails()
        {

            Console.WriteLine("\nScegliere il numero di Conto:");
            int.TryParse(Console.ReadLine(), out int numeroConto);

            Account a = RecuperaConto(numeroConto);
            Console.WriteLine(a.Statement());

        }

        public static Account RecuperaConto(int numeroConto)
        {
            Account result;

            foreach (Account a in Banca)
            {
                if (a.NumeroConto == numeroConto)
                {
                    result = a;
                    return result;
                }

            }
            return null;
        }

        #endregion






        #region TEST di Funzionalità: non fanno parte dell'implementazione finale.

        //string dummyBanca = "DB";
        //Account a1 = new Account(dummyBanca, 0, new DateTime(2021, 04, 1));

        ////Check autoincremento ID ---OK
        //Console.WriteLine($"Account creato: {a1.NumeroConto}");
        //Account a2 = new Account(dummyBanca, 10, new DateTime(2021, 04, 1));
        //Console.WriteLine($"Account creato: {a2.NumeroConto}");

        //Movement m1 = new Movement(100, new DateTime(2021, 05, 1));
        //Movement m2 = new Movement(50, new DateTime(2021, 05, 10));


        ////Check Operazioni attivo e passivo ---OK
        //Console.WriteLine($"Il saldo originale era: {a1.Saldo}\n" +
        //                     $"Data Ultima Operazione aggiornata: {a1.DataUltimaOperazione}");

        //Account aUpdated = a1 + m1;
        //Console.WriteLine($"Il saldo aggiornato con deposito è: {aUpdated.Saldo}\n" +
        //                     $"Data Ultima Operazione aggiornata: {aUpdated.DataUltimaOperazione}");


        //Account aUpdatedWit = a1 - m2;
        //Console.WriteLine($"Il saldo aggiornato con ritiro è: {aUpdatedWit.Saldo}\n" +
        //                     $"Data Ultima Operazione aggiornata: {aUpdatedWit.DataUltimaOperazione}");

        //Console.Clear();
        ////Check ToString di Account --OK
        //Console.WriteLine(a1.ToString());
        //Console.WriteLine(a2.ToString());

        ////Check Statement di Account ---OK
        //Console.WriteLine(a1.Statement());
        //Console.WriteLine(a2.Statement());

        //Console.Clear();
        ////Check ToString dei movimenti ---OK
        //CashMovement mc = new CashMovement(100, new DateTime(2021, 05, 1), "Bank Manager");
        //TransfertMovement tm = new TransfertMovement(50, new DateTime(2021, 05, 10), "BO", "MI");
        //CreditCardMovement ccm = new CreditCardMovement(50, new DateTime(2021, 05, 10), "IT120", TipoCarta.MASTERCARD);

        //Console.WriteLine(mc.ToString());
        //Console.WriteLine(tm.ToString());
        //Console.WriteLine(ccm.ToString());

        //Console.Clear();
        ////Check aggiunta dei movimenti alla lista --OK
        //Console.WriteLine($"Il saldo originale era: {a1.ToString()}");

        //Account aUpdatedDiff = a1 + mc;
        //Console.WriteLine($"{aUpdatedDiff.ToString()}\n{mc.ToString()}\n");


        //Account aUpdatedWitDiff = a1 - ccm;
        //Console.WriteLine($"{aUpdatedWitDiff.ToString()}\n{ccm.ToString()}\n");

        //Account a2Updated = a2 - tm;
        //Console.WriteLine($"{a2Updated.ToString()}\n{tm.ToString()}\n");

        //Console.Clear();
        #endregion
    }
}
