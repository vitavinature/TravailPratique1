﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailPratique1;


namespace Preparation_1
{
    class GestionnaireDePatients
    {
        public GestionnaireDePatients(GestionnaireDeMedecins gestionMedecin)
        {
            _listePatients = new List<Patient>();

            #region Lecture du fichier des patients
            try
            {
                string fichierPatients = "patients.txt";
                // Ouverture du canalLecturePat pour l'accès au fichier "patients.txt"
                using (StreamReader canalLecturePat = new StreamReader(fichierPatients))
                {
                    // Lit la première ligne qui identifie le patient
                    string lignePat = canalLecturePat.ReadLine();
                    int matriculeMedecin = 0;
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
                            matriculeMedecin = Convert.ToInt32(donnees[3]);

                            donnees.Add(_dateDefaut);
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
                        foreach (Patient item in _listePatients)
                        {
                            if (item.AssMaladie == numeroAssMaladie)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro d'assurance maladie identiques.");
                            }
                        }

                        // Construction d'un objet Patient
                        _listePatients.Add(new Patient(donnees[1], donnees[2], numeroAssMaladie, donnees[3], donnees[4]));


                        gestionMedecin.AjouterPatientALaListeDunMedecin(ref gestionMedecin,  matriculeMedecin, numeroAssMaladie);
                        //************************************************************


                        lignePat = canalLecturePat.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier patients.txt sera créé");
                Program.Pause();
            }
            #endregion
        }

        #region         static void AfficherListePatients(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        /// <summary>
        /// Affiche la liste des patients (numéro d'assurance maladie, prénom et nom) et leur médecin respectifs (matricule, prénom, nom).
        /// Si le patient est décédé, la mention décédé remplace l'information du médecin.
        /// </summary>
        /// <param name="Medecins"></param>
        /// <param name="Patients"></param>
        public void AfficherListePatients(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            Console.WriteLine();
            Console.WriteLine("Liste des patients");
            Console.WriteLine("------------------");
            foreach (Patient itemPatient in _listePatients)
            {
                itemPatient.Afficher();
                if (itemPatient.DateDeces == itemPatient.NonDecede)
                {
                    gestionMedecin.AfficherLeMedecinDUnPatient(ref gestionMedecin, ref gestionPatient, itemPatient.AssMaladie, itemPatient.MatriculeMedecin);
                    //*******************************************************
                }

                Console.WriteLine();
            }
            Program.Pause();
        }
        #endregion

        #region         static void AfficherUnPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        /// <summary>
        /// Affiche les informations d'un patient: numéro d'assurance maladie, prénom, nom et matricule de son médecin.
        /// Si le patient est décédé, aucun matricule de médecin n'est affiché. La date sdu décès est affichée à la place.
        /// Le numéro d'assurance maladie entré par l'utilisateur est validé. S'il ne figure pas dans le registre des patients un message est affiché.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        public void AfficherUnPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            int patientMatch = 0;// Pour vérifier que le patient demandé fait bien parti du registre des patients.
            string texte = "Numéro d'assurance maladie: ";
            int numeroAssuranceMaladie = Program.DemanderCode(texte, 1000, 9999);

            foreach (Patient itemPatient in _listePatients)
            {
                if (itemPatient.AssMaladie == numeroAssuranceMaladie)
                {
                    patientMatch += 1;
                    Console.WriteLine("Patient");
                    Console.WriteLine("-------");
                    Console.WriteLine($"Numéro d'assurance maladie: {itemPatient.AssMaladie}");
                    Console.WriteLine($"Nom: {itemPatient.Prenom} {itemPatient.Nom}");
                    if (itemPatient.MatriculeMedecin != 0)
                    {
                        Console.Write("Medecin:");

                        gestionMedecin.AfficherLeMedecinDUnPatient(ref gestionMedecin, ref gestionPatient, itemPatient.AssMaladie, itemPatient.MatriculeMedecin);

 
                    }
                    else
                    {
                        Console.WriteLine($"Décédé le {itemPatient.DateDeces.ToLongDateString()}");
                    }
                }
            }
            if (patientMatch == 0)
            {
                Console.WriteLine($"Il n'y a aucun patient avec le numéro d'assurance maladie {numeroAssuranceMaladie}.");
            }
            Program.Pause();
        }
        #endregion

