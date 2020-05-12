using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteCredit : Compte
    {
        public CompteCredit(string type, string prenom, string nom, double montant, int numero) : base(type, prenom, nom, montant, numero)
        {
        }
        public CompteCredit(string type, string prenom, string nom, double montant) : base(type, prenom, nom, montant)
        {
        }
    }


}
