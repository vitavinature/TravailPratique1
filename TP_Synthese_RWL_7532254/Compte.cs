using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    abstract class Compte : IComparable<Compte>
    {
        //public Compte(int numero, string prenom, string nom, double solde, string type) 
        public Compte(int numero, string prenom, string nom, string type) 
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
           // _solde = solde;
            _numero = numero;
        }
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
            // Détermine si this est plus petit, égal
            // ou plus grand que that 
            // Retourne -1, 0 ou 1 en conséquence 

            // ... 
            return 0;
        }

        public int NumeroCompte { get { return _numero; } set { _numero = DernierNumero(); } }
        public string Type { get { return _type; } set {  } }
        public string Prenom { get { return _prenom; } set {} }

        public string Nom { get { return _nom; } set {} }

        #region public static int DernierNumero()
        /// <summary>
        /// Accesseur du dernier numéro utilisé
        /// </summary>
        /// <returns></returns>
        // Accesseur du dernier numéro utilisé
        // Une Méthode static est une méthode de classe, et non une méthode d'objet
        // Il n'est pas nécessaire d'avoir un objet de la classe pour appeller la méthode
        public static int DernierNumero()
        {
            // Une méthode statique ne peut utiliser que des attributs statics
            return _dernierNumero;
        }
        #endregion


        // Un attribut static est dit un "attribut de classe", par opposition à un attribut d'objet pour les attributs ordinaires
        // Tous les objets de la classe partage la même variable
        private static int _dernierNumero = 101; // Les numéros de compte débutent à 101

        // Le mot clé readonly est similaire à const, mais la valeur n'a pas besoin d'être connue à la déclaration
        // l'initialisation se fera dans le constructeur, puis la valeur ne pourra plus être modifiée
        // Permet de protéger le code d'erreurs éventuelles en indiquant que la valeur ne devrait pas changer
        private readonly string _type;
        private readonly string _prenom;
        private readonly string _nom;
       // private readonly double _solde;

        // Numéro qui identifie le compte de manière unique
        private int _numero;
        private List<Compte> _listeComptes;
    }
}
