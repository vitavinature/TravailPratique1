using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteCheque : Compte
    {
        public CompteCheque(string type, string prenom, string nom, int numero) : base(type, prenom, nom, numero)// Constructeur pour compte existant dans le fichier texte.
        {
        }
        public CompteCheque(string type, string prenom, string nom) : base(type, prenom, nom)// Constructeur qui crée un nouveau compte.
        {
        }
        //public abstract void Sauvegarder(ref StreamWriter fichier, Compte nouveauCompte)
        public override void Sauvegarder(StreamWriter canalEcriture)
        {
                canalEcriture.WriteLine($"C;{_numeroDeCompte};{_prenom};{_nom}");
        }
    }
}
