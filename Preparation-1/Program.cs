using System;
using System.IO;
using System.Collections.Generic; // Pour la classe List


namespace TravailPratique1

{
    class Program
    {
        static void Main(string[] args)
        {
            # region List <Medecin> Medecins = new List<Medecin>();

            List<Medecin> Medecins = new List<Medecin>();
            try
            {
                string fichierMedecins = "medecins.txt";
                // Ouverture du canalLectureMed pour l'accès au fichier "medecins.txt"
                using (StreamReader canalLectureMed = new StreamReader(fichierMedecins))
                {
                    // Lit la première ligne qui identifie le médecin
                    string ligneMed = canalLectureMed.ReadLine();
                    Console.WriteLine(ligneMed);

                    while (ligneMed != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        List<string> donnees = new List<string>(ligneMed.Split(';'));
                        foreach (string item in donnees)
                        {
                            Console.WriteLine(item);
                        }

                        if (donnees.Count < 3)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }

                        if (donnees.Count == 3)
                        {
                            donnees.Add("3000-01-01");
                            Console.WriteLine(donnees[3]);
                        }

                        if (donnees.Count > 4)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                        }

                        if (donnees[0].Length < 3 || donnees[0].Length > 3)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le numéro du médecin n'est pas conforme.");
                        }

                        if (donnees[1].Length < 2)
                        {
                            throw new Exception("Erreur, le prénom est invalide.");
                        }
                        if (donnees[2].Length < 2)
                        {
                            throw new Exception("Erreur, le nom est invalide.");
                        }

                        // Construction d'un objet Medecin
                        Medecins.Add(new Medecin(donnees[1], donnees[2], donnees[0], donnees[3]));

                        Console.WriteLine("Objet médecin créé");

                        ligneMed = canalLectureMed.ReadLine();
                        Console.WriteLine("");

                    }
                    //*******************************************************************************
                    foreach (Medecin item in Medecins)
                    {
                        item.Afficher();
                    }
                    //********************************************************************************
                    #endregion

                    #region List <Patient> Patients = new List<Patient>();

                    List<Patient> Patients = new List<Patient>();
                    try
                    {
                        string fichierPatients = "patients.txt";
                        // Ouverture du canalLecturePat pour l'accès au fichier "patients.txt"
                        using (StreamReader canalLecturePat = new StreamReader(fichierPatients))
                        {
                            // Lit la première ligne qui identifie le patient
                            string lignePat = canalLecturePat.ReadLine();
                            Console.WriteLine(lignePat);

                            while (lignePat != null)
                            {
                                // Extrait les valeurs individuelles de la ligne
                                List<string> donnees = new List<string>(lignePat.Split(';'));
                                foreach (string item in donnees)
                                {
                                    Console.WriteLine(item);
                                }

                                if (donnees.Count < 4)
                                {
                                    throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                                }

                                if (donnees.Count == 4)
                                {
                                    donnees.Add("3000-01-01");
                                    Console.WriteLine(donnees[4]);
                                }

                                if (donnees.Count > 5)
                                {
                                    throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                                }

                                if (donnees[0].Length < 4 || donnees[0].Length > 4)
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide; le numéro du patient n'est pas conforme.");
                                }

                                if (donnees[1].Length < 2)
                                {
                                    throw new Exception("Erreur, le prénom est invalide.");
                                }
                                if (donnees[2].Length < 2)
                                {
                                    throw new Exception("Erreur, le nom est invalide.");
                                }

                                // Construction d'un objet Patient
                                Patients.Add(new Patient(donnees[1], donnees[2], donnees[0], donnees[3], donnees[4]));

                                Console.WriteLine("Objet patient créé");

                                lignePat = canalLecturePat.ReadLine();
                                Console.WriteLine("");

                            }
                            //*******************************************************************************
                            foreach (Patient item in Patients)
                            {
                                item.Afficher();
                            }
                            //********************************************************************************
                            #endregion

                            // Affiche les détails de l'étudiant
                            Console.WriteLine("\n\n------------------------------");
                            //Medecins.Afficher();
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
            }
            catch (Exception e)
            {

            }

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
                    Menu1(); break;
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
