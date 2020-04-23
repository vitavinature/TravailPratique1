// Programme permettant de gérer des dossiers médicaux. Il permet de créer, afficher et modifier une liste de médecins et une liste de patients. 
// Travail Pratique No.1 du Cours Programmation orientée objet 420-2C6-JR
// Enseignant Jooël Beaudet
// Par Richard Wayne Lussier 7532254 gr. 102
// Date 20 Avril 2020
// Fichiers liés au programme: Programme.cs, Personne.cs, Patient.cs et Medecin.cs

using System;
using System.IO; // pour les fichiers (input output stream)
using System.Collections.Generic; // Pour la classe List
using Preparation_1;

namespace TravailPratique1
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionnaireDeMedecins gestionMedecin = new GestionnaireDeMedecins();
            GestionnaireDePatients gestionPatient = new GestionnaireDePatients();


            Menu1(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
            
            // Le programme est terminé rendu ici.
        }


        #region         public static int DemanderCode(string texte, int minimum, int maximum)
        /// <summary>
        /// Demande à l'utilisateur d'entrer une valeur
        /// Valide la valeur entrée pour qu'elle soit du bon type (un entier)
        /// Valide la valeur entrée pour qu'elle soit entre un minimum et un maximum
        /// Redemande à l'utilisateur d'entrer une valeur tant qu'elle n'est pas valide
        /// </summary>
        /// <param name="texte">contient la description de valeur demandé à l'utilisateur</param>
        /// <param name="minimum">valeur minimale que la valeur entrée pa</param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static int DemanderCode(string texte, int minimum, int maximum)
        {
            int entier = 0;
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    Console.Write(texte);
                    entier = Convert.ToInt32(Console.ReadLine());
                    while (entier < minimum || entier > maximum)
                    {
                        try
                        {
                            Console.WriteLine($"Valeur invalide, doit être entre {minimum} et {maximum}");
                            Console.Write(texte);
                            entier = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    erreur = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return entier;
        }
        #endregion

        #region         public static string DemanderTexte(string texte)
        /// <summary>
        /// Demande à l'utilisateur d'entrer un nom ou un prénom et assure que l'entrée est valide.
        /// Le programme continu à demander à l'utilisateur tant que l'entrée n'est pas valide.
        /// </summary>
        /// <param name="texte">Ce qui est demandé à l'utilisateur.</param>
        /// <returns></returns>
        public static string DemanderTexte(string texte)
        {
            string texteValide = "";
            bool valide = false;
            while (!valide)
            {
                Console.Write($"{texte}: ");
                texteValide = Console.ReadLine();
                if (texteValide.Length >= 2)
                {
                    for (int i = 0; i < texteValide.Length; i++)
                    {
                        valide = ValiderChoix(Convert.ToString(texteValide[i]), "abcçdeéèêëfghiîjklmnoôpqrstuvwxyzABCDEÉÈÊËFGHIÎJKLMNOPQRSTUVWXYZ' ''-'");
                        if (valide == false)
                        {
                            Console.WriteLine($"Le {texte} n'est pas valide");
                            Pause();
                            DemanderTexte(texte);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Le {texte} n'est pas valide");
                    Pause();
                    DemanderTexte(texte);
                }
            }
            return texteValide;
        }
        #endregion

        #region         static void ImprimeLigne(int v1, char v2)

        static void ImprimeLigne(int v1, char v2)
        {
            for (int i = 0; i < v1; i++)
            {
                Console.Write(v2);
            }
            Console.WriteLine();
        }
        #endregion

        #region         static void Menu1(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le menu d'entré lorsque l'application est lancée.
        /// Le menu offre les choix d'ajouter, modifier, afficher médecins et patients ou de quitter le programme.
        /// Le choix de l'utilisateur est validé.
        /// Un switch oriente le programme selon le choix de l'utilisateur.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets patient</param>
        /// <param name="nombreMedecinActif">Nombre des médecin(s) actif(s)</param>
        static void Menu1(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        {
            while (true)
            {
                Console.Clear();
                char choixChar = '0';
                ImprimeLigne(73, '=');
                Console.WriteLine("= Gestion des dossiers médicaux                                         =");
                ImprimeLigne(73, '=');
                Console.WriteLine();
                Console.WriteLine(" 1) Ajouter");
                Console.WriteLine(" 2) Modifier");
                Console.WriteLine(" 3) Afficher");
                Console.WriteLine(" Q) Quitter");
                Console.WriteLine();
                Console.Write("> ");

                string choix = Console.ReadLine();
                if (choix.Length == 1 && ValiderChoix(choix, "123qQ") == true)
                {
                    choixChar = Convert.ToChar(choix);
                }
                else
                {
                    Menu1(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                }
                switch (choixChar)
                {
                    case '1':
                        MenuAjouter(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                        break;
                    case '2':
                        MenuModifier(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                        break;
                    case '3':
                        MenuAfficher(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                        break;
                    case 'q':
                    case 'Q':
                        Quitter(ref gestionMedecin, ref gestionPatient);
                        Console.WriteLine("Sauvegarde des données et fin du programme");
                        Pause();
                        return;
                }
            }
        }
        #endregion

        #region         static void MenuAfficher(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le Menu Afficher où l'information concernant médecins et patients peut être vu sous différentes façon.
        /// Le choix de l'utilisateur est validé
        /// Un switch fait la sélection de la méthode à apeller selon le choix de l'utilisateur
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecins actifs</param>
        static void MenuAfficher(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    char choixChar = '0';
                    ImprimeLigne(73, '=');
                    Console.WriteLine("= Gestion des dossiers médicaux - Affichage                            =");
                    ImprimeLigne(73, '=');
                    Console.WriteLine();
                    Console.WriteLine(" 1) Afficher les statistiques");
                    Console.WriteLine(" 2) Afficher la liste de médecins");
                    Console.WriteLine(" 3) Afficher un médecin");
                    Console.WriteLine(" 4) Afficher la liste de patients");
                    Console.WriteLine(" 5) Afficher un patient");
                    Console.WriteLine(" R) Retour au menu principal");
                    Console.WriteLine();
                    Console.Write("> ");
                    string choix = Console.ReadLine();
                    if (choix.Length == 1 && ValiderChoix(choix, "12345rR") == true)
                    {
                        choixChar = Convert.ToChar(choix);
                    }
                    else
                    {
                        MenuAfficher(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                    }
                    switch (choixChar)
                    {
                        case '1':
                            AfficherLesStatistiques(ref gestionMedecin, ref gestionPatient);
                            break;
                        case '2':
                            AfficherListeMedecins(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                        case '3':
                            AfficherUnMedecin(ref gestionMedecin, ref gestionPatient);
                            break;
                        case '4':
                            AfficherListePatients(ref gestionMedecin, ref gestionPatient);
                            break;
                        case '5':
                            AfficherUnPatient(ref gestionMedecin, ref gestionPatient);
                            break;
                        case 'r':
                        case 'R':
                            return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region         static void MenuAjouter(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        /// <summary>
        /// Permet de choisir l’élément à ajouter, un médecin ou un patient.
        /// Pour pouvoir ajouter un patient, au moins un médecin actif doit être défini.
        /// Si aucun médecin actif n’est défini, un message informe l’utilisateur et l’ajout du patient est impossible. 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        static void MenuAjouter(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        {
            while (true)
            {
                try
                {
                    Console.Clear();

                    char choixChar = '0';

                    ImprimeLigne(73, '=');
                    Console.WriteLine("= Gestion des dossiers médicaux - Ajout                                 =");
                    ImprimeLigne(73, '=');
                    Console.WriteLine();
                    Console.WriteLine(" 1) Ajouter un médecin");
                    Console.WriteLine(" 2) Ajouter un patient");
                    Console.WriteLine(" R) Retour au menu principal");
                    Console.WriteLine();
                    Console.Write("> ");
                    string choix = Console.ReadLine();
                    if (choix.Length == 1 && ValiderChoix(choix, "12rR") == true)
                    {
                        choixChar = Convert.ToChar(choix);
                    }
                    else
                    {
                        MenuAjouter(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                    }
                    switch (choixChar)
                    {
                        case '1':
                            AjouterMedecin(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                        case '2':
                            if (nombreMedecinActif == 0)
                            {
                                Console.WriteLine("Il n'y a aucun médecin actif présentement. Veuillez ressayer plus tard.");
                                Pause();
                                AjouterMedecin(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            }
                            AjouterPatient(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                        case 'r':
                        case 'R':
                            Menu1(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region     static void MenuModifier(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le Menu Modifier où un médecin peut être mis à la retraite ou le décès d'un patient peut être enregistré
        /// Le choix est validé
        /// Si le nombre de médecins actifs est inférieur à 2 il est impossible de mettre un médecin à la retraite
        /// Un switch fait la sélection de la méthode à apeller selon qu'un médecin est mis à la retraite ou un patient est décédé
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecins actifs</param>
        static void MenuModifier(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient, ref int nombreMedecinActif)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    char choixChar = '0';
                    ImprimeLigne(73, '=');
                    Console.WriteLine("= Gestion des dossiers médicaux - Modification                          =");
                    ImprimeLigne(73, '=');
                    Console.WriteLine();
                    Console.WriteLine(" 1) Indiquer un départ à la retraite d'un médecin");
                    Console.WriteLine(" 2) Indiquer un décès d'un patient");
                    Console.WriteLine(" R) Retour au menu principal");
                    Console.WriteLine();
                    Console.Write("> ");

                    string choix = Console.ReadLine();
                    if (choix.Length == 1 && ValiderChoix(choix, "12rR") == true)
                    {
                        choixChar = Convert.ToChar(choix);
                    }
                    else
                    {
                        MenuModifier(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                    }
                    switch (choixChar)
                    {
                        case '1':
                            if (nombreMedecinActif <= 1)
                            {
                                Console.WriteLine("Il est présentement impossible de mettre un médecin à la retraite,");
                                Console.WriteLine($"Puisqu'il n'y a que {nombreMedecinActif} médecin actif.");
                                Console.WriteLine("Veuillez tenter à nouveau plus tard.");
                                Console.WriteLine();
                                Pause();
                                return;
                            }
                            RetraitMedecin(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                        case '2':
                            DecesPatient(ref gestionMedecin, ref gestionPatient, ref nombreMedecinActif);
                            break;
                        case 'r':
                        case 'R':
                            return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region         public static void Pause()
        /// <summary>
        /// Pour pauser le programme jusqu'à ce que l'utilisateur appuie sur une touche
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("Appuyez sur une touche pour continuer");
            Console.ReadKey(true);
        }
        #endregion

        #region         static void Quitter(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient)
        /// <summary>
        /// Ouvre le canal d'écriture de fichierMedecins
        /// Écrit le contenu des informations des médecins
        /// Ouvre le canal d'écriture de fichierPatients
        /// Écrit le contenu des informations des patients
        /// </summary>
        /// <param name="Medecins">nom de la List<Medecin> passé en paramètre référence</param>
        /// <param name="Patients">nom de la List<Patient> passé en paramètre référence</param>
        static void Quitter(ref List<Medecin> gestionMedecin, ref List<Patient> gestionPatient)
        {
            try
            {
                string fichierMedecins = "medecins.txt";
                // Ouverture du canalEcritureMed pour l'écriture dans le fichier "medecins.txt"
                using (StreamWriter canalEcritureMed = new StreamWriter(fichierMedecins))
                {
                    foreach (Medecin item in gestionMedecin)
                    {
                        if (item.DateRetraite != item.NonRetraite)
                        {
                            canalEcritureMed.WriteLine($"{item.Matricule};{item.Prenom};{item.Nom};{item.DateRetraite}");
                        }
                        else
                        {
                            canalEcritureMed.WriteLine($"{item.Matricule};{item.Prenom};{item.Nom}");
                        }
                    }
                }
                string fichierPatients = "patients.txt";
                // Ouverture du canalEcriturePat pour l'écriture dans le fichier "patients.txt"
                using (StreamWriter canalEcriturePat = new StreamWriter(fichierPatients))
                {
                    foreach (Patient item in gestionPatient)
                    {
                        if (item.DateDeces != item.NonDecede)
                        {
                            canalEcriturePat.WriteLine($"{item.AssMaladie};{item.Prenom};{item.Nom};{0};{item.DateDeces}");
                        }
                        else
                        {
                            canalEcriturePat.WriteLine($"{item.AssMaladie};{item.Prenom};{item.Nom};{item.MatriculeMedecin}");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region         static bool ValiderChoix(string choix, string choixPossibles)
        /// <summary>
        /// Vérifie que le choix de l'utilisateur est valide, selon le menu présenté
        /// </summary>
        /// <param name="choix">valeur entrée par l'utilisateur</param>
        /// <param name="choixPossibles">mot contenant tout les choix possibles selon le menu présenté</param>
        /// <returns></returns>
        static bool ValiderChoix(string choix, string choixPossibles)
        {
            if (choixPossibles.Contains(choix))
            {
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }
        #endregion
    }
}