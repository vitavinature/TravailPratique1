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
    class Banque
    {
        #region public Banque()

        public Banque()
        {
            // TODO
            _listeDesComptes = new List<Compte>();

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
                        // Extrait les valeurs individuelles de la ligne
                        List<string> donnees = new List<string>(ligne.Split(';'));

                        if (donnees.Count < 4)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                        }
                        if (donnees.Count == 4)
                        {
                            donnees.Add(_creditDefaut);
                        }
                        if (donnees.Count > 5)
                        {
                            throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                        }
                        if (donnees[0].Length > 1 || !donnees[0].Contains("E") && !donnees[0].Contains("C") && !donnees[0].Contains("R"))
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le type de compte n'est pas conforme.");
                        }
                        if (donnees[1].Length < 3)
                        {
                            throw new Exception("Erreur, le numéro de compte est invalide.");
                        }

                        numeroCompte = Convert.ToInt32(donnees[1]);

                        if (numeroCompte < 100 || numeroCompte > 9999)
                        {
                            throw new Exception("Erreur le fichier n'est pas valide; le numéro de compte est en erreur");
                        }
                        foreach (Compte item in _listeDesComptes)
                        {
                            if (item.NumeroCompte == numeroCompte)
                            {
                                throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro de comptes identiques.");
                            }
                        }

                        // Construction d'un objet Compte
                        _listeDesComptes.Add(new Compte(numeroCompte, donnees[2], donnees[3], donnees[0]));

// DateTime dateDAujourdhui = DateTime.Today;
                       // string date = Convert.ToString(dateDAujourdhui);
                        Transaction.LimiteDeCredit(donnees[1], donnees[0], donnees[4]);
                        //************************************************************


                        ligne = canalRead.ReadLine(); // Lecture de la ligne suivante dans le fichier
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
        #endregion

        #region public void Action(DbTransaction transaction)
        public void Action(DbTransaction transaction)
        {

        }
        #endregion

        #region public int AjouterCompte(string type, string prenom, string nom, double montant)

        public int AjouterCompte(string type, string prenom, string nom, double montant)
        {

            int numero = Compte.DernierNumero();

            // _compte = Compte(numero, prenom, nom, montant, type);

            // TODO
            // Ici un numéro de compte doit être généré
            return numero;
        }
        #endregion

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

        //private Compte _compte;
        private List<Compte> _listeDesComptes;//c'est pas sur que ça va ici;
        const string _creditDefaut = "0";
    }
}
