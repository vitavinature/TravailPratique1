﻿using System;
using System.IO;
using System.Text;

namespace TPSynthese
{
    /// <summary>
    /// La classe Compte redéfinit la méthode ToString pour retourner la description du compte afficher par la fonctionnalité « Lister les comptes » du programme.
    /// Elle est responsable de gérer et d’assigner le numéro unique au compte lors de sa création. 
    /// Elle implémente l’interface IComparable pour permettre d’afficher les comptes dans l’ordre demandé par la fonctionnalité « Lister les comptes » du programme.
    /// </summary>
    abstract class Compte : IComparable<Compte>// La classe compte est une classe abstraite. Il est impossible de créer un objet de ce type.
                                               // Il est possible d'avoir une variable d'un type abstrait "Compte unCompte;"
                                               // Il n'est pas possible de créer un simple compte: "unCompte = new Compte();"
    {
        #region        public Compte(string type, string prenom, string nom)// Constructeur qui crée un nouveau compte.
        /// <summary>
        /// Constructeur qui crée un nouveau compte.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        public Compte(string type, string prenom, string nom)
        {
            _type = type;
            _prenom = prenom;
            _nom = nom;
            _solde = 0;// Créer un nouveau compte revient à créer un compte avec un solde de zéro, puis de faire un dépôt.
            // Il doit donc y avoir une transaction de dépôt de sauvegardée dans le fichier de transaction lors de la création d'un compte.
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
                if (resultat != 0)
                {
                    return resultat;
                }
                resultat = _numeroDeCompte.CompareTo(that._numeroDeCompte);
                return resultat;
            }
        }
        #endregion

        #region        public virtual void Deposer(double montant)
        /// <summary>
        /// Cette méthode calcule le nouveau solde du compte
        /// </summary>
        /// <param name="montant">Seul le montant du dépôt est nécessaire.</param>
        public virtual void Deposer(double montant)
        // On peut laisser la méthode virtuelle, même si elle n'a pas besoin d'être redéfinie, un dépôt est pareil pour tous les types de comptes
        {
            _solde += montant;// Le solde précédent est augmenté du montant fourni.
        }
        #endregion

        #region        public virtual void Retirer(double montant)
        /// <summary>
        /// Cette méthode calcule le nouveau solde suite à un retrait. Le montant du retrait est soustrait du solde précédent.
        /// Cette méthode virtuelle ne s'applique qu'aux comptes chèques et épargne.
        /// Le comportement est différent selon le type de compte.
        /// Une méthode override redéfini Retirer dans la classe CompteCredit.
        /// Puisque le solde peut être négatif, mais jamais moins que la valeur négative de la limite de crédit.
        /// </summary>
        /// <param name="montant">Seul le montant du retrait est fourni.</param>
        public virtual void Retirer(double montant)
        {
            if (_solde - montant < 0)// Pour les « Comptes-Chèques » et les « Comptes d’épargne », le solde du compte ne peut jamais être négatif. 
            {
                throw new Exception("Erreur le retrait est trop important; il y a insuffisance de fonds.");
            }
            else
            {
                _solde -= montant;
            }
        }
        #endregion

        #region        public abstract void Sauvegarder(StreamWriter canalEcriture);
        /// <summary>
        /// La méthode abstraite n'a pas de définition.
        /// Chaque classe qui hérite de compte doit redéfinir (override) la méthode Sauvegarder.
        /// </summary>
        /// <param name="canalEcriture">Seul le canal d'écriture est passé en paramètre.</param>
        public abstract void Sauvegarder(StreamWriter canalEcriture);
        #endregion

        #region        public override string ToString()
        /// <summary>
        /// Méthode override qui est particulière aux comptes chèques et d'épargne.
        /// L'information du type de compte est ajoutée aux informations de base soit le numéro, nom et prénom.
        /// Un string builder est utilisé pour rassembler les informations sous la forme d'une chaîne de caractères.
        /// </summary>
        /// <returns>Une chaîne de caractères est retournée.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();// Un stringbuilder est déclaré et instancié.
            string type;
            switch (_type)// Selon dle type de compte, la chaîne de caractère prend une définition différente.
            {
                case "C":
                    {
                        type = "Chèques";
                    }
                    break;
                case "E":
                    {
                        type = "Épargne";
                    }
                    break;
                default: throw new Exception("Type de compte invalide");

            }
            sb.AppendFormat($"{Convert.ToString(_numeroDeCompte)}  {type}   {_nom}, {_prenom}");
            // Retourne une chaîne de format composite sous la forme d'une chaîne de caractères.
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// Les propriétés
        /// </summary>
        public string Type { get { return _type; } }
        public double Solde { get { return _solde; } }
        public int NumeroDeCompte { get { return _numeroDeCompte; } }
        // Pour que le numéro de compte soit accessible dans la banque, cette propriété est nécessaire.

        // Un attribut static est dit un "attribut de classe", par opposition à un attribut d'objet pour les attributs ordinaires
        // Tous les objets de la classe partage la même variable
        private static int _dernierNumero = 100; // Les numéros de compte débutent à 101 (ici 100 puisque incrémenté dans le constructeur.
        // Un numéro unique est assigné à chaque compte (indépendant de son type). Le premier compte créé par le programme porte le numéro 101.
        // Chaque compte créé par la suite porte le dernier nombre assigné, incrémenté de un. 

        // Le mot clé readonly est similaire à const, mais la valeur n'a pas besoin d'être connue à la déclaration
        // l'initialisation se fera dans le constructeur, puis la valeur ne pourra plus être modifiée
        // Permet de protéger le code d'erreurs éventuelles en indiquant que la valeur ne devrait pas changer
        protected readonly string _type;
        protected readonly string _prenom;
        protected readonly string _nom;
        protected double _solde;// Le solde du compte n'est pas conservé. Le montant initial est considéré comm un dépôt.
        // Numéro qui identifie le compte de manière unique
        protected readonly int _numeroDeCompte;
    }
}
