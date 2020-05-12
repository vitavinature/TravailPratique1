using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteEpargne : Compte
    {
        public CompteEpargne(string type, string prenom, string nom, double montant, int numero) : base(type, prenom, nom, montant, numero)
        {
        }
        public CompteEpargne(string type, string prenom, string nom, double montant) : base(type, prenom, nom, montant)
        {
        }
    }
}
