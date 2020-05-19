using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Retrait : Transaction
    {
        #region        public Retrait(int numeroCompte, double montant) : base (numeroCompte, montant)
        /// <summary>
        /// Constructeur qui hérite de la classe parent Transaction
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Retrait(int numeroCompte, double montant) : base (numeroCompte, montant)
        {
        }
        #endregion
    }
}
