using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Examen : Evaluation
    {
        public Examen(string titre, string ponderation, string dateHeure)
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
        private string _titre;
        private double _ponderation;
        private DateTime _dateHeure;
    }
}
