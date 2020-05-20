using System.IO;

namespace TPSynthese
{
    /// <summary>
    /// La classe spécialisée CompteCheque de Compte utilise le polymorphisme afin de pouvoir être manipulée à travers l’interface de la classe de base Compte. 
    /// </summary>
    class CompteCheque : Compte// CompteCheque hérite de la classe de base Compte
    {
        #region        public CompteCheque(string type, string prenom, string nom, int numero) : base(type, prenom, nom, numero)
        /// <summary>
        /// Constructeur pour compte existant dans le fichier texte.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="numero"></param>
        public CompteCheque(string type, string prenom, string nom, int numero) : base(type, prenom, nom, numero)
        {
        }
        #endregion

        #region        public CompteCheque(string type, string prenom, string nom) : base(type, prenom, nom)
        /// <summary>
        /// Constructeur qui crée un nouveau compte.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        public CompteCheque(string type, string prenom, string nom) : base(type, prenom, nom)
        {
        }
        #endregion

        #region        public override void Sauvegarder(StreamWriter canalEcriture)
        /// <summary>
        /// Méthode override qui sauvegarde, (écrit) l'information du compte dans le fichier d'écriture, via le canal d'écriture.
        /// Cette méthode est spécifique au CompteCheque. C'est une méthode override de la méthode abstract dans Compte.
        /// </summary>
        /// <param name="canalEcriture"></param>
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"C;{_numeroDeCompte};{_prenom};{_nom}");// Les informations du compte sont écrites dans le fichier comptes.txt.
        }
        #endregion
    }
}
