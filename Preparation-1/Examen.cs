using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Examen : Evaluation
    {
        public Examen(string titre, string ponderation, string dateHeure) : base (titre, ponderation,dateHeure)
        {
            


        }

        public double DemanderNote()
        {
            while (true)
            {
                try
                {
                    Console.Write("Veuillez entrer la note obtenue: ");
                    double note = Convert.ToDouble(Console.ReadLine());
                    if (note < 0 || note > 100)
                    {
                        throw new Exception("La note doit être comprise entre 0 et 100");
                    }
                    _note = note;
                    return note;
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
