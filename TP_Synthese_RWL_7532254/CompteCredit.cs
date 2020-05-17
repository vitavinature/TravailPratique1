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
        public CompteCredit(string type, string prenom, string nom, double limiteCredit, int numero) : base(type, prenom, nom, numero)// Constructeur pour compte existant dans le fichier texte.
        {
            _limiteCredit = limiteCredit;
        }
        public CompteCredit(string type, string prenom, string nom, double limiteCredit) : base(type, prenom, nom)// Constructeur qui crée un nouveau compte.
        {
            _limiteCredit = limiteCredit;
        }
        public abstract void Sauvegarder(ref StreamWriter fichier)
        {
            // Ouverture du canalEcriture pour l'écriture dans le fichier "comptes.txt"
            using (StreamWriter canalEcriture = new StreamWriter(fichier, true))// true est utilisé pour que le nouveau compte soit ajouté aux comptes existants.
            {
                canalEcriture.WriteLine($"{nouveaucompte.Type};{nouveaucompte.NumeroDeCompte};{nouveaucompte.Prenom};{nouveaucompte.Nom};{nouveauCompte._limiteCredit}");
            }
        }
        public int LimiteCredit { get { return _limiteCredit; } }// Pour que la limite de crédit soit accessible dans la banque, cette propriété est nécessaire.

        private double _limiteCredit;

    }


}
