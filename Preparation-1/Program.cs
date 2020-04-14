using System;
using System.IO;
using System.Collections.Generic; // Pour la classe List


namespace TravailPratique1

{
    class Program
    {
        static void Main(string[] args)
        {

            Medecin medecin = null;
            try
            {
                string fichierMedecins = "medecins.txt";
                // Ouverture du canalLecture pour l'accès au fichier "medecins.txt"
                using (StreamReader canalLecture = new StreamReader(fichierMedecins))
                {
                    // Lit la première ligne qui identifie le médecin
                    string ligne = canalLecture.ReadLine();

                    if (ligne != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        List<string> donnees = new List<string>( ligne.Split(';'));
                        if (donnees.Count < 4)
                        {
                            donnees[3] = "3000-01-01";
                        }
                        if (donnees.Count < 3)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }
                        if (donnees.Count > 4)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                        }

                        if (donnees[0].Length < 3 || donnees[0].Length > 3)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le nouméro du médecin n'est pas conforme.");
                        }

                        if (donnees[1].Length < 2)
                        {
                            throw new Exception("Erreur, le nom est invalide.");
                        }
                        if (donnees[2].Length < 2)
                        {
                            throw new Exception("Erreur, le prénom est invalide.");
                        }

                        // Construction d'un objet Medecin
                        medecin = new Medecin(donnees[0], donnees[1], donnees[2], donnees[3]);
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
                                    /* case "E":
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
 */
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
                    Menu1();

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

        private static void ImprimeLigne(int v1, char v2)
        {
            for (int i = 0; i < v1; i++)
            {
                Console.Write(v2);
            }
            Console.WriteLine();
            //throw new NotImplementedException();
        }
        private static void Menu1()
        {
            char choixChar = '0';
            ImprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux                                         =");
            ImprimeLigne(73, '=');
            Console.WriteLine("1) Ajouter");
            Console.WriteLine("2) Modifier");
            Console.WriteLine("3) Afficher");
            Console.WriteLine("Q) Quitter");
            Console.Write("> ");
            string choix = Console.ReadLine();
            if (choix.Length == 1 && ValiderChoix(choix, "123qQ") == true) 
            {
                choixChar = Convert.ToChar(choix);
            }
            else
            {
                Menu1();
            }
            switch (choixChar)
            {
                case '1':
                    MenuAjouter();
                    break;
                case '2':
                    MenuModifier();
                    break;
                case '3':
                    MenuAfficher();
                    break;
                case 'q':
                case 'Q':
                    //Quitter();*************************************** à faire
                    break;
                default:
                    Menu1();
                    break;
            }

        }

        private static bool ValiderChoix(string choix, string v)
        {
            if (v.Contains(choix))
            {
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        private static void MenuAjouter()
        {
            char choixChar = '0';

            ImprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux - Ajout                                 =");
            ImprimeLigne(73, '=');
            Console.WriteLine("1) Ajouter un médecin");
            Console.WriteLine("2) Ajouter un patient");
            Console.WriteLine("R) Retour au menu principal");
            Console.Write("> ");
            string choix = Console.ReadLine();
            if (choix.Length == 1 && ValiderChoix(choix, "12rR") == true)
            {
                choixChar = Convert.ToChar(choix);
            }
            else
            {
                MenuAjouter();
            }
            switch (choixChar)
            {
                case '1':
                    //AjouterMedecin(); ******************
                    break;
                case '2':
                    //AjouterPatient(); ******************
                    break;
                 case 'r':
                case 'R':
                    Menu1();                    break;
                default:
                    MenuAjouter();
                    break;
            }
        }
        private static void MenuModifier()
        {
            char choixChar = '0';

            ImprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux - Modification                          =");
            ImprimeLigne(73, '=');
            Console.WriteLine("1) Retrait d'un médecin");
            Console.WriteLine("2) Décès d'un patient");
            Console.WriteLine("R) Retour au menu principal");
            Console.Write("> ");
            string choix = Console.ReadLine();
            if (choix.Length == 1 && ValiderChoix(choix, "12rR") == true)
            {
                choixChar = Convert.ToChar(choix);
            }
            else
            {
                MenuModifier();
            }
            switch (choixChar)
            {
                case '1':
                    //RetraitMedecin(); ******************
                    break;
                case '2':
                    //DecesPatient(); ******************
                    break;
                case 'r':
                case 'R':
                    Menu1(); break;
                default:
                    MenuModifier();
                    break;
            }
        }
        private static void MenuAfficher()
        {
            char choixChar = '0';

            ImprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux - Affichage                            =");
            ImprimeLigne(73, '=');
            Console.WriteLine("1) Afficher les statistiques");
            Console.WriteLine("2) Afficher la liste de médecins");
            Console.WriteLine("3) Afficher un médecin");
            Console.WriteLine("4) Afficher la liste de patients");
            Console.WriteLine("5) Afficher un patient");
            Console.WriteLine("R) Retour au menu principal");
            Console.Write("> ");
            string choix = Console.ReadLine();
            if (choix.Length == 1 && ValiderChoix(choix, "12345rR") == true)
            {
                choixChar = Convert.ToChar(choix);
            }
            else
            {
                MenuAfficher();
            }
            switch (choixChar)
            {
                case '1':
                    //AfficherStatistiques(); ******************
                    break;
                case '2':
                    //AfficherListeMedecins(); ******************
                    break;
                case '3':
                    //AfficherUnMedecin(); ******************
                    break;
                case '4':
                    //AfficherListePatients(); ******************
                    break;
                case '5':
                    //AfficherUnPatient(); ******************
                    break;
                case 'r':
                case 'R':
                    Menu1(); break;
                default:
                    MenuAfficher();
                    break;
            }
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
