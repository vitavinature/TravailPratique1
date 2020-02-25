# Exercice formatif pour Examen 1
Renommez le fichier `Exemple` en `Exemple.exe`, il correspond au résultat attendu que vous devez reproduire. Il a été renommé pour ne pas être ignoré par Git.

Le fichier `Program.cs` contient une implémentation partielle du programme que vous devez compléter.

## But du programme
Le but est de lire le fichier `etudiant.txt` qui contient une liste d'évaluations d'un cours pour un étudiant. Pour chaque évaluation, le programme demande à l'utilisateur la note obtenue par l'étudiant. À la fin, il affiche la note finale de l'étudiant.

## Format du fichier
La toute première ligne contient l'identification d'un étudiant, puis sur les lignes suivantes les évaluations d'un cours suivi par l'étudiant.

Les lignes débutant par `#` sont des commentaires qui doivent être ignorées par le programme. Les lignes vides doivent aussi être ignorées. La toute première ligne ne doit pas être vide, ni être un commentaire. Le fichier founi contient des commentaires décrivant le format de chaque ligne.

Si une ligne ne respectant pas le format attendu est lue par le programme, un message d'erreur informe l'utilisateur et la ligne est ingorée.

## Déroulement du programme
Le programme ouvre le fichier `etudiant.txt` et lit la première ligne qui identifie l'étudiant et construit un objet `Etudiant`.

Il lit ensuite toutes les autres lignes, en ignorant les lignes vides et les lignes de commentaires, qui correspondent aux évaluations.

### Pour chaque ligne
Il existe 2 types d'évaluations: des examens et des tp (travaux pratiques). Un objet du bon type est créé (`Examen` ou `TP`) selon le type de l'évaluation. Puis les détails de l'évaluation sont affichés.

Le programme demande alors à l'utilisateur la note obtenue par l'étudiant. **Cette note doit être entre 0 et 100.** Si la note n'est pas valide, le programme affiche une erreur et la redemande.

Dans le cas d'un travail pratique, et uniquement dans ce cas, le programme demande aussi la date et l'heure à laquelle l'étudiant à remis le travail. Il calculera alors la pénalité imposée si le travail a été remis en retard, qui est de 10% par jour de retard. Si la date donnée n'est pas valide, le programme affiche une erreur et redemande la note et la date.
> Référez vous au commentaire à la fin du fichier Program.cs pour un exemple de manipulation et de calcul avec des dates.

Le programme calcule alors la portion de la note finale de la note obtenue pour l'évaluation.
```C#
noteFinale = (note * ponderation) / 100.0
```
> Par exemple, une note de 75% obtenue à une évaluation ayant une pondération de 20% donnera une note finale de 15 points.
La note est alors ajoutée à la note totale de l'étudiant.

Lorsque la fin du fichier est atteinte, le programme affiche la note totale de l'étudiant et se termine.

## Classes à définir
Votre programme doit définir les classes suivantes:
#### Personne
Une classe qui représente une personne et qui contient un *prénom* et un *nom*. Ces deux valeurs ne peuvent pas être modifiées après la création de la personne.  
Le constructeur reçoit en paramètre les valeurs du prénom et du nom.
#### Etudiant
Une classe qui représente un étudiant, qui hérite le la classe `Personne` et qui contient un *matricule* (nombre entier) et une *note totale* (nombre réel). Ces deux valeurs ne peuvent pas être directement modifiées de l'extérieur après la création de la personne.  
Le constructeur reçoit en paramètre les valeurs du prénom, du nom et du matricule **sous forme de chaines de caractères**, et valide que le matricule est bien un entier de 7 chiffres.

La classe fournit une méthode `Afficher` pour afficher les détails de l'étudiant, et une méthode `AjouterNote` pour ajouter la note d'une évaluation à la note totale de l'étudiant.
#### Evaluation
Une classe qui représente une évaluation et contient une *nom*, une *pondération* (nombre entier entre 0 et 100) et une date et heure (type DateTime).  Ces valeurs ne peuvent pas être modifiées après la création d'un objet de ce type.  
Le constructeur reçoit les valeurs en paramètre **sous forme de chaines de caractères** et s'assure de leur validité.

La classe fournit une méthode `Afficher` pour afficher les détails de l'évaluation.
#### Examen
Une classe qui représente un examen, qui hérite le la classe `Evaluation`.

La classe fournit une méthode `DemanderNote` qui demande la note obtenue à l'utilisateur, calcule la portion de la note finale basé sur la pondération de l'examen, et retourne cette valeur.
#### TP
Une classe qui représente un travail pratique, qui hérite le la classe `Evaluation`.

La classe fournit une méthode `DemanderNote` qui demande la note obtenue et la date de remise à l'utilisateur, calcule la pénalité de retard, calcule la portion de la note finale basé sur la pondération de l'examen, et retourne cette valeur.

---
## Remise
Ceci est un exercice non évalué.

Mais pour tester la méthode de remise, faites un commit et un push de votre dépôt à la fin.