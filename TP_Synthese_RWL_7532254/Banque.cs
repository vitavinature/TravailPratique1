using System;
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
            if (typeDeCompte=="C")
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

        #region public List<string> ListeDeComptes(List<Compte> listeCompte)

        public List<string> ListeDeComptes(Banque banque)
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

        private Compte _compte;
    }
}
