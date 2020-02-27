using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Preparation_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Etudiant etudiant = null;
            try
            {
                // Ouverture du fichier
                using (StreamReader fichier = new StreamReader("etudiant.txt"))
                {
                    // Lit la première ligne qui identifie l'étudiant
                    string ligne = fichier.ReadLine();

                    if (ligne != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        string[] valeurs2 = ligne.Split(';');

                        if (valeurs2[0].StartsWith("#") || valeurs2[0].StartsWith(" "))
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; la première ligne n'est pas conforme.");
                        }
                                                
                        if (valeurs2[1].Length < 2)
                        {
                            throw new Exception("Erreur, le nom est invalide.");
                        }
                        if (valeurs2[2].Length < 2)
                        {
                            throw new Exception("Erreur, le prénom est invalide.");
                        }

                        // Construction d'un objet étudiant
                        etudiant = new Etudiant(valeurs2[2], valeurs2[1], valeurs2[0]);
                    }
                    else
                    {
                        throw new Exception("Erreur le fichier n'est pas valide.");
                    }
//----------------------------------------------------------------------------------------------------------------------------------
                    // Lit la prochaine ligne
                    ligne = fichier.ReadLine();
                    Console.WriteLine("");

                    // Pour chaque lignwe lue (si elle contient quelque chose)
                    while (ligne != null)
                    {
                        string[] valeurs;
                        valeurs = ligne.Split(';'); // Séparation de la ligne en segments délimité par (;)

                        try
                        {
                            // Si la ligne ne commence pas par # ou si elle n'est pas vide
                            // Ce qui a pour effet d'ignorer les lignes de commentaires et les lignes vides
                            // Les lignes qui correspondent aux évaluations entrent dans le if
                            if (!(ligne.StartsWith("#") || ligne.Length == 0))
                            {
                                // Selon la valeur du (ou des) premier(s) caractère(s) 
                                switch (valeurs[0])
                                {
                                    case "E":
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
                                        break;

                                    case "T":
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
                                        break;

                                    default:
                                        throw new Exception("Type " + valeurs[0] + " non valide");
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Il manque des valeurs.");
                            Console.WriteLine("Appuyez sur une touche pour continuer");
                            Console.ReadKey(true);

                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                            Console.WriteLine("Appuyez sur une touche pour continuer");
                            Console.ReadKey(true);
                            Console.WriteLine("");
                        }
                        ligne = fichier.ReadLine();
                    }
                    
                    // Affiche les détails de l'étudiant
                    Console.WriteLine("\n\n------------------------------");
                    etudiant.Afficher();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // Le programme est terminé rendu ici.*************************
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
