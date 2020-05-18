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
    abstract class Compte : IComparable<Compte>// La classe compte est une classe abstraite. Il est impossible de créer un objet de ce type.
        // Il est possible d'avoir une variable d'un type abstrait "Compte unCompte;"
        // Il n'est pas possible de créer un simple compte: "unCompte = new Compte();"
    {
        #region        public Compte(string type, string prenom, string nom)// Constructeur qui crée un nouveau compte.

        public Compte(string type, string prenom, string nom)// Constructeur qui crée un nouveau compte.
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
            _solde = 0;// Créer un nouveau compte revient à créer un compte avec un solde de zéro, ûis de faire un dépôt.
            // Il doit donc y avoir yne transaction de dépôt de sauvegardée dans le fichier de transaction lors de la création d'un compte.
            _numeroDeCompte = ++_dernierNumero;// Le prochain numéro (le dernier numéro attribué ou lu, incrémenté) est attribué à ce compte
            _dernierNumero = _numeroDeCompte;// _denierNumero est réinitialisé avec le dernier numéro de compte attribué
        }
        #endregion

        #region        public Compte(string type, string prenom, string nom, int numero)// Constructeur pour compte existant dans le fichier texte.
        public Compte(string type, string prenom, string nom, int numero)// Constructeur pour compte existant dans le fichier texte.
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
            _solde = 0;// Lors de la lecture du fichier compte, le solde du compte est initialisé à zéro. Puis  à la lecture de toutes les transactions
            // (inclauant le dépôt initial) on trouve le solde du compte.
            _numeroDeCompte = numero;
            if (numero > _dernierNumero­­­)// Pour s'assurer de ne pas réutiliser un numéro de compte existant, il faut mettre à jour le dernier numéro.
                // On conserve ainsi le  plus grand numéro déjà utilisé. Les prochains comptes à être créés vont continuer à partir de là.
                // Cette méthode fait qu'il n'est pas nécessaire de valider si un numéro de compte existe déjà aavant de l'octroyer.
                // _dernierNuméro donne une valeur directement utilisable.
            {
                _dernierNumero = numero;
            }
        }
        #endregion

        #region        public int CompareTo(Compte that)
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
        #endregion

        public abstract void SauvegarderCompte(Compte nouveauCompte);

        public string Type { get { return _type; } set { } }
        public string Prenom { get { return _prenom; } set { } }

        public string Nom { get { return _nom; } set { } }
        public double Solde { get { return _solde; } set { } }

        public int NumeroDeCompte { get { return _numeroDeCompte; } }// Pour que le numéro de compte soit accessible dans la banque, cette propriété est nécessaire.

        // Un attribut static est dit un "attribut de classe", par opposition à un attribut d'objet pour les attributs ordinaires
        // Tous les objets de la classe partage la même variable
        private static int _dernierNumero = 100; // Les numéros de compte débutent à 101

        // Le mot clé readonly est similaire à const, mais la valeur n'a pas besoin d'être connue à la déclaration
        // l'initialisation se fera dans le constructeur, puis la valeur ne pourra plus être modifiée
        // Permet de protéger le code d'erreurs éventuelles en indiquant que la valeur ne devrait pas changer
        private readonly string _type;
        private readonly string _prenom;
        private readonly string _nom;
        private readonly double _solde;// Le solde du compte n'est pas conservé. Le montant initial est considéré comm un dépôt.
        // Numéro qui identifie le compte de manière unique
        private int _numeroDeCompte;

    }
}
