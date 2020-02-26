﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class TP : Evaluation
    {
        public TP(string titre, string ponderation, string dateHeure) : base(titre, ponderation, dateHeure)
        {
                       
        }

        public double DemanderNote()
        {
            while (true)
            {
                try
                {
                    Console.Write("Veuillez entrer la note obtenue: ");
                    double noteExamen = Convert.ToDouble(Console.ReadLine());
                    if (noteExamen < 0 || noteExamen > 100)
                    {
                        throw new Exception("La note doit être comprise entre 0 et 100");
                    }
                    return noteExamen;
                }
                catch (Exception e)
                {
                    Console.Write("Mauvaise entrée: ");
                }

            }
        }


        public void Afficher()
        {
            Console.WriteLine($"{_titre}, {_ponderation}, {_dateHeure}");
        }

        private double _note;
        private string _titre;
        private double _ponderation;
        private DateTime _dateHeure;
    }
}
