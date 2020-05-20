using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    /// <summary>
    /// La classe spécialisée Retrait de Transaction utilise le polymorphisme afin de pouvoir être manipulée à travers l’interface de la classe de base Transaction. 
    /// </summary>
    class Retrait : Transaction // Retrait est une spécialisation de la classe abstraite Transaction
    {
        #region        public Retrait(int numeroCompte, double montant) : base (numeroCompte, montant)
        /// <summary>
        /// Constructeur qui hérite de la classe parent Transaction
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Retrait(int numeroCompte, double montant) : base (numeroCompte, montant)
        {
        }
        #endregion

        #region        public override void Sauvegarder(StreamWriter canalEcriture)
        /// <summary>
        /// Méthode pour l'écriture de la transaction dans le canal d'écriture qui dirige l'enregistrement vers transactions.txt
        /// </summary>
        /// <param name="canalEcriture"></param>
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"{_numeroCompte};R;{_montant};{_aujourDHui}");// Écriture de la ligne de la transaction dans le fichier transactions.txt
        }
        #endregion
    }
}
