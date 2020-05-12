using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteCheque : Compte
    {
        public CompteCheque(string type, string prenom, string nom, int numero) : base(type, prenom, nom, numero)
        {
        }
        public CompteCheque(string type, string prenom, string nom) : base(type, prenom, nom)
        {
        }
    }
}
