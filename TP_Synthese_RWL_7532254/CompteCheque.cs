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
        public void Sauvegarder(CompteCheque compteCheque)
        {
            // Ouverture du canalEcriture pour l'écriture dans le fichier "comptes.txt"
            using (StreamWriter canalEcriture = new StreamWriter("comptes.txt", true))// true est utilisé pour que le nouveau compte soit ajouté aux comptes existants.
            {
                canalEcriture.WriteLine($"{compteCheque.Type};{compteCheque.NumeroDeCompte};{compteCheque.Prenom};{compteCheque.Nom}");
            }
        }
    }
}
