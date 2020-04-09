using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
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
                    Console.Write("Note: ");
                    double note = Convert.ToDouble(Console.ReadLine());
                    if (note < 0 || note > 100)
                    {
                        throw new Exception("Note invalide, doit être entre 0 et 100");
                    }
                    _note = note * _ponderation/100;
                    Console.WriteLine($"Portion de la note finale: " + _note);
                    Console.WriteLine("");

                    return _note;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        private double _note;
    }
}
