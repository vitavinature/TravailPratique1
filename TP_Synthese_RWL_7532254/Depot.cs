using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Depot : Transaction// Depot est une spécialisation de la classe abstraite Transaction
    {
        /// <summary>
        /// Constructeur qui hérite de la classe parent Transaction
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Depot(int numeroCompte, double montant) : base(numeroCompte, montant)
        {

        }
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"{_numeroCompte};D;{_montant};{_aujourDHui}");// Écriture de la ligne de la transaction dans le fichier transactions.txt
        }
    }
}
