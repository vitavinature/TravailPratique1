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
            Compte(type, numero, prenom, nom, solde);
            // TODO
            // Ici un numéro de compte doit être généré
            return 123;
        }
        #endregion

        #region public double CalculerInterets(int numeroCompte)

        public double CalculerInterets(int numeroCompte)
        {
            // TODO
            return 0.25;
        }
        #endregion

        #region public double Deposer(int numeroCompte, double montant)

        public double Deposer(int numeroCompte, double montant)
        {
            return 23.45;
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

        private Compte _compte;
    }
}
