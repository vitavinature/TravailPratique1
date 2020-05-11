using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteCredit : Compte
    {
        public CompteCredit(int numero, string prenom, string nom, string type) : base(numero, prenom, nom, type)
        {

        }
    }
}
