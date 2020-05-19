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
        public CompteCredit(string type, string prenom, string nom, int limiteCredit, int numero) : base(type, prenom, nom, numero)// Constructeur pour compte existant dans le fichier texte.
        {
            _limiteCredit = limiteCredit;
        }
        public CompteCredit(string type, string prenom, string nom, int limiteCredit) : base(type, prenom, nom)// Constructeur qui crée un nouveau compte.
        {
            _limiteCredit = limiteCredit;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"{Convert.ToString(_numeroDeCompte)}  Crédit   {_nom}, {_prenom} Limite de crédit {_limiteCredit}");
            return sb.ToString();
        }

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
