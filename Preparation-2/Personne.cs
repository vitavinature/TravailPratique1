using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_2
{
    class Personne
    {
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;

        }

        protected readonly string _prenom;
     protected readonly string _nom;

    }
}
