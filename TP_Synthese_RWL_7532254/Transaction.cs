using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    abstract class Transaction // La classe de base Transaction est abstraite. Elle représente des concepts abstraits desquels les classes Depot et Retrait,
                               // dites concrètes, héritent (dérivent), mais ne peuvent pas être instanciées.
                               // À partir du moment où une classe contient au moins une méthode abstraite, elle doit aussi être déclarée abstraite, (abstract). 
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

        #region        public abstract void Sauvegarder(StreamWriter canalEcriture)
        /// <summary>
        /// Méthode pour l'écriture de la transaction dans le canal d'écriture qui dirige l'enregistrement vers transactions.txt
        /// </summary>
        /// <param name="canalEcriture"></param>
        public abstract void Sauvegarder(StreamWriter canalEcriture);// Une méthode abstraite ne contient pas de définition.
        // Une méthode abstraite doit absolument être définie dans toutes les classes dérivées (soit Depot et Retrait).
        // Si une classe qui hérite de « Transaction » ne définit pas la méthode « Sauvegarder», le programme ne compilera pas. 
        #endregion

        private protected readonly string _aujourDHui;
        private protected readonly int _numeroCompte;
        private protected readonly double _montant;

    }

}
