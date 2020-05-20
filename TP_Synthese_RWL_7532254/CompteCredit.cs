using System;
using System.IO;
using System.Text;

namespace TPSynthese
{
    /// <summary>
    /// La classe spécialisée CompteCrédit de Compte utilise le polymorphisme afin de pouvoir être manipulée à travers l’interface de la classe de base Compte. 
    /// </summary>
    class CompteCredit : Compte
    {
        #region        public CompteCredit(string type, string prenom, string nom, int limiteCredit, int numero) : base(type, prenom, nom, numero)
        /// <summary>
        /// Constructeur qui hérite de la classe Compte.
        /// Constructeur pour compte existant dans le fichier texte.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="limiteCredit">limite de crédit du compte</param>
        /// <param name="numero">numéro du compte</param>
        public CompteCredit(string type, string prenom, string nom, int limiteCredit, int numero) : base(type, prenom, nom, numero)
        {
            _limiteCredit = limiteCredit;// Seuls les comptes crédits ont une limite de crédit.
        }
        #endregion

        #region        public CompteCredit(string type, string prenom, string nom, int limiteCredit) : base(type, prenom, nom)
        /// <summary>
        /// Constructeur qui hérite de la classe Compte.
        /// Constructeur qui crée un nouveau compte.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="limiteCredit">limite de crédit du compte</param>
        /// <param name="numero">numéro du compte</param>
        public CompteCredit(string type, string prenom, string nom, int limiteCredit) : base(type, prenom, nom)
        {
            _limiteCredit = limiteCredit;
        }
        #endregion

        #region        public override void Retirer(double montant)
        /// <summary>
        /// Cette méthode calcule le nouveau solde suite à un retrait. Le montant du retrait est soustrait du solde précédent.
        /// Cette méthode override redéfini la méthode virtuelle dans Compte (celle-là ne s'applique qu'aux comptes chèques et épargne).
        /// Le comportement est différent selon le type de compte.
        /// Une méthode override redéfini Retirer dans la classe CompteCredit.
        /// Puisque le solde peut être négatif, mais jamais moins que la valeur négative de la limite de crédit.
        /// </summary>
        /// <param name="montant">Seul le montant du retrait est nécessaire.</param>
        public override void Retirer(double montant)
        {
            if ((_solde - montant) < (-_limiteCredit))// Le solde peut être négatif, mais jamais moins que la valeur négative de la limite de crédit.
            {
                throw new Exception("Erreur le retrait est trop important; il y a insuffisance de crédit.");// Une exception est levée si la limite est franchie.
            }
            else
            {
                _solde -= montant;// Le montant est soustrait du solde précédent.
            }
        }
        #endregion

        #region        public override void Sauvegarder(StreamWriter canalEcriture)
        /// <summary>
        /// Méthode override qui sauvegarde, (écrit) l'information du compte dans le fichier d'écriture, via le canal d'écriture.
        /// Cette méthode est spécifique au CompteCredit. C'est une méthode override de la méthode abstract dans Compte.
        /// </summary>
        /// <param name="canalEcriture"></param>
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"R;{_numeroDeCompte};{_prenom};{_nom};{_limiteCredit}");// Les informations du compte sont écrites dans le fichier comptes.txt.
        }
        #endregion

        #region        public override string ToString()
        /// <summary>
        /// Méthode virtuelle qui est particulière aux comptes de crédits.
        /// L'information du type de compte et la limite de crédit est ajoutée aux informations de base soit le numéro, nom et prénom.
        /// Un string builder est utilisé pour rassembler les informations sous la forme d'une chaîne de caractères.
        /// </summary>
        /// <returns>Retourne la ligne (chaîne de caractères) à afficher lors de l'affichage de la liste des comptes.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();// Un stringbuilder est utilisé.
            sb.AppendFormat($"{Convert.ToString(_numeroDeCompte)}  Crédit   {_nom}, {_prenom} Limite de crédit {_limiteCredit}");
            // Retourne une chaîne de format composite sous la forme d'une chaîne de caractères.
            return sb.ToString();
        }
        #endregion

        private readonly int _limiteCredit;// Déclaratin de l'attribut.
    }
}
