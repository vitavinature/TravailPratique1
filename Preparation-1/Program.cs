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
                            throw new Exception("Erreur le fichier n'est pas valide, le prénom du médecin est invalide.");
                        }
                        if (donnees[2].Length < 2)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide, le nom du médecin est invalide.");
                        }

                        int matricule = Convert.ToInt32(donnees[0]);
                        if (matricule < 100 || matricule > 999)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le matricule du médecin est invalide.");
                        }
                        foreach (Medecin item in Medecins)
                        {
                            if (item._matricule == matricule)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro de médecin identiques.");
                            }
                        }

                        // Construction d'un objet Medecin dans la liste d'objets LIST<Medecin>
                        Medecins.Add(new Medecin(donnees[1], donnees[2], matricule, donnees[3], ref nombreMedecinActif));

                        ligneMed = canalLectureMed.ReadLine(); // Lecture d'une autre ligne dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Pause();
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
                                if (matriculeMedecin == item._matricule && item._matricule != 0)
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

                        int numeroAssMaladie = Convert.ToInt32(donnees[0]);

                        if (numeroAssMaladie < 1000 || numeroAssMaladie > 9999)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le numéro d'assurance maladie du patient est en erreur");
                        }
                        foreach (Patient item in Patients)
                        {
                            if (item._assMaladie == numeroAssMaladie)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro d'assurance maladie identiques.");
                            }
                        }

                        // Construction d'un objet Patient
                        Patients.Add(new Patient(donnees[1], donnees[2], numeroAssMaladie, donnees[3], donnees[4]));

                        lignePat = canalLecturePat.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Pause();
            }
            #endregion

            Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);

            // Le programme est terminé rendu ici.
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
            Console.WriteLine($"  {compteurMedecin} médecins, dont {compteurMedecinRetraite} à la retraite");

            foreach (Patient itemPatient in Patients)
            {
                compteurPatient += 1;
                if (itemPatient._dateDeces != itemPatient._nonDecede)
                {
                    compteurPatientDecede += 1;
                }
            }
            Console.WriteLine($"  {compteurPatient} Patients, dont {compteurPatientDecede} décédés");

            Pause();
        }
        #endregion

        #region         static void AfficherListePatients(ref List<Medecin> Medecins, ref List<Patient> Patients)
        /// <summary>
        /// Affiche la liste des patients (numéro d'assurance maladie, prénom et nom) et leur médecin respectifs (matricule, prénom, nom).
        /// Si le patient est décédé, la mention décédé remplace l'information du médecin.
        /// </summary>
        /// <param name="Medecins"></param>
        /// <param name="Patients"></param>
        static void AfficherListePatients(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            Console.WriteLine();
            Console.WriteLine("Liste des patients");
            Console.WriteLine("------------------");
            foreach (Patient itemPatient in Patients)
            {
                itemPatient.Afficher();
                if (itemPatient._dateDeces == itemPatient._nonDecede)
                {
                    foreach (Medecin itemMedecin in Medecins)
                    {
                        if (itemMedecin._matricule == itemPatient._matriculeMedecin)
                        {
                            Console.Write($"{itemMedecin._prenom} {itemMedecin._nom}");
                        }
                    }
                }

                Console.WriteLine();
            }
            Pause();
        }
        #endregion

        #region         static void AfficherListeMedecins(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void AfficherListeMedecins(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.WriteLine();
            Console.WriteLine("Liste des médecins");
            Console.WriteLine("------------------");

            foreach (Medecin item in Medecins)
            {
                item.Afficher();
            }
            Pause();
        }
        #endregion

        #region         static void AfficherUnMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients)
        /// <summary>
        /// Affiche les informations d'un médecin: prénom, nom, matricule et la liste des ses patients, s'il n'est pas retraité.
        /// S'il est retraité, la date du début de sa retraite est affichée.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        static void AfficherUnMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            string texte = "Code d'identification: ";
            int codeIdentification = DemanderCode(texte, 100, 999);

            foreach (Medecin itemMedecin in Medecins)
            {
                if (itemMedecin._matricule == codeIdentification)
                {
                    Console.WriteLine();
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
        /// <summary>
        /// Affiche les informations d'un patient: numéro d'assurance maladie, prénom, nom et matricule de son médecin.
        /// Si le patient est décédé, aucun matricule de médecin n'est affiché. La date sdu décès est affichée à la place.
        /// Le numéro d'assurance maladie entré par l'utilisateur est validé. S'il ne figure pas dans le registre des patients un message est affiché.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        static void AfficherUnPatient(ref List<Medecin> Medecins, ref List<Patient> Patients)
        {
            int patientMatch = 0;// Pour vérifier que le patient demandé fait bien parti du registre des patients.
            string texte = "Numéro d'assurance maladie: ";
            int numeroAssuranceMaladie = DemanderCode(texte, 1000, 9999);

            foreach (Patient itemPatient in Patients)
            {
                if (itemPatient._assMaladie == numeroAssuranceMaladie)
                {
                    patientMatch += 1;
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
                        Console.WriteLine($"Décédé le {Convert.ToDateTime(itemPatient._dateDeces)}");
                    }
                }
            }
            if (patientMatch == 0)
            {
                Console.WriteLine($"Il n'y a aucun patient avec le numéro d'assurance maladie {numeroAssuranceMaladie}.");
            }
            Pause();
        }
        #endregion

        #region         static void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Demande le (Prénom et le Nom qui sont des chaines de caractères), et le (Code d’identification, un entier) du médecin
        /// Un message d’erreur indique que l’ajout est impossible si un médecin portant le même code d’identification est déjà présent dans le système 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patients</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        static void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Ajout d'un médecin");
                Console.WriteLine("------------------");
                Console.Write("Prénom: ");
                string prenom = Console.ReadLine();
                List<string> donnees = new List<string>();
                donnees.Add(prenom);
                Console.Write("Nom: ");
                string nom = Console.ReadLine();
                donnees.Add(nom);

                string texte = "Code d'identification: ";
                int matricule = DemanderCode(texte, 100, 999);

                foreach (Medecin item in Medecins)
                {
                    if (matricule == item._matricule)
                    {
                        Console.WriteLine("Impossible d'ajouter le médecin, Le  code d'identification existe déjà.");
                        Pause();
                        MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                    }
                }
                donnees.Add(Convert.ToString(matricule));
                donnees.Add("3000-01-01");
                // Construction d'un objet Medecin dans la liste d'objets List<Medecin>
                Medecins.Add(new Medecin(donnees[0], donnees[1], matricule, donnees[3], ref nombreMedecinActif));

                Console.WriteLine("Médecin ajouté");
                //}
                Pause();
                MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region static void AjouterPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Pour pouvoir ajouter un patient, au moins un médecin actif doit être défini.
        /// Si aucun médecin actif n’est défini, un message informe l’utilisateur et l’ajout du patient est impossible.
        /// Lors de l’ajout d’un patient, le programme demande le Prénom, le Nom, et de Numéro d’assurance maladie du patient.
        /// Le prénom et le nom sont des chaines de caractères.Le numéro d’assurance maladie est un entier de 4 chiffres (entre 1000 et 9999).
        /// Le programme le redemande tant qu’il n’est pas valide.
        /// Lorsque l’information est entrée correctement, le patient est ajouté à la liste du système.
        /// Un message d’erreur indique que l’ajout est impossible si un patient portant le même numéro d’assurance maladie est déjà présent dans le système.
        /// Lors de l’ajout d’un patient, celui-ci est associé au médecin actif ayant le plus petit nombre de patients déjà associés.
        /// En cas d’égalité, l'un des deux médecins est choisi. 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        static void AjouterPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            int minimumPatient = 1000;// Nombre de patient minimum qu'un des médecins actifs a
            int medecinAvecMinPatient = 0;// Matricule du médecin actif ayant le moins de patient

            Console.WriteLine();
            Console.WriteLine("Ajout d'un patient");
            Console.WriteLine("------------------");
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            List<string> donnees = new List<string>();
            donnees.Add(prenom);
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            donnees.Add(nom);

            string texte = "Numéro d'assurance maladie: ";
            int numero = DemanderCode(texte, 1000, 9999);// Numéro d'assurance maladie du patient

            foreach (Patient item in Patients)
            {
                if (numero == item._assMaladie)
                {
                    Console.WriteLine("Impossible d'ajouter le patient, Le numéro d'assurance maladie existe déjà.");
                    Pause();
                    MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
                }
            }
            donnees.Add(Convert.ToString(numero));

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
                    item.AjouterPatient(numero);//******************************ÉTRANGE VÉRIFIER
                }
            }
            donnees.Add(Convert.ToString(medecinAvecMinPatient));

            donnees.Add("3000-01-01");
            // Construction d'un objet Patient dans la liste d'objets List<Patient>
            Patients.Add(new Patient(donnees[0], donnees[1], numero, donnees[3], donnees[4]));

            Console.WriteLine("Patient ajouté");

            Pause();
            MenuAjouter(ref Medecins, ref Patients, ref nombreMedecinActif);
        }
        #endregion

        #region         static void DecesPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)

        static void DecesPatient(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            bool matchNumeroAssMaladie = false;//boolléen de match de numéro d'assurance maladie trouvé

            string texte = "Numero d'assurance maladie: ";
            int numeroAssMaladie = DemanderCode(texte, 1000, 9999);// Validation de la valeur entrée par l'utilisateur

            Console.WriteLine();
            foreach (Patient item in Patients)
            {
                if (item._assMaladie == numeroAssMaladie)
                {
                    matchNumeroAssMaladie = true;
                    Console.WriteLine("Indiquer décès");
                    Console.WriteLine("--------------");
                    Console.Write("Date du décès (AAAA-MM-JJ): ");
                    DateTime dateDeces = Convert.ToDateTime(Console.ReadLine());

                    item._matriculeMedecin = 0;
                    item._dateDeces = dateDeces;

                    Pause();
                }
            }

            if (matchNumeroAssMaladie == false)
            {
                Console.WriteLine($"Il n'y a pas de patient avec le numéro d'assurance maladie {numeroAssMaladie}");
                Pause();
            }
            MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
        }
        #endregion

        #region         static int DemanderCode(string texte, int minimum, int maximum)

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
        static int DemanderCode(string texte, int minimum, int maximum)
        {
            int entier = 0;
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    Console.WriteLine(texte);
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

        #region         static void Menu1(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le menu d'entré lorsque l'application est lancée.
        /// Le menu offre les choix d'ajouter, modifier, afficher médecins et patients ou de quitter le programme.
        /// Le choix de l'utilisateur est validé.
        /// Un switch oriente le programme selon le choix de l'utilisateur.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets patient</param>
        /// <param name="nombreMedecinActif">Nombre des médecin(s) actif(s)</param>
        static void Menu1(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
                    Console.WriteLine("Sauvegarde des données et fin du programme");
                    Pause();
                    break;
            }
        }
        #endregion

        #region         static void MenuAfficher(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le Menu Afficher où l'information concernant médecins et patients peut être vu sous différentes façon.
        /// Le choix de l'utilisateur est validé
        /// Un switch fait la sélection de la méthode à apeller selon le choix de l'utilisateur
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecins actifs</param>
        static void MenuAfficher(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
                        Menu1(ref Medecins, ref Patients, ref nombreMedecinActif);
                        break;
                }
                MenuAfficher(ref Medecins, ref Patients, ref nombreMedecinActif);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region         static void MenuAjouter(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Permet de choisir l’élément à ajouter, un médecin ou un patient.
        /// Pour pouvoir ajouter un patient, au moins un médecin actif doit être défini.
        /// Si aucun médecin actif n’est défini, un message informe l’utilisateur et l’ajout du patient est impossible. 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        static void MenuAjouter(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
            }
        }
        #endregion

        #region     static void MenuModifier(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Affiche le Menu Modifier où un médecin peut être mis à la retraite ou le décès d'un patient peut être enregistré
        /// Le choix est validé
        /// Si le nombre de médecins actifs est inférieur à 2 il est impossible de mettre un médecin à la retraite
        /// Un switch fait la sélection de la méthode à apeller selon qu'un médecin est mis à la retraite ou un patient est décédé
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecins actifs</param>
        static void MenuModifier(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            Console.Clear();
            char choixChar = '0';
            try
            {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
        /// <summary>
        /// Ouvre le canal d'écriture de fichierMedecins
        /// Écrit le contenu des informations des médecins
        /// Ouvre le canal d'écriture de fichierPatients
        /// Écrit le contenu des informations des patients
        /// </summary>
        /// <param name="Medecins">nom de la List<Medecin> passé en paramètre référence</param>
        /// <param name="Patients">nom de la List<Patient> passé en paramètre référence</param>
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
                            canalEcriturePat.WriteLine($"{item._assMaladie};{item._prenom};{item._nom};{0};{item._dateDeces}");
                        }
                        else
                        {
                            canalEcriturePat.WriteLine($"{item._assMaladie};{item._prenom};{item._nom};{item._matriculeMedecin}");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Pause();
            }
        }
        #endregion

        #region         static void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Il est possible pour un médecin de partir à la retraite.
        /// Il est par contre impossible de ne plus avoir de médecins actifs dans le système. (Cette condition est vérifiée avant l'entrée dans cette méthode).
        /// Donc s’il n’y a qu’un seul médecin actif, il est impossible pour lui de partir à la retraite (et ceci même si aucun patient n’est défini).
        /// Lors d’un départ à la retraite, le système demande le code d’identification du médecin.Celui-ci doit être valide et correspondre à un médecin du système.
        /// Sinon, un message d’erreur est affiché et l’opération est annulée.
        /// Lorsqu’un code valide est donné, la date du départ à la retraite est demandée, en format Année/Mois/Jour.
        /// La date est validée. Lorsqu’une date valide est donnée, le médecin est alors marqué comme retraité.
        /// Il demeure quand même dans le système, il n’est pas effacé.
        /// Tous les patients associés à ce médecin sont alors redistribués un par un aux médecins actifs, selon le même critère lors d’un ajout,
        /// c’est-à-dire en choisissant le (ou l’un des) médecin ayant le moins de patients.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        static void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            try
            {
                int minimumPatient = 1000;// Nombre de patient minimum qu'un des médecins actifs a
                int medecinAvecMinPatient = 0;// Matricule du médecin actif ayant le moins de patient
                bool match = false;
                int nombreMedecinActifDebut = nombreMedecinActif;

                string texte = "Code d'identification: ";
                int matricule = DemanderCode(texte, 100, 999);// matricule est celui du médecin que l'utilisateur désire retraiter
                Console.WriteLine();
                
                foreach (Medecin itemMedecin in Medecins)// Pour chaque médecin dans la liste des médecins
                {
                    if (itemMedecin._matricule == matricule)// Si le médecin existe dans le registre des médecins
                    {
                        match = true;// Oui le médecin existe
                        if (itemMedecin._dateRetraite != itemMedecin._nonRetraite)// Si le médecin en question est déjà à la retraite
                        {
                            Console.WriteLine($"Le médecin matricule {itemMedecin._matricule} est déja retraité");// Oui il est déjà retraité
                            Pause();
                            MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);// Retour au menu modifier
                        }
                    }
                }

                if (match == false)// S'il n'y a pas de médecin avec le matricule choisi par l'utilisateur
                {
                    Console.WriteLine($"Il n'y a pas de médecin avec le code D'identification {matricule}");// Message à l'utilisateur
                    Pause();
                    MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);// Retour au menu modifier
                }

                foreach (Medecin itemMedecin in Medecins)// Pour chaque médecin dans la liste des médecins
                {
                    if (itemMedecin._matricule == matricule)// Si un des médecins (qui n'est pas déjà à la retraite) a le matricule recherché 
                    {
                        Console.WriteLine("Indiquer retraite");
                        Console.WriteLine("-----------------");
                        Console.Write("Date de la retraite (AAAA-MM-JJ): ");// La date de la retraite est demandée
                        DateTime dateRetraite = Convert.ToDateTime(Console.ReadLine());//************************DATETIME

                        nombreMedecinActif -= 1;// Le nombre de médecins actifs est réduit de 1

                        foreach (Patient itemPatient in Patients)// Pour chacun des patient de la liste des patients
                        {
                            if (itemPatient._matriculeMedecin == itemMedecin._matricule)// Si le patient a comme médecin celui qui vient d'être retraité
                            {
                                foreach (Medecin medecinListe in Medecins)// Pour chaque médecin dans la liste des médecins
                                {
                                    // Si le médecin de la liste n'est pas retraité et que son matricule est différent de celui qui vient d'être retraité
                                    if (medecinListe._dateRetraite == medecinListe._nonRetraite && medecinListe._matricule != itemPatient._matriculeMedecin)
                                    {
                                        if (medecinListe._ListePatient.Count < minimumPatient)// Si le nombre de patient(s) du médecin de la liste est inférieur au compteur du minimum de patients
                                        {
                                            minimumPatient = medecinListe._ListePatient.Count;// Alors le compteur minimum de patient prend la valeur du nombre de patient(s) du médecin
                                            medecinAvecMinPatient = medecinListe._matricule;// Et le matricule du médecin est entré dans la variable avec le minimum de patient
                                        }
                                    }
                                }

                                itemPatient._matriculeMedecin = medecinAvecMinPatient;// Le patient est assigné au médecin avec le minimum de patient(s)
                
                                itemMedecin.EnleverPatient(itemPatient._assMaladie);// Le patient est retiré de la liste des patients du médecin retraité

                                foreach (Medecin item in Medecins)// Pour chaque médecin dans la liste des médecins
                                {
                                    if (item._matricule == medecinAvecMinPatient)// Si le matricule du médecin est celue qui a le moins de patients
                                    {
                                        item.AjouterPatient(itemPatient._assMaladie);// Le patient retiré de la liste du médecin retraité est ajouté à la liste du médecin ayant le moins de patients
                                    }
                                }
                            }
                            minimumPatient = 1000;// Remise des variables aux valeurs initiales
                            medecinAvecMinPatient = 0;
                        }
                        // Tous les patients on été réassignés
                        itemMedecin._dateRetraite = dateRetraite;// Alors la date de la retraite du médecin est entrée au registre
                        MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);// Retour au menu Modifier
                    }
                }
                Pause();
                MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);
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