        #region static void AjouterPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
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
        public void AjouterPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {

            Console.WriteLine();
            Console.WriteLine("Ajout d'un patient");
            Console.WriteLine("------------------");
            string prenom = Program.DemanderTexte("Prénom");
            List<string> donnees = new List<string>();
            donnees.Add(prenom);
            string nom = Program.DemanderTexte("Nom");
            donnees.Add(nom);

            string texte = "Numéro d'assurance maladie: ";
            int numero = Program.DemanderCode(texte, 1000, 9999);// Numéro d'assurance maladie du patient

            foreach (Patient item in _listePatients)
            {
                if (numero == item.AssMaladie)
                {
                    Console.WriteLine("Impossible d'ajouter le patient, Le numéro d'assurance maladie existe déjà.");
                    Program.Pause();
                }
            }
            donnees.Add(Convert.ToString(numero));

            //************************************************
            int medecinAvecMinPatient = gestionMedecin.TrouverMedecinAvecMinPatient();

            donnees.Add(Convert.ToString(medecinAvecMinPatient));

            donnees.Add("3000/01/01");
            // Construction d'un objet Patient dans la liste d'objets List<Patient>
            _listePatients.Add(new Patient(donnees[0], donnees[1], numero, donnees[3], donnees[4]));

            gestionPatient._matriculeMedecin = medecinAvecMinPatient;// Le patient est assigné au médecin avec le minimum de patient(s)

            Console.WriteLine("Patient ajouté");

            Program.Pause();
        }
        #endregion

        #region         public void DecesPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        /// <summary>
        /// Il est possible pour un patient de décéder. Le système demande alors le numéro d’assurance maladie du patient.
        /// Celui-ci doit être valide et correspondre à un patient du système. Sinon, un message d’erreur est affiché et l’opération est annulée. 
        /// Lorsqu’un numéro valide est donné, la date du décès est demandée, en format Année/Mois/Jour.La date doit être valide.
        /// Lorsqu’une date valide est donnée, le patient est marqué comme décédé.Il demeure quand même dans le système, il n’est pas effacé.
        /// Il est par contre enlevé de la liste des patients du médecin auquel il était associé. Il n’est plus considéré comme suivi par un médecin. 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        public void DecesPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            bool matchNumeroAssMaladie = false;// Booléen de match de numéro d'assurance maladie trouvé

            string texte = "Numero d'assurance maladie: ";// Message à l'utilisateur
            int numeroAssMaladie = Program.DemanderCode(texte, 1000, 9999);// Validation de la valeur entrée par l'utilisateur

            Console.WriteLine();// Saut d'une ligne 
            foreach (Patient item in _listePatients)// Pour chaque patient de la liste de patients
            {
                if (item.AssMaladie == numeroAssMaladie)// Si le numéro d'assurance maladie est trouvé dans la liste des patients
                {
                    matchNumeroAssMaladie = true;// Le booléen de match est activé
                    Console.WriteLine("Indiquer décès");// Affichage à l'écran
                    Console.WriteLine("--------------");
                    Console.Write("Date du décès (AAAA/MM/JJ): ");
                    //DateTime dateDeces = Convert.ToDateTime(Console.ReadLine());// Une date de décès est demandée à l'utilisateur
                    DateTime dateDeces = DateTime.Parse(Console.ReadLine());// Une date de décès est demandée à l'utilisateur

                    gestionMedecin.RetraitDUnPatientDecede(ref gestionMedecin, ref gestionPatient, item.MatriculeMedecin, numeroAssMaladie);
                    gestionPatient.MatriculeMedecin = 0;

                    //item.Patient._dateDeces = dateDeces;
                    gestionPatient.DateDeces = dateDeces;
                    Program.Pause();

                }
            }
            if (matchNumeroAssMaladie == false)
            {
                Console.WriteLine($"Il n'y a pas de patient avec le numéro d'assurance maladie {numeroAssMaladie}");
                Program.Pause();
            }
        }
        #endregion

        public void SauvegarderPatients(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            string fichierPatients = "patients.txt";
            // Ouverture du canalEcriturePat pour l'écriture dans le fichier "patients.txt"
            using (StreamWriter canalEcriturePat = new StreamWriter(fichierPatients))
            {
                foreach (Patient item in _listePatients)
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


        public void StatistiquesPatients()
        {
            int compteurPatient = 0;
            int compteurPatientDecede = 0;
            foreach (Patient item in _listePatients)
            {
                compteurPatient += 1;
                if (item.DateDeces != item.NonDecede)
                {
                    compteurPatientDecede += 1;
                }
            }
            Console.WriteLine($"  {compteurPatient} Patients, dont {compteurPatientDecede} décédés");
        }

        public int MatriculeMedecin { get { return _matriculeMedecin; } set { _matriculeMedecin = value; } }
        public DateTime DateDeces { get { return _dateDeces; } set { _dateDeces = value; } }

        const string _dateDefaut = "3000/1/1";
        private List<Patient> _listePatients;
        private DateTime _dateDeces = new DateTime();
        private DateTime _nonDeces = new DateTime(3000, 1, 1);
        private int _matriculeMedecin;

    }
}
