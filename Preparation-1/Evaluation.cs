﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Evaluation
    {
        public Evaluation(string titre, string ponderation, string dateHeure)
        {
            _titre = titre;
            _ponderation = Convert.ToInt32(ponderation);
            _dateHeure = DateTime.Parse(dateHeure);

        }

        private string _titre;
        private int _ponderation;
        private DateTime _dateHeure;
    }
}
