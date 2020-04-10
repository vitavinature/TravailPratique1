using System;
using System.IO;

namespace TravailPratique1

{
    class Program
    {
        static void Main(string[] args)
        {
            imprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux                                         =");
            imprimeLigne(73, '=');
            Console.WriteLine("1) Ajouter");
            Console.WriteLine("2) Modifier");
            Console.WriteLine("3) Afficher");
            Console.WriteLine("Q) Quitter");




            imprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux - Affichage                            =");
            imprimeLigne(73, '=');
            Console.WriteLine("1) Afficher les statistiques");
            Console.WriteLine("2) Afficher la liste de médecins");
            Console.WriteLine("3) Afficher la liste de patients");
            Console.WriteLine("3) Afficher un patient");
            Console.WriteLine("Q) Retour au menu principal");

            Medecin medecin = null;
            try
            {
                // Ouverture du canalLecture pour l'accès au fichier "medecins.txt"
                using (StreamReader canalLecture = new StreamReader("medecins.txt"))
                {
                    // Lit la première ligne qui identifie l'étudiant
                    string ligne = canalLecture.ReadLine();

                    if (ligne != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        string[] valeurs2 = ligne.Split(';');
                        if (valeurs2.Length < 3)
                        {
                            throw new Exception("Erreur: Il manque une information.");
                        }
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
                        medecin = new Medecin(valeurs2[2], valeurs2[1], valeurs2[0]);
                    }
                    else
                    {
                        throw new Exception("Erreur le fichier n'est pas valide.");
                    }
//----------------------------------------------------------------------------------------------------------------------------------
                    // Lit la prochaine ligne
                    ligne = canalLecture.ReadLine();
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
                                            medecin.AjouterNote(note);
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
                                            medecin.AjouterNote(note);
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
                        ligne = canalLecture.ReadLine();
                    }
                    
                    // Affiche les détails de l'étudiant
                    Console.WriteLine("\n\n------------------------------");
                    medecin.Afficher();

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

        private static void imprimeLigne(int v1, char v2)
        {
            for (int i = 0; i < v1; i++)
            {
                Console.Write(v2);
            }
            Console.WriteLine();
            throw new NotImplementedException();
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
