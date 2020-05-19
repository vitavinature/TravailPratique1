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
        #region        public Transaction(int numeroCompte, double montant)
        /// <summary>
        /// Constructeur des transactions
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Transaction(int numeroCompte, double montant)
        {
            _aujourDHui = DateTime.Today.ToString("yyyy'-'MM'-'dd");// Prendre la date du jour et la convertir en texte du bon format
            _numeroCompte = numeroCompte;
            _montant = montant;
        }
        #endregion

        #region        public void Sauvegarder(StreamWriter canalEcriture)
        /// <summary>
        /// Méthode pour l'écriture de la transaction dans le canal d'écriture qui dirige l'enregistrement vers transactions.txt
        /// </summary>
        /// <param name="canalEcriture"></param>
        public void Sauvegarder(StreamWriter canalEcriture)
        {
            string numero = Convert.ToString(_numeroCompte);// déclaration et initialisation des variables de travail
            string montant = Convert.ToString(_montant);
            // Conversion des valeurs numériques en valeurs textes pour passer à l'écriture du fichier transaction.

            string type = "D";//*************************************************************** À CORRIGER *********************************

            canalEcriture.WriteLine($"{numero};{"D ou R"};{montant};{_aujourDHui}");// Écriture de la ligne de la transaction dans le fichier transactions.txt
        }
        #endregion

        private protected readonly string _aujourDHui;
        private protected readonly int _numeroCompte;
        private protected readonly double _montant;

    }

}
