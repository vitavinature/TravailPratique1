using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TravailPratique1;

namespace Preparation_1
{
    class GestionnaireDeMedecins
    {
        public GestionnaireDeMedecins()
        {
            _listeMedecins = new List<Medecin>();

            #region Lecture du fichier des médecins
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
                            _nombreMedecinActif += 1;
                            donnees.Add(Convert.ToString(_dateDefaut));
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

                        // Construction d'un objet Medecin dans la liste d'objets LIST<Medecin>
                        _listeMedecins.Add(new Medecin(donnees[1], donnees[2], matricule, donnees[3], _nombreMedecinActif));

                        ligneMed = canalLectureMed.ReadLine(); // Lecture d'une autre ligne dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier medecins.txt sera créé");
                Program.Pause();
            }
            #endregion
        }

        #region         public void AfficherLesStatistiques(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)

        public void AfficherLesStatistiques(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            int compteurMedecin = 0;
            int compteurMedecinRetraite = 0;

            Console.WriteLine("Le système contient:");
            foreach (Medecin itemMedecin in gestionMedecin._listeMedecins)
            {
                compteurMedecin += 1;
                if (itemMedecin.DateRetraite != itemMedecin.NonRetraite)
                {
                    compteurMedecinRetraite += 1;
                }
            }
            Console.WriteLine($"{compteurMedecin} médecins, dont {compteurMedecinRetraite} à la retraite");

            gestionPatient.StatistiquesPatients();

            Program.Pause();
        }
        #endregion

        #region         public void AfficherListeMedecins()

        public void AfficherListeMedecins()
        {
            Console.WriteLine();
            Console.WriteLine("Liste des médecins");
            Console.WriteLine("------------------");

            foreach (Medecin item in _listeMedecins)
            {
                item.Afficher();
            }
            Program.Pause();
        }
        #endregion

        #region         public void AfficherLeMedecinDUnPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient, int assuranceMaladie, int matriculeMedecin)
        /// <summary>
        /// Affiche les informations du médecin d'un patient: prénom, nom, matricule
        /// </summary>
        /// <param name="gestionMedecin"></param>
        /// <param name="gestionPatient"></param>
        /// <param name="assuranceMaladie"></param>
        /// <param name="matriculeMedecin"></param>
        public void AfficherLeMedecinDUnPatient(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient, int assuranceMaladie, int matriculeMedecin)
        {
            foreach (Medecin item in _listeMedecins)
            {
                if (item.Matricule == matriculeMedecin)
                {
                    Console.WriteLine($"{item.Matricule} {item.Prenom} {item.Nom}");
                }
            }
            Program.Pause();
        }
        #endregion

        #region         public void AfficherUnMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        /// <summary>
        /// Affiche les informations d'un médecin: prénom, nom, matricule et la liste des ses patients, s'il n'est pas retraité.
        /// S'il est retraité, la date du début de sa retraite est affichée.
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patient</param>
        public void AfficherUnMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            string texte = "Code d'identification: ";
            int codeIdentification = Program.DemanderCode(texte, 100, 999);

