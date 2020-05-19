using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
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

        #region        public override string ToString()
        /// <summary>
        /// Méthode virtuelle qui est particulière aux comptes de crédits.
        /// L'information du type de compte et la limite de crédit est ajoutée aux informations de base soit le numéro, nom et prénom.
        /// Un string builder est utilisé pour rassembler les informations sous la forme d'une chaîne de caractères.
        /// </summary>
        /// <returns>Retourne la ligne à afficher lors de l'affichage de la liste des comptes</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"{Convert.ToString(_numeroDeCompte)}  Crédit   {_nom}, {_prenom} Limite de crédit {_limiteCredit}");
            return sb.ToString();
        }
        #endregion

        public override void Retirer(double montant)
        {
            if (_solde - montant < -_limiteCredit)
            {
                throw new Exception("Erreur le retrait est trop important; il y a insuffisance de crédit.");
            }
            else
            {
                _solde -= montant;
            }
        }


        public override void Sauvegarder(StreamWriter canalEcriture)
        {
            canalEcriture.WriteLine($"R;{_numeroDeCompte};{_prenom};{_nom};{_limiteCredit}");
        }
        //public int LimiteCredit { get { return _limiteCredit; } }// Pour que la limite de crédit soit accessible dans la banque, cette propriété est nécessaire.

        private readonly int _limiteCredit;

    }


}
