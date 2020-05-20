using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canalEcriture"></param>
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"{_numeroCompte};R;{_montant};{_aujourDHui}");// Écriture de la ligne de la transaction dans le fichier transactions.txt
        }
    }
}
