using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TPSynthese
{
    abstract class Compte : IComparable<Compte>
    {
        public Compte(string type, string prenom, string nom)
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
            _solde = 0;
            _numeroDeCompte = ++_dernierNumero;
        }
        public Compte(string type, string prenom, string nom, int numero)
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
            _solde = 0;
            _numeroDeCompte = numero;
            if (numero > _dernierNumero­­­)
            {
                _dernierNumero = numero;
            }
        }


        public int CompareTo(Compte that)
        {
            /*Une classe qui implémente l’interface « IComparable » doit définir la méthode « CompareTo » et déterminer si l’objet reçu en paramètre est plus petit, égal ou plus grand que l’objet courrant.Les valeurs de retour possibles sont : 
            	-1 : L’objet courrant (this) est plus petit que l’objet reçu en paramètre(that). 
            	 0 : L’objet courrant(this) est égal à l’objet reçu en paramètre(that). 
            	 1 : L’objet courrant(this) est plus grand que l’objet reçu en paramètre(that). 
            */
            int resultat = _nom.CompareTo(that._nom);
            if (resultat != 0)
            {
                return resultat;
            }
            else
            {
                resultat = _prenom.CompareTo(that._prenom);
                if (resultat !=0)
                {
                    return resultat;
                }
                resultat = _numeroDeCompte.CompareTo(that._numeroDeCompte);
                return resultat;              
            }
            // On doit vérifier que l'objet donné n'est pas nul
            /*
            if (that == null)
            {
                return 1;// On se considère plus grand qu'un objet nul 
            }
            */
        }

        public abstract void Sauvegarder(ref StreamWriter fichier);

        public string Type { get { return _type; } set { } }
        public string Prenom { get { return _prenom; } set { } }

        public string Nom { get { return _nom; } set { } }

        public int NumeroDeCompte { get { return _numeroDeCompte; } }

        // Un attribut static est dit un "attribut de classe", par opposition à un attribut d'objet pour les attributs ordinaires
        // Tous les objets de la classe partage la même variable
        private static int _dernierNumero = 100; // Les numéros de compte débutent à 101

        // Le mot clé readonly est similaire à const, mais la valeur n'a pas besoin d'être connue à la déclaration
        // l'initialisation se fera dans le constructeur, puis la valeur ne pourra plus être modifiée
        // Permet de protéger le code d'erreurs éventuelles en indiquant que la valeur ne devrait pas changer
        private readonly string _type;
        private readonly string _prenom;
        private readonly string _nom;
        private readonly double _solde;
        // Numéro qui identifie le compte de manière unique
        private int _numeroDeCompte;
        //private List<Compte> _listeDesComptes;

    }
}
