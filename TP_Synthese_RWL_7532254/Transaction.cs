using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    abstract class Transaction
    {
        public Transaction()
        {

        }
        public void LimiteDeCredit(string numeroDeCompte, string typeDeCompte, string limiteDeCredit)
        {

        }
        public void AjouterMontantALaListeDesTransactions()
        {
        
        }
        public void Action2(Compte compte)
        {

        }
        private List<Transaction> _listeDesTransactions;
    }
  
}
