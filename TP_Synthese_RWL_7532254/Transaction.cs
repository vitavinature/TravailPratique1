using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    abstract class Transaction
    {
        public Transaction(int numeroCompte, double montant)
        {
            _aujourDHui = DateTime.Today.ToString("yyyy'-'MM'-'dd");// Prendre la date du jour et la convertir en texte du bon format
            _numeroCompte = numeroCompte;
            _montant = montant;
        }

        public void Sauvegarder(StreamWriter canalEcriture)
        {
            string numero = Convert.ToString(_numeroCompte);
            string montant = Convert.ToString(_montant);

            string type = "D";//*************************************************************** À CORRIGER *********************************

            // Conversion des valeurs numériques en valeurs textes pour passer à l'écriture du fichier transaction.
            canalEcriture.WriteLine($"{numero};{type};{montant};{_aujourDHui}");
// Appel de la méthode pour sauvegarder la transaction dans le fichier des transactions.

        }
        private string _aujourDHui;
        private int _numeroCompte;
        private double _montant;

    }
  
}