            foreach (Medecin itemMedecin in _listeMedecins)
            {
                if (itemMedecin.Matricule == codeIdentification)
                {
                    itemMedecin.Afficher2();
                }
            }
            Program.Pause();
        }
        #endregion

        #region         public void AjouterMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        /// <summary>
        /// Demande le (Prénom et le Nom qui sont des chaines de caractères), et le (Code d’identification, un entier) du médecin
        /// Un message d’erreur indique que l’ajout est impossible si un médecin portant le même code d’identification est déjà présent dans le système 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patients</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        public void AjouterMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            try
            {
                List<string> donnees = new List<string>();

                Console.WriteLine();
                Console.WriteLine("Ajout d'un médecin");
                Console.WriteLine("------------------");
                string prenom = Program.DemanderTexte("Prénom");
                donnees.Add(prenom);

                string nom = Program.DemanderTexte("Nom");
                donnees.Add(nom);

                string texte = "Code d'identification: ";
                int matricule = Program.DemanderCode(texte, 100, 999);

                foreach (Medecin item in _listeMedecins)
                {
                    if (matricule == item.Matricule)
                    {
                        Console.WriteLine("Impossible d'ajouter le médecin, Le  code d'identification existe déjà.");
                        return;
                    }
                }
                donnees.Add(Convert.ToString(matricule));
                donnees.Add("3000/01/01");
                // Construction d'un objet Medecin dans la liste d'objets List<Medecin>
                _nombreMedecinActif += 1;
                //_medecins.Add(new Medecin(donnees[0], donnees[1], matricule, donnees[3], ref _nombreMedecinActif));
                _listeMedecins.Add(new Medecin(donnees[0], donnees[1], matricule, donnees[3], _nombreMedecinActif));

                Console.WriteLine("Médecin ajouté");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region         public void RetraitDUnPatientDecede(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient, int matriculeMedecin, int numeroAssMaladie)

        /// <summary>
        /// Retrait d'un patient décédé de la liste de patients d'un médecin
        /// </summary>
        /// <param name="gestionMedecin"></param>
        /// <param name="gestionPatient"></param>
        /// <param name="matriculeMedecin"></param>
        /// <param name="numeroAssMaladie"></param>
        public void RetraitDUnPatientDecede(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient, int matriculeMedecin, int numeroAssMaladie)
        {
            foreach (Medecin item in _listeMedecins)
            {
                if (item.Matricule == matriculeMedecin)
                {
                    _listePatient.Remove(numeroAssMaladie);
                }
            }
        }
        #endregion

        public void AjouterPatientALaListeDunMedecin(ref GestionnaireDeMedecins gestionMedecin, int matriculeMedecin, int numeroAssMaladie)
        {
            foreach (Medecin item in _listeMedecins)
            {
                if (item.Matricule == matriculeMedecin)
                {
                    //_listePatient.Add(numeroAssMaladie);//**************************************************************PROBLÈME
                }
            }
        }

        #region         public void RetraitMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
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
        public void RetraitMedecin(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            try
            {
                int minimumPatient = 1000;// Nombre de patient minimum qu'un des médecins actifs a
                int medecinAvecMinPatient = 0;// Matricule du médecin actif ayant le moins de patient
                bool match = false;
                int nombreMedecinActifDebut = _nombreMedecinActif;

                string texte = "Code d'identification: ";
                int matricule = Program.DemanderCode(texte, 100, 999);// matricule est celui du médecin que l'utilisateur désire retraiter
                Console.WriteLine();

                foreach (Medecin itemMedecin in _listeMedecins)// Pour chaque médecin dans la liste des médecins
                {
                    if (itemMedecin.Matricule == matricule)// Si le médecin existe dans le registre des médecins
                    {
                        match = true;// Oui le médecin existe
                        if (itemMedecin.DateRetraite != itemMedecin.NonRetraite)// Si le médecin en question est déjà à la retraite
                        {
                            Console.WriteLine($"Le médecin matricule {itemMedecin.Matricule} est déja retraité");// Oui il est déjà retraité
                            Program.Pause();
                        }
                    }
                }

                if (match == false)// S'il n'y a pas de médecin avec le matricule choisi par l'utilisateur
                {
                    Console.WriteLine($"Il n'y a pas de médecin avec le code D'identification {matricule}");// Message à l'utilisateur
                    Program.Pause();
                }

                foreach (Medecin itemMedecin in _listeMedecins)// Pour chaque médecin dans la liste des médecins
                {
                    if (itemMedecin.Matricule == matricule)// Si un des médecins (qui n'est pas déjà à la retraite) a le matricule recherché 
                    {
                        Console.WriteLine("Indiquer retraite");
                        Console.WriteLine("-----------------");
                        Console.Write("Date de la retraite (AAAA/MM/JJ): ");// La date de la retraite est demandée
                                                                            //DateTime dateRetraite = Convert.ToDateTime(Console.ReadLine());//************************DATETIME
                        DateTime dateRetraite = DateTime.Parse(Console.ReadLine());//************************DATETIME valider ????

                        _nombreMedecinActif -= 1;// Le nombre de médecins actifs est réduit de 1

                        foreach (Patient itemPatient in itemMedecin.ListePatient)// Pour chacun des patient de la liste des patients
                        {
                            if (itemPatient.MatriculeMedecin == itemMedecin.Matricule)// Si le patient a comme médecin celui qui vient d'être retraité
                            {
                                foreach (Medecin medecinListe in _listeMedecins)// Pour chaque médecin dans la liste des médecins
                                {
                                    // Si le médecin de la liste n'est pas retraité et que son matricule est différent de celui qui vient d'être retraité
                                    if (medecinListe.DateRetraite == medecinListe.NonRetraite && medecinListe.Matricule != itemPatient.MatriculeMedecin)
                                    {
                                        if (medecinListe.ListePatient.Count < minimumPatient)// Si le nombre de patient(s) du médecin de la liste est inférieur au compteur du minimum de patients
                                        {
                                            minimumPatient = medecinListe.ListePatient.Count;// Alors le compteur minimum de patient prend la valeur du nombre de patient(s) du médecin
                                            medecinAvecMinPatient = medecinListe.Matricule;// Et le matricule du médecin est entré dans la variable avec le minimum de patient
                                        }
                                    }
                                }

                                //*********************************************************************************************************************************************************************************************

                                itemMedecin.EnleverPatient(itemPatient);// Le patient est retiré de la liste des patients du médecin retraité

                                foreach (Medecin item in gestionMedecin.ListeMedecins)// Pour chaque médecin dans la liste des médecins
                                {
                                    if (item.Matricule == medecinAvecMinPatient)// Si le matricule du médecin est celue qui a le moins de patients
                                    {
                                        item.AjouterPatient(itemPatient);// Le patient retiré de la liste du médecin retraité est ajouté à la liste du médecin ayant le moins de patients
                                    }
                                }
                            }
                            minimumPatient = 1000;// Remise des variables aux valeurs initiales
                            medecinAvecMinPatient = 0;
                        }
                        // Tous les patients on été réassignés
                        _dateRetraite = dateRetraite;// Alors la date de la retraite du médecin est entrée au registre
                    }
                }
                Program.Pause();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        public void SauvegarderMedecins(ref GestionnaireDeMedecins gestionMedecin, ref GestionnaireDePatients gestionPatient)
        {
            string fichierMedecins = "medecins.txt";
            // Ouverture du canalEcritureMed pour l'écriture dans le fichier "medecins.txt"
            using (StreamWriter canalEcritureMed = new StreamWriter(fichierMedecins))
            {
                foreach (Medecin item in ListeMedecins)
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
        }

            public int TrouverMedecinAvecMinPatient()
            {
                int minimumPatient = 1000;

                foreach (Medecin item in ListeMedecins)
                {
                    if (item.DateRetraite == item.NonRetraite)
                    {
                        if (item.ListePatient.Count < minimumPatient)
                        {
                            minimumPatient = item.ListePatient.Count;
                            _medecinAvecMinimumPatient = item.Matricule;
                        }
                    }
                }
                return _medecinAvecMinimumPatient;
            }




        public int NombreMedecinActif { get { return _nombreMedecinActif; } }
        public List<Medecin> ListeMedecins { get { return _listeMedecins; } }

        private List<Medecin> _listeMedecins;

        private int _medecinAvecMinimumPatient;

        private int _nombreMedecinActif = 0;
        private List<int> _listePatient;
        private DateTime _dateDefaut = new DateTime(3000, 01, 01);
        private DateTime _dateRetraite = new DateTime();
    }
}
