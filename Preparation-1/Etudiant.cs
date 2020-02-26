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

        private int _matricule;
        private double _noteTotale;
    }
}
