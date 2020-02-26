﻿using System;
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
                using (StreamReader fichier = new StreamReader("etudiant.txt"))
                {
                    // Lit la première ligne qui indique l'étudiant
                    string ligne = fichier.ReadLine();

                    if (ligne != null)
                    {
                        // Extrait les valeurs individuelles de la ligne, et construit un objet Etudiant
                        string[] valeurs2 = ligne.Split(';');
                        etudiant = new Etudiant(valeurs2[2], valeurs2[1], valeurs2[0]);
                        if (Convert.ToInt32(valeurs2[0]) < 1000000 || Convert.ToInt32(valeurs2[0]) > 9999999)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le matricule est en erreur");
                        }
                        if (valeurs2[1].Length < 2)
                        {
                            throw new Exception("Erreur, le nom est invalide.");
                        }
                        if (valeurs2[2].Length < 2)
                        {
                            throw new Exception("Erreur, le prénom est invalide.");
                        }

                    }
                    else
                    {
                        throw new Exception("Erreur le fichier n'est pas valide.");
                    }

                    // Lit la prochaine ligne
                    ligne = fichier.ReadLine();

                    while (ligne != null)
                    {
                        string[] valeurs = ligne.Split(';');
                        char type = Convert.ToChar(valeurs[0]);

                        // Si le type est E pour examen
                        if (type == 'E')
                        {
                            if (!valeurs[1].StartsWith("Examen"))
                            {
                                throw new Exception("Erreur: Ligne d'examen non compatible");
                            }
                            if (Convert.ToInt32(valeurs[2])<0|| Convert.ToInt32(valeurs[2]) >100)
                            {
                                throw new Exception("Erreur: Pondération incorrecte");
                            }

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
                    }

                    Console.WriteLine("\n\n------------------------------");
                    // Affiche les détails de l'étudiant
                    etudiant.Afficher();
                }
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
