using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Personne
    {
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;
        }
        protected string _prenom;
        protected string _nom;
    }
}
