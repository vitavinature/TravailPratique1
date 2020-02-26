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
                    Console.Write("Veuillez entrer la note obtenue: ");
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
                    //Console.WriteLine("Nombre de jours: " + retard.Days);
                    //Console.WriteLine("Nombre de jours total: " + retard.TotalDays);
                    int retardJour = (int)Math.Ceiling(retard.TotalDays);
                    //Console.WriteLine("retard jour: " + retardJour);

                    double penalite = 0;
                    if (retardJour > 0)
                    {
                        noteTP -= retardJour * 10;
                        if (noteTP < 0)
                        {
                            noteTP = 0;
                        }
                    }

                    Console.WriteLine($"Note avec pénalité de {retardJour} jours de retard: " + noteTP);
                    Console.WriteLine($"Portion de la finale: " + noteTP * _ponderation / 100);
                    return noteTP;
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

    }
}
