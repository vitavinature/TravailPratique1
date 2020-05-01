using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Synthese_RWL_7532254
{
    abstract class Compte : IComparable<Compte>
    {
        public int CompareTo(Compte that)
        {
            /*Une classe qui implémente l’interface « IComparable » doit définir la méthode « CompareTo » et déterminer si l’objet reçu en paramètre est plus petit, égal ou plus grand que l’objet courrant.Les valeurs de retour possibles sont : 
            	-1 : L’objet courrant (this) est plus petit que l’objet reçu en paramètre(that). 
            	 0 : L’objet courrant(this) est égal à l’objet reçu en paramètre(that). 
            	 1 : L’objet courrant(this) est plus grand que l’objet reçu en paramètre(that). 
            */

            // On doit vérifier que l'objet donné n'est pas nul 
            if (that == null)
            {
                return 1;// On se considère plus grand qu'un objet nul 
            }
            // Détermine si 'this' est plus petit, égal, 
            // ou plus grand que 'that' 
            // Retourne -1, 0 ou 1 en conséquence 

            // ... 
            return 0;
        }

        private int _numero = 100;
    }
}
