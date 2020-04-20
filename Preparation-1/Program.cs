using System;
using System.IO;
using System.Collections.Generic; // Pour la classe List
using System.Text;


namespace TravailPratique1
{
    class Program
    {
        static void Main(string[] args)
        {
            int nombreMedecinActif = 0;

            string dateDefaut = "3000-01-01";
            List<Medecin> Medecins = new List<Medecin>();
            List<Patient> Patients = new List<Patient>();

            # region Lecture du fichier des médecins

            try
            {
                string fichierMedecins = "medecins.txt";
                // Ouverture du canalLectureMed pour l'accès au fichier "medecins.txt"
                using (StreamReader canalLectureMed = new StreamReader(fichierMedecins))
                {
                    // Lit la première ligne qui identifie le médecin
                    string ligneMed = canalLectureMed.ReadLine();

                    while (ligneMed != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        List<string> donnees = new List<string>(ligneMed.Split(';'));
                        if (donnees.Count < 3)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }
                        if (donnees.Count == 3)
                        {
                            nombreMedecinActif += 1;
                            donnees.Add(dateDefaut);
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

                        // Construction d'un objet Medecin dans la liste d'objets LIST<Medecin>
                        Medecins.Add(new Medecin(donnees[1], donnees[2], donnees[0], donnees[3], ref nombreMedecinActif));

                        ligneMed = canalLectureMed.ReadLine(); // Lecture d'une autre ligne dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Lecture du fichier des patients

            try
            {
                string fichierPatients = "patients.txt";
                // Ouverture du canalLecturePat pour l'accès au fichier "patients.txt"
                using (StreamReader canalLecturePat = new StreamReader(fichierPatients))
                {
                    // Lit la première ligne qui identifie le patient
                    string lignePat = canalLecturePat.ReadLine();

                    while (lignePat != null)
                    {
                        // Extrait les valeurs individuelles de la ligne
                        List<string> donnees = new List<string>(lignePat.Split(';'));

                        if (donnees.Count < 4)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }
                        if (donnees.Count == 4)
                        {
                            int matriculeMedecin = Convert.ToInt32(donnees[3]);
                            foreach (Medecin item in Medecins)
                            {
                                if (matriculeMedecin == item._matricule)
                                {
                                    item.AjouterPatient(Convert.ToInt32(donnees[0]));
                                }
                            }
                            donnees.Add(dateDefaut);
                        }
                        if (donnees.Count > 5)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                        }
                        if (donnees[0].Length < 4 || donnees[0].Length > 4)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le numéro d'assurance maladie du patient n'est pas conforme.");
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

                        lignePat = canalLecturePat.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);

            // Le programme est terminé rendu ici.*************************
            Pause();
        }
        #region         static void AfficherLesStatistiques(ref List<Medecin> Medecins, ref List<Patient> Patients)

        static void AfficherLesStatistiques(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            int compteurMedecin = 0;
            int compteurMedecinRetraite = 0;
            int compteurPatient = 0;
            int compteurPatientDecede = 0;

            Console.WriteLine("Le système contient:");
            foreach (Medecin itemMedecin in Medecins)
            {
                compteurMedecin += 1;
                if (itemMedecin._dateRetraite != itemMedecin._nonRetraite)
                {
                    compteurMedecinRetraite += 1;
                }
            }
            Console.WriteLine($"{compteurMedecin} médecins, dont {compteurMedecinRetraite} à la retraite");

            foreach (Patient itemPatient in Patients)
            {
                compteurPatient += 1;
                if (itemPatient._dateDeces != itemPatient._nonDecede)
                {
                    compteurPatientDecede += 1;
                }
            }
            Console.WriteLine($"{compteurPatient} Patients, dont {compteurPatientDecede} décédés");

            Console.WriteLine();
            Pause();
        }
        #endregion

        #region         static void AfficherListePatients(ref List<Medecin> Medecins, ref List<Patient> Patients)

        static void AfficherListePatients(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            Console.WriteLine("Liste des patients");
            Console.WriteLine("------------------");
            foreach (Patient itemPatient in Patients)
            {
                itemPatient.Afficher();
                foreach (Medecin itemMedecin in Medecins)
                {
                    if (itemMedecin._matricule == itemPatient._matriculeMedecin)
                    {
                        Console.Write($"{itemMedecin._prenom} {itemMedecin._nom}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Pause();
        }
        #endregion

        #region         static void AfficherListeMedecins(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void AfficherListeMedecins(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.WriteLine("Liste des médecins");
            Console.WriteLine("------------------");


            foreach (Medecin item in Medecins)
            {
                item.Afficher();
            }
            Console.WriteLine();
            Console.WriteLine($"Nombre de médecins actifs: {nombreMedecinActif}");
            Console.WriteLine();
            Pause();
        }
        #endregion

        #region         static void AfficherUnMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients)

        static void AfficherUnMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            Console.WriteLine("Code d'identification: ");
            string code = Console.ReadLine();
            int codeDIdentification = Convert.ToInt32(code);

            foreach (Medecin itemMedecin in Medecins)
            {
                if (itemMedecin._matricule == codeDIdentification)
                {
                    Console.WriteLine("Medecin");
                    Console.WriteLine("-------");
                    Console.WriteLine($"Code d'identification: {itemMedecin._matricule}");
                    Console.WriteLine($"Nom: {itemMedecin._prenom} {itemMedecin._nom}");
                    if (itemMedecin._ListePatient.Count > 0)
                    {
                        Console.WriteLine("Patients:");
                        foreach (Patient itemPatient in Patients)
                        {
                            if (itemPatient._matriculeMedecin == itemMedecin._matricule)
                            {
                                Console.WriteLine($"{itemPatient._assMaladie} {itemPatient._prenom} {itemPatient._nom}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucun patient");
                    }
                }
            }
            Pause();
        }
        #endregion

        #region         static void AfficherUnPatient(ref List<Medecin> Medecins, ref List<Patient> Patients)

        static void AfficherUnPatient(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            Console.WriteLine("Numéro d'assurance maladie: ");
            string numAssMal = Console.ReadLine();
            int numeroDAssuranceMaladie = Convert.ToInt32(numAssMal);

            foreach (Patient itemPatient in Patients)
            {
                if (itemPatient._assMaladie == numeroDAssuranceMaladie)
                {
                    Console.WriteLine("Patient");
                    Console.WriteLine("-------");
                    Console.WriteLine($"Numéro d'assurance maladie: {itemPatient._assMaladie}");
                    Console.WriteLine($"Nom: {itemPatient._prenom} {itemPatient._nom}");
                    if (itemPatient._matriculeMedecin != 0)
                    {
                        Console.Write("Medecin:");
                        foreach (Medecin itemMedecin in Medecins)
                        {
                            if (itemPatient._matriculeMedecin == itemMedecin._matricule)
                            {
                                Console.WriteLine($"{itemMedecin._matricule} {itemMedecin._prenom} {itemMedecin._nom}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Patient décédé");
                    }
                }
            }
            Pause();
        }
        #endregion

        #region         static void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.WriteLine("Ajout d'un médecin");
            Console.WriteLine("------------------");
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            List<string> donnees = new List<string>();
            donnees.Add(prenom);
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            donnees.Add(nom);
            Console.Write("Code d'identification: ");
            string code = Console.ReadLine();
            int idCode = Convert.ToInt32(code);
            if (idCode >= 100 && idCode <= 999)
            {
                foreach (Medecin item in Medecins)
                {
                    if (idCode == item._matricule)
                    {
                        Console.WriteLine("Impossible d'ajouter le médecin, Le  code d'identification existe déjà.");
                        Pause();

                        Console.Clear();
                        MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                    }
                }
                donnees.Add(code);
                donnees.Add("3000-01-01");
                // Construction d'un objet Medecin dans la liste d'objets List<Medecin>
                Medecins.Add(new Medecin(donnees[0], donnees[1], donnees[2], donnees[3], ref nombreMedecinActif));

                Console.WriteLine("Médecin ajouté");
                Pause();

                foreach (Medecin item in Medecins)
                {
                    item.Afficher();
                }
                Console.WriteLine();
                Pause();

                MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
        }
        #endregion

        #region static void AjouterPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void AjouterPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.WriteLine("Ajout d'un patient");
            Console.WriteLine("------------------");
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            List<string> donnees = new List<string>();
            donnees.Add(prenom);
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            donnees.Add(nom);
            Console.Write("Numéro d'assurance maladie: ");
            string code = Console.ReadLine();
            int idCode = Convert.ToInt32(code);
            if (idCode >= 1000 && idCode <= 9999)
            {
                foreach (Patient item in Patients)
                {
                    if (idCode == item._assMaladie)
                    {
                        Console.WriteLine("Impossible d'ajouter le patient, Le numéro d'assurance maladie existe déjà.");
                        Pause();

                        Console.Clear();
                        MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                    }
                }
                donnees.Add(code);
                int minimumPatient = 1000;
                int medecinAvecMinPatient = 0;
                foreach (Medecin item in Medecins)
                {
                    if (item._dateRetraite == item._nonRetraite)
                    {
                        if (item._ListePatient.Count < minimumPatient)
                        {
                            minimumPatient = item._ListePatient.Count;
                            medecinAvecMinPatient = item._matricule;
                        }
                    }
                }
                foreach (Medecin item in Medecins)
                {
                    if (item._matricule == medecinAvecMinPatient)
                    {
                        item.AjouterPatient(idCode);
                    }
                }

                donnees.Add(Convert.ToString(medecinAvecMinPatient));

                donnees.Add("3000-01-01");
                // Construction d'un objet Patient dans la liste d'objets List<Patient>
                Patients.Add(new Patient(donnees[0], donnees[1], donnees[2], donnees[3], donnees[4]));

                Console.WriteLine("Patient ajouté");
                Pause();

                foreach (Patient item in Patients)
                {
                    item.Afficher();
                }
                Console.WriteLine();
                Pause();

                Console.Clear();
                MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
        }
        #endregion

        #region         static void DecesPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void DecesPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Write("Numero d'assurance maladie: ");
            int numeroAssMaladie = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            foreach (Patient item in Patients)
            {
                if (item._assMaladie == numeroAssMaladie)
                {
                    Console.WriteLine("Indiquer décès");
                    Console.WriteLine("--------------");
                    Console.Write("Date du décès (AAAA-MM-JJ): ");
                    DateTime dateDeces = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine($"Date du décès: {dateDeces}");
                    Console.WriteLine();

                    Console.WriteLine($"Nombre de médecin(s) actif(s): {nombreMedecinActif}");
                    Console.WriteLine();

                    Pause();
                }
            }
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
            //throw new NotImplementedException();
        }
        #endregion

        #region         static void Menu1(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void Menu1(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Clear();
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
                Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            switch (choixChar)
            {
                case '1':
                    MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case '2':
                    MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case '3':
                    MenuAfficher(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case 'q':
                case 'Q':
                    Quitter(ref Medecins, ref Patients);
                    break;
                default:
                    Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
            }
        }
        #endregion

        #region         static void MenuAfficher(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void MenuAfficher(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
                Console.Clear();
                MenuAfficher(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            switch (choixChar)
            {
                case '1':
                    AfficherLesStatistiques(ref Medecins, ref Patients);
                    break;
                case '2':
                    AfficherListeMedecins(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case '3':
                    AfficherUnMedecin(ref Medecins, ref Patients);
                    break;
                case '4':
                    AfficherListePatients(ref Medecins, ref Patients);
                    break;
                case '5':
                    AfficherUnPatient(ref Medecins, ref Patients);
                    break;
                case 'r':
                case 'R':
                    Console.Clear();
                    Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                default:
                    Console.Clear();
                    MenuAfficher(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
            }
            MenuAfficher(ref Medecins, ref Patients, ref nombreMedecinActif);

        }
        #endregion

        #region         static void MenuAjouter(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void MenuAjouter(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Clear();

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
                MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            switch (choixChar)
            {
                case '1':
                    AjouterMedecin(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case '2':
                    if (nombreMedecinActif == 0)
                    {
                        Console.WriteLine("Il n'y a aucun médecin actif présentement. Veuillez ressayer plus tard.");
                        Pause();
                        AjouterMedecin(ref Medecins, ref Patients, ref nombreMedecinActif);
                    }
                    AjouterPatient(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case 'r':
                case 'R':
                    Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                default:
                    MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
            }
        }
        #endregion

        #region     static void MenuModifier(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void MenuModifier(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Clear();

            char choixChar = '0';

            ImprimeLigne(73, '=');
            Console.WriteLine("= Gestion des dossiers médicaux - Modification                          =");
            ImprimeLigne(73, '=');
            Console.WriteLine("1) Indiquer un départ à la retraite d'un médecin");
            Console.WriteLine("2) Indiquer un décès d'un patient");
            Console.WriteLine("R) Retour au menu principal");
            Console.Write("> ");

            string choix = Console.ReadLine();
            if (choix.Length == 1 && ValiderChoix(choix, "12rR") == true)
            {
                choixChar = Convert.ToChar(choix);
            }
            else
            {
                MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            switch (choixChar)
            {
                case '1':
                    if (nombreMedecinActif <= 1)
                    {
                        Console.WriteLine("Il est présentement impossible de mettre un médecin à la retraite,");
                        Console.WriteLine($"Puisqu'il y a {nombreMedecinActif} médecin actif.");
                        Console.WriteLine("Veuillez tenter à nouveau plus tard.");
                        Console.WriteLine();
                        Pause();
                        MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
                    }
                    RetraitMedecin(ref Medecins, ref Patients, ref nombreMedecinActif);
                    MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);

                    break;
                case '2':
                    DecesPatient(ref Medecins, ref Patients, ref nombreMedecinActif);
                    MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                case 'r':
                case 'R':
                    Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
                default:
                    MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
                    break;
            }
        }
        #endregion

        #region         static void Pause()

        static void Pause()
        {
            Console.WriteLine("Appuyez sur une touche pour continuer");
            Console.ReadKey(true);
        }
        #endregion

        #region         static void Quitter(ref List<Medecin> Medecins, ref List<Patient> Patients)

        static void Quitter(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            try
            {
                string fichierMedecins = "medecins.txt";
                // Ouverture du canalEcritureMed pour l'écriture dans le fichier "medecins.txt"
                using (StreamWriter canalEcritureMed = new StreamWriter(fichierMedecins))
                {
                    foreach (Medecin item in Medecins)
                    {
                        if (item._dateRetraite != item._nonRetraite)
                        {
                            canalEcritureMed.WriteLine($"{item._matricule};{item._prenom};{item._nom};{item._dateRetraite}");
                        }
                        else
                        {
                            canalEcritureMed.WriteLine($"{item._matricule};{item._prenom};{item._nom}");
                        }
                    }
                }
                string fichierPatients = "patients.txt";
                // Ouverture du canalEcriturePat pour l'écriture dans le fichier "patients.txt"
                using (StreamWriter canalEcriturePat = new StreamWriter(fichierPatients))
                {
                    foreach (Patient item in Patients)
                    {
                        if (item._dateDeces != item._nonDecede)
                        {
                            canalEcriturePat.WriteLine($"{item._assMaladie};{item._prenom};{item._nom};{item._dateDeces}");
                        }
                        else
                        {
                            canalEcriturePat.WriteLine($"{item._assMaladie};{item._prenom};{item._nom}");
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

        #region         static void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Write("Code d'identification: ");
            int codeIdentification = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            foreach (Medecin item in Medecins)
            {
                if (item._matricule == codeIdentification)
                {
                    Console.WriteLine("Indiquer retraite");
                    Console.WriteLine("-----------------");
                    Console.Write("Date de la retraite (AAAA-MM-JJ): ");
                    DateTime dateRetraite = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine($"Date de la retraite: {dateRetraite}");
                    Console.WriteLine();

                    nombreMedecinActif -= 1;

                    Console.WriteLine($"Nombre de médecin(s) actif(s): {nombreMedecinActif}");
                    Console.WriteLine();

                    Pause();
                }
            }
        }
        #endregion

        #region         static bool ValiderChoix(string choix, string v)

        static bool ValiderChoix(string choix, string v)
        {
            if (v.Contains(choix))
            {
                return true;
            }
            return false;
            throw new NotImplementedException();
        }
        #endregion

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
