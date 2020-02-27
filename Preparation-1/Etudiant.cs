using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Etudiant : Personne
    {
        public Etudiant(string prenom, string nom, string matricule) : base (prenom, nom)
        {
            _matricule = Convert.ToInt32(matricule);
        
        }

        public void AjouterNote(double note)
        {
            _noteTotale += note;
        }

        public void Afficher()
        {
            Console.WriteLine($"{_prenom} {_nom} ({_matricule}), Note =  {_noteTotale}");
        }

        private int _matricule;
        private double _noteTotale;
    }

}
