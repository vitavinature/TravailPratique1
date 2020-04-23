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


        }

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
        private List<Medecin> _listeMedecins;

        private int _nombreMedecinActif = 0;

        private string _dateDefaut = "3000/01/01";
    }
}
