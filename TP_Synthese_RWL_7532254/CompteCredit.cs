using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class CompteCredit : Compte
    {
        public CompteCredit(string type, string prenom, string nom, double limiteCredit, int numero) : base(type, prenom, nom, numero)
        {
            _limiteCredit = limiteCredit;
        }
        public CompteCredit(string type, string prenom, string nom, double limiteCredit) : base(type, prenom, nom)
        {
            _limiteCredit = limiteCredit;
        }

        private readonly double _limiteCredit;

    }


}
