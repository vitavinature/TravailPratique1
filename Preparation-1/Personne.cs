using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Personne
    {
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;
        }
        public string Prenom { get { return _prenom; } }
        public string Nom { get { return _nom; } }

        protected readonly string _prenom;
        protected readonly string _nom;
    }
}





