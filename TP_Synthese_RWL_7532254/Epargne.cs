﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Epargne : Compte
    {
        public Epargne(int numero, string prenom, string nom, double solde, string type) : base(numero, prenom, nom, solde, type)
        {

        }
    }
}
