﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Depot : Transaction
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        public Depot(int numeroCompte, double montant) : base (numeroCompte, montant)
        {

        }
    }
}
