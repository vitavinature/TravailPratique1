using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_2
{
    class TP : Evaluation
    {
        public TP(string titre, string ponderation, string date) : base(titre, ponderation, date)
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
                    caPlante = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            caPlante = true;
            while (caPlante)
            {
                try
                {
                    Console.WriteLine("Date de remise (A/M/J h:m): ");
                    _dateRemise = DateTime.Parse(Console.ReadLine());

                    TimeSpan retard = _dateRemise - _date;
                    int jourRetard = (int)Math.Ceiling(retard.TotalDays);
                    if (jourRetard > 0)
                    {
                        _note -= jourRetard * 10;
                        if (_note < 0)
                        {
                            _note = 0;
                        }
                        Console.WriteLine($"Note avec pénalité de {jourRetard} jours de retard: {_note}");

                    }
                    caPlante = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
                _note = _note * _ponderation / 100;
                return _note;
            }

        private double _note;
        private DateTime _dateRemise;
    }
}
