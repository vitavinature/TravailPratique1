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
                            donnees.Add(_dateDefaut);
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
                        _listeMedecins.Add(new Medecin(donnees[1], donnees[2], matricule, donnees[3], ref nombreMedecinActif));

                        ligneMed = canalLectureMed.ReadLine(); // Lecture d'une autre ligne dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier medecins.txt sera créé");
                Pause();
            }
            #endregion


        }
        #region         static void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        /// <summary>
        /// Demande le (Prénom et le Nom qui sont des chaines de caractères), et le (Code d’identification, un entier) du médecin
        /// Un message d’erreur indique que l’ajout est impossible si un médecin portant le même code d’identification est déjà présent dans le système 
        /// </summary>
        /// <param name="Medecins">Liste des objets Medecin</param>
        /// <param name="Patients">Liste des objets Patients</param>
        /// <param name="nombreMedecinActif">Nombre de médecin(s) actif(s)</param>
        public void AjouterMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
                    if (matricule == item._matricule)
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
                _listeMedecins.Add(new Medecin(donnees[0], donnees[1], matricule, donnees[3], ref _nombreMedecinActif));

                Console.WriteLine("Médecin ajouté");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region         public void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
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
        public void RetraitMedecin(ref List<Medecin> Medecins, ref List<Patient> Patients, ref int nombreMedecinActif)
        {
            try
            {
                int minimumPatient = 1000;// Nombre de patient minimum qu'un des médecins actifs a
                int medecinAvecMinPatient = 0;// Matricule du médecin actif ayant le moins de patient
                bool match = false;
                int nombreMedecinActifDebut = nombreMedecinActif;

                string texte = "Code d'identification: ";
                int matricule = Program.DemanderCode(texte, 100, 999);// matricule est celui du médecin que l'utilisateur désire retraiter
                Console.WriteLine();

                foreach (Medecin itemMedecin in Medecins)// Pour chaque médecin dans la liste des médecins
                {
                    if (itemMedecin._matricule == matricule)// Si le médecin existe dans le registre des médecins
                    {
                        match = true;// Oui le médecin existe
                        if (itemMedecin._dateRetraite != itemMedecin._nonRetraite)// Si le médecin en question est déjà à la retraite
                        {
                            Console.WriteLine($"Le médecin matricule {itemMedecin._matricule} est déja retraité");// Oui il est déjà retraité
                            Program.Pause();
                            Program.MenuModifier(ref Medecins, ref Patients, ref nombreMedecinActif);// Retour au menu modifier
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
                        Console.Write("Date de la retraite (AAAA/MM/JJ): ");// La date de la retraite est demandée
                                                                            //DateTime dateRetraite = Convert.ToDateTime(Console.ReadLine());//************************DATETIME
                        DateTime dateRetraite = DateTime.Parse(Console.ReadLine());//************************DATETIME valider ????

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

        private List<Medecin> _listeMedecins;

        private int _nombreMedecinActif = 0;

        private string _dateDefaut = "3000/01/01";
    }
}
