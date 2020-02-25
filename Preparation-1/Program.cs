using System;

namespace Preparation_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Etudiant etudiant = null;

            // Lit la première ligne qui indique l'étudiant
            string ligne = fichier.ReadLine();
            if (ligne != null)
            {
                // Extrait les valeurs individuelles de la ligne, et construit un objet Etudiant
                string[] valeurs = ligne.Split(';');
                etudiant = new Etudiant(valeurs[2], valeurs[1], valeurs[0]);

                // Lit la prochaine ligne
                ligne = fichier.ReadLine();
            }


            string[] valeurs = ligne.Split(';');
            char type;

            // Si le type est E pour examen
            if (type == 'E')
            {
                // Construit un nouvel examen avec les valeurs lues
                Examen examen = new Examen(valeurs[1], valeurs[2], valeurs[3]);
                // Affiche les détails de l'examen
                examen.Afficher();
                // Demande à l'utilisateur la note de l'examen
                double note = examen.DemanderNote();
                // Ajoute la note à la note totale de l'étudiant
                etudiant.AjouterNote(note);
            }
            else // Sinon le type est T pour TP
            {
                // Construit un nouveau tp avec les valeurs lues
                TP tp = new TP(valeurs[1], valeurs[2], valeurs[3]);
                // Affiche les détails du tp
                tp.Afficher();
                // Demande à l'utilisateur la note du tp
                double note = tp.DemanderNote();
                // Ajoute la note à la note totale de l'étudiant
                etudiant.AjouterNote(note);
            }


            Console.WriteLine("\n\n------------------------------");
            // Affiche les détails de l'étudiant
            etudiant.Afficher();

            Console.WriteLine("Appuyez sur une touche pour continuer");
            Console.ReadKey(true);
        }
    }
}


/*
Exemples de manipulation de dates:

La méthode static Parse de la classe DateTime retourne un objet DateTime contenant la conversion de la valeur textuelle
            string s1 = "2020/2/25 10:45";
            DateTime date = DateTime.Parse(s1);

            string s2 = "2020/2/24 16:30";
            DateTime dateRemise = DateTime.Parse(s2);

La classe TimeSpan contient une durée, obtenue en faisant la soustraction de 2 dates
            TimeSpan retard = dateRemise - date;
            Console.WriteLine("Nombre de jours: " + retard.Days);
            Console.WriteLine("Nombre de jours total: " + retard.TotalDays);
*/
