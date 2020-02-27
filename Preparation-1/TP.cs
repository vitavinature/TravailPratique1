using System;
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
                    Console.Write("Note: ");
                    double noteTP = Convert.ToDouble(Console.ReadLine());
                    if (noteTP < 0 || noteTP > 100)
                    {
                        throw new Exception("La note doit être comprise entre 0 et 100");
                    }
                    Console.Write("Date de remise (A/M/J h:m): ");

                    string s2 = Console.ReadLine();
                    DateTime dateRemise = DateTime.Parse(s2);

                    // La méthode static Parse de la classe DateTime retourne un objet DateTime contenant la conversion de la valeur textuelle

                    DateTime date = _dateHeure;

                    // La classe TimeSpan contient une durée, obtenue en faisant la soustraction de 2 dates
                    TimeSpan retard = dateRemise - date;
                    int retardJour = (int)Math.Ceiling(retard.TotalDays);

                    if (retardJour > 0)
                    {
                        noteTP -= retardJour * 10;
                        if (noteTP < 0)
                        {
                            noteTP = 0;
                        }
                        Console.WriteLine($"Note avec pénalité de {retardJour} jours de retard: " + noteTP);
                    }
                    _note = noteTP * _ponderation / 100;
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
