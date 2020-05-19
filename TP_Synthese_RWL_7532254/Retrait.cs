using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Retrait : Transaction
    {
        public Retrait(int numeroCompte, double montant) : base (numeroCompte, montant)
        {

        }
    }
}
