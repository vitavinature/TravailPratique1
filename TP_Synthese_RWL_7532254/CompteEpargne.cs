using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteEpargne : Compte
    {
        public CompteEpargne(string type, string prenom, string nom, int numero) : base(type, prenom, nom, numero)
        {
        }
        public CompteEpargne(string type, string prenom, string nom) : base(type, prenom, nom)
        {
        }
    }
}
