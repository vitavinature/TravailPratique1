﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Depot : Transaction
    {
        public Depot(int numeroCompte, double montant) : base (numeroCompte, montant)
        {

        }
    }
}
