using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TP_Synthese_RWL_7532254
{
    class Banque
    {
        public Banque()
        {
            // TODO
        }
        public void Action(DbTransaction transaction)
        {

        }
        public void AjouterCompte(Compte type, string prenom, string nom, double montant)
        {
            // TODO
        }
        public double Retirer(Compte numeroCompte, double montant)
        {
            // TODO
            return 13.23;
        }
        public double CalculerInterest(Compte numeroCompte)
        {
            // TODO
            return 0.25;
        }
        public double Deposer(Compte numeroCompte, double montant)
        {
            return 23.45;
        }

        public void ListeDeComptes()
        {
            //TODO
        }
        public double Solde(Compte numeroCompte)
        {
            return 34.23;
        }

        public void ValiderExistence(Compte numeroCompte)
        {

        }

        private Compte _compte;
    }
}
