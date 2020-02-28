using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_2
{
    class Examen : Evaluation
    {
        public Examen(string titre, string ponderation, string date) : base(titre, ponderation, date)
        {

        }

        public double DemanderNote()
        {
            bool caPlante = true;
            while (caPlante)
            {
                try
                {
                    Console.Write("Note : ");
                    _note = Convert.ToDouble(Console.ReadLine());
                    while (_note < 0 || _note > 100)
                    {
                        try
                        {
                            Console.WriteLine("Note invalide, doit être entre 0 et 100");
                            Console.Write("Note : ");
                            _note = Convert.ToDouble(Console.ReadLine());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    _note = _note * _ponderation / 100;
                    caPlante = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return _note;
        }

        private double _note;

    }
}
