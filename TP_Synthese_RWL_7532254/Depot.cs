using System.IO;

namespace TPSynthese
{
    /// <summary>
    /// La classe spécialisée Depot de Transaction utilise le polymorphisme afin de pouvoir être manipulée à travers l’interface de la classe de base Transaction. 
    /// </summary>
    class Depot : Transaction// Depot est une spécialisation de la classe abstraite Transaction
    {
        #region        public Depot(int numeroCompte, double montant) : base(numeroCompte, montant)
        /// <summary>
        /// Constructeur qui hérite de la classe parent Transaction
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Depot(int numeroCompte, double montant) : base(numeroCompte, montant)
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
            canalEcriture.WriteLine($"{_numeroCompte};D;{_montant};{_aujourDHui}");// Écriture de la ligne de la transaction dans le fichier transactions.txt
        }
        #endregion
    }
}
