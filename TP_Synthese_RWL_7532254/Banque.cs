using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    class Banque : Object //Classe de base de toutes les classes définies dans ce programme.
        // Il n'est pas nécessaire d'indiquer l'héritage de la classe Objet puisqu'elle est implicite.
        // J'ai décidé de l'indiquer pour essayer de comprendre.
    {
        #region public Banque()

        public Banque()
        {
            // TODO
            _listeDesComptes = new List<Compte>();// l'objet est créé. De la mémoire est allouée.
            // À partir d'ici la variable peut être utilisée, elle existe, car elle a été créé avec l'instruction "new"
            #region Lecture du fichier des comptes
            try
            {
                string fichierComptes = "comptes.txt";
                // Ouverture du canalRead pour l'accès au fichier "comptes.txt"
                using (StreamReader canalRead = new StreamReader(fichierComptes))
                {
                    // Lit la première ligne qui identifie le compte
                    string ligne = canalRead.ReadLine();
                    int numeroCompte = 0;

                    while (ligne != null)
                    {
                        List<string> donnees = new List<string>(ligne.Split(';'));
                        if (donnees.Count >= 4)
                        {
                            // Extrait les valeurs individuelles de la ligne

                            string type = donnees[0];
                            string numero = donnees[1];

                            string prenom = donnees[2];
                            string nom = donnees[3];
                            string typeComptePossible = "ECR";
                            double limiteCredit = 0;

                            if (donnees.Count < 4)
                            {
                                throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                            }

                            if (donnees.Count == 5)
                            {
                                limiteCredit = Convert.ToDouble(donnees[4]);
                            }
                            if (donnees.Count > 5)
                            {
                                throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                            }

                            if (donnees[0].Length > 1 || !typeComptePossible.Contains(donnees[0]))
                            {
                                throw new Exception("Erreur le fichier n'est pas valide; le type de compte n'est pas conforme.");
                            }
                            if (donnees[1].Length < 3)
                            {
                                throw new Exception("Erreur, le numéro de compte est invalide.");
                            }

                            numeroCompte = Convert.ToInt32(donnees[1]);

                            if (numeroCompte < 101 || numeroCompte > 9999)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide; le numéro de compte est en erreur");
                            }

                            foreach (Compte item in _listeDesComptes)
                            {
                                if (item.NumeroDeCompte == numeroCompte)
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro de comptes identiques.");
                                }
                            }

                            switch (type)
                            {
                                case "C": _listeDesComptes.Add(new CompteCheque(type, prenom, nom, numeroCompte)); break;
                                case "E": _listeDesComptes.Add(new CompteEpargne(type, prenom, nom, numeroCompte)); break;
                                case "R": _listeDesComptes.Add(new CompteCredit(type, prenom, nom, limiteCredit, numeroCompte)); break;
                                default: throw new Exception("Type de compte invalide");
                            }
                        }
                        ligne = canalRead.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier comptes.txt sera créé");
                Program.Pause();
            }
            //listeDe Comptes.sort(
            // Lecture du fichier transactions

            #endregion

            #region Lecture du fichier des transactions
            try
            {
                string fichierTransactions = "transactions.txt";
                // Ouverture du canalRead pour l'accès au fichier "transactions.txt"
                using (StreamReader canalRead = new StreamReader(fichierTransactions))
                {
                    // Lit la première ligne qui identifie le compte
                    string ligne = canalRead.ReadLine();
                    int numeroCompte = 0;

                    while (ligne != null)
                    {
                        List<string> donnees = new List<string>(ligne.Split(';')); // Extrait les valeurs individuelles de la ligne

                        if (donnees.Count == 4)
                        {
                            //string numero = donnees[0];
                            string type = donnees[1];

                            double montant = Convert.ToDouble(donnees[2]);

                            string date = donnees[3];
                            string typeTransactionPossible = "DR";

                            numeroCompte = Convert.ToInt32(donnees[0]);

                            if (numeroCompte < 101 || numeroCompte > 9999)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide; le numéro de compte est en erreur");
                            }

                            if (type.Length > 1 || !typeTransactionPossible.Contains(type))
                            {
                                throw new Exception("Erreur le fichier n'est pas valide; le type de compte n'est pas conforme.");
                            }

                            foreach (Compte item in _listeDesComptes)
                            {
                                if (item.NumeroDeCompte == numeroCompte)
                                {
                                    throw new Exception("Erreur, le fichier n'est pas valide; Il y a deux numéros de compte identiques.");
                                }
                            }

                            switch (type)
                            {
                                case "D": Deposer(numeroCompte, montant); break;
                                case "R": Retirer(numeroCompte, montant); break;
                                default: throw new Exception("Type de compte invalide");
                            }
                        }
                        if (donnees.Count < 4)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }

                        if (donnees.Count > 5)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                        }
                        ligne = canalRead.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier transactions.txt sera créé");
                Program.Pause();
            }
            #endregion
            //listeDe Comptes.sort(
        }
        #endregion

        #region public int AjouterCompte(string type, string prenom, string nom, double montant)

        public int AjouterCompte(string type, string prenom, string nom, double montant)
        {
            Compte nouveauCompte;
            switch (type)
            {
                case "C": nouveauCompte = new CompteCheque(type, prenom, nom); break;
                case "E": nouveauCompte = new CompteEpargne(type, prenom, nom); break;
                case "R": nouveauCompte = new CompteCredit(type, prenom, nom, 1000 /* Générer un nombre aléatoire */); break;
                default: throw new Exception("Type de compte invalide");
            }

            if (montant > 0)
            {
                // Faire une transaction de dépôt dans le compte du montant initial
                // Sauvegarder la transaction dans le fichier des transactions
            }


            _listeDesComptes.Add(nouveauCompte);

            SauvegarderCompte(nouveauCompte);

            return nouveauCompte.NumeroDeCompte;//pcq est défini public dans compte

        }
        #endregion

        private void SauvegarderCompte(Compte compte)
        {
            using (StreamWriter fichier = new StreamWriter("comptes.txt", true))
            {
                //compte.Sauvegarder(fichier);
            }
        }

        #region public double CalculerInterets(int numeroCompte)
        /// <summary>
        /// Cette option calcule les intérêts courants du compte. Le solde est inchangé par cette opération.
        /// Le calcul effectué dépend du type de compte. 
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <returns></returns>
        public double CalculerInterets(int numeroCompte)
        {
            string typeDeCompte = "C"; // C = cheques , E = epargne , R = credit;
            double interet = 0; //********************** TODO
            double solde = 100;
            if (typeDeCompte == "C")
            {
                interet = 0.001 * solde;
            }
            if (typeDeCompte == "E")
            {
                interet = 0.01 * solde;
            }
            if (typeDeCompte == "R")
            {
                if (solde < 0)
                {
                    interet = 0.045 * solde;
                }
                else
                {
                    interet = 0;
                }
            }

            return interet;
        }
        #endregion

        #region public double Deposer(int numeroCompte, double montant)

        public double Deposer(int numeroCompte, double montant)
        {
            //int oui = Compte.CompareTo();

            double solde = +montant;
            return solde;
        }
        #endregion

        #region public List<string> ListeDeComptes()

        public List<string> ListeDeComptes()
        {
            // Ici, surement une liste des numéro de compte déjà existants
            List<string> liste = new List<string>();
            //TODO
            foreach (var item in _listeDesComptes)
            {
                string type;
                string limiteCredit;

                switch (item.Type)
                {
                    case "C":
                        {
                            type = "Chèques";
                            limiteCredit = "";
                        }
                        break;
                    case "E":
                        {
                            type = "Épargne";
                            limiteCredit = "";
                        }
                        break;
                    case "R":
                        {
                            type = "Crédit";
                            limiteCredit = $"          Limite de crédit:  {Convert.ToString(item.NumeroDeCompte)} $";
                        }
                        break;
                    default: throw new Exception("Type de compte invalide");

                }

                liste.Add($"{Convert.ToString(item.NumeroDeCompte)}  {type}   {item.Nom}, {item.Prenom}{limiteCredit}");
            }
            return liste;
        }
        #endregion

        #region public double Retirer(int numeroCompte, double montant)
        public double Retirer(int numeroCompte, double montant)
        {
            // TODO
            return 13.23;
        }
        #endregion

        #region public double Solde(int numeroCompte)

        public double Solde(int numeroCompte)
        {
            return 34.23;
        }
        #endregion

        #region public void ValiderExistence(int numeroCompte)

        public void ValiderExistence(int numeroCompte)
        {

        }
        #endregion


        private Compte _compte;
        private List<Compte> _listeDesComptes;// Ici le nom de la variable (attribut) est défini. 
        // À ce stade ci: - il n'y a pas encore de mémoire d'allouée dans l'ordinateur pour cette variable.
        //                - l'objet n'a pas été initialisé, donc ne peut être utilisé tant que son constructeur n'est pas appelé.

        const string _creditDefaut = "0";
    }
}
