using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_2
{
    class Etudiant : Personne
    {
        public Etudiant(string prenom, string nom, string matricule) : base(prenom, nom)
        {

    
                _matricule = Convert.ToInt32(matricule);
                if (_matricule < 1000000 || _matricule > 9999999)
                {
                    throw new Exception("Le matricule ne contient pas 7 chiffres.");
                }

  

        }

        public void AjouterNote(double note)
        {
            _noteTotale += note;
            if (_noteTotale>100)
            {
                _noteTotale = 100;
            }

        }

        public void Afficher()
        {
            Console.WriteLine($"{_prenom} {_nom} ({_matricule}), NOte = {_noteTotale}");
        }
        private readonly int _matricule;
        private double _noteTotale;

    }
}
