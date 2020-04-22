using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Personne
    {
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;
        }

        public readonly string _prenom;
        public readonly string _nom;
    }
}





/*----------------------------------------------------------------------------------------------------------
#region         public Rectangle()
// Un CONSTRUCTEUR qui ne prend pas de PARAMÈTRES se nomme un CONSTRUCTEUR PAR DÉFAUT
// Si, et seulement si, aucun  constructeur n'est implicitement défini, un constructeur par défaut implicite existe
// Si un constructeur explicite est créé le constructeur implicite par défaut n'est pas généré
// Si nous voulons un constructeur par défaut, en plus d'autres constructeurs avec paramètres, il faut alors le définir explicitement
public Rectangle() //CONSTRUCTEUR PAR DÉFAUT
{
    _hauteur = 1;
    _largeur = 1;
    Console.WriteLine("Constructeur par défaut");
}
#endregion
//----------------------------------------------------------------------------------------------------------
#region         public int Hauteur()
//pour respecter le PRINCIPE D'ENCAPSULATION, AUCUN ATTRIBUT NE SERA PUBLIC,
// il faut donc fournir des MÉTHODES pour donner accès aux ATTRIBUTS
// Une MÉTHODE pour aller chercher la valeur d'un ATTRIBUT se nomme un ACCESSEUR
public int Hauteur() // MÉTHODE qui est un ACCESSEUR = une MÉTHODE qui va chercher la valeur d'un ATTRIBUT
{
    // Tout ce qu'un ACCESSEUR fait est de retourner la valeur d'un ATTRIBUT
    return _hauteur; // Voir la valeur définie (assignée) de _hauteur, à la fin du code
}
#endregion
//----------------------------------------------------------------------------------------------------------
#region         public void SetHauteur(int value)
// Une MÉTHODE qui permet de modifier un attribut se nomme un MUTATEUR
public void SetHauteur(int value) // MÉTHODE un MUTATEUR (celle-ci ne retourne aucune valeur)
{
    // Un MUTATEUR PERMET DE VALIDER les valeurs données avant de les assigner aux ATTRIBUTS
    // La hauteur ne peut pas être négative ni nulle
    if (value > 0)
    {
        _hauteur = value;
    }
}
#endregion
//----------------------------------------------------------------------------------------------------------
#region         public int PropLargeur      Une PROPRIÉTÉ (Lecture et Écriture)
// Une PROPRIÉTÉ est à mi-chemin entre un ATTRIBUT et une MÉTHODE
// S'utilise comme un attribut, se comporte comme une méthode
// PAS DE () POUR UNE PROPRIÉTÉ car CE N'EST PAS UNE MÉTHODE
public int PropLargeur // Une PROPRIÉTÉ (Lecture et Écriture) (PropLargeur n'est pas une variable)
{ // Bloc d'instruction
  // Dans le bloc d'une PROPRIÉTÉ, il est possible de définir 2 SIMILI-MÉTHODES
    get // SIMILI-MÉTHODE (ACCESSEUR) get est le nom de l'accesseur, le mot clé
    {
        return _largeur; // _largeur est un attribut
    }
    set // SIMILI-MÉTHODE (MUTATEUR), "c'est comme si nous avions (int value), c'est implicite"
        // set est un mot clé du langage. Il inclut value, un autre mot clé
    {
        // Dans la partie set d'une PROPRIÉTÉ, une variable nommée "value" existe pour contenir la valeur assignée
        if (value > 0) // Validation de la valeur
                       // value c'est aussi un mot clé du langage
        {
            _largeur = value; // Assignation
        }
    }
}
#endregion
//----------------------------------------------------------------------------------------------------------
#region         public string Nom { get; set; } = "Rectangle";  PROPRIÉTÉ AUTOMATIQUE
// PROPRIÉTÉ AUTOMATIQUE
// Une VALEUR INTERNE est UTILISÉE IMPLICITEMENT, SANS qu'on doive DÉFINIR UN ATTRIBUT _nom
// Le "get" RETOURNE simplement cette VALEUR, le "set" l'ASSIGNE DIRECTEMENT SANS VALIDATION
// Il est possible d'assigner une valeur par défaut à la PROPRIÉTÉ (dans le cas présent = "Rectangle")
// C'est une très légère COUCHE AU DESSUS D'UN ATTRIBUT afin que ce dernier reste privé
// À utiliser avec modération
public string Nom { get; set; } = "Rectangle";

// PROPRIÉTÉ AUTOMATIQUE
// Racourci que le langage donne. Toujours se demander pourquoi je fais ça
// Il y aura des cas où ce sera ok de le faire.
// Quand nous utilisons un MUTATEUR, il faut se demander pourquoi donne t'on un accès direct à un utilisateur ?
#endregion
//----------------------------------------------------------------------------------------------------------
#region         public int PropValeur     PROPRIÉTÉ EN LECTURE SEULE
public int PropValeur // PROPRIÉTÉ EN LECTURE SEULE
{
    get
    { return _valeurDeTest; }
    // On ne définit pas la méthode set
}
#endregion
//----------------------------------------------------------------------------------------------------------
#region public int Couleur { get; private set; } = 15;  PROPRIÉTÉ AUTOMATIQUE, EN LECTURE SEULE

// PROPRIÉTÉ AUTOMATIQUE, en lecture seule de l'extérieur, mais modifiable dans la classe
public int Couleur { get; private set; } = 15; // PROPRIÉTÉ AUTOMATIQUE, EN LECTURE SEULE
                                               // Assigné avec une valeur par défaut 15
                                               // seul ce n'est pas utile
#endregion

// Les deux prochaines méthodes donnent le même résultat
// Joël préfère la deuxième façon

#region public int Aire() MÉTHODE en lecture seule
// Une MÉTHODE qui retourne l'aire du rectangle
public int Aire() // MÉTHODE en lecture seule
{
    return _hauteur * _largeur;
}
#endregion

// bloc =  méthode (get et set)
// wrench = propriété (que le get) ou un attribut

#region public int Surface   PROPRIÉTÉ accesseur uniquement en lecture seule, utilisé comme une variable
// PROPRIÉTÉ pour récupérer l'aire d'un rectangle
public int Surface // PROPRIÉTÉ accesseur uniquement en lecture seule, utilisé comme une variable
{
    get
    {
        return _hauteur * _largeur;
        // On ne définit pas la méthode set
    }
}
#endregion
//----------------------------------------------------------------------------------------------------------
*/
