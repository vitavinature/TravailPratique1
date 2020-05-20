using System;
using System.IO;
using System.Collections.Generic;

namespace TPSynthese
{
    /// <summary>
    /// La classe Banque c'est le système de gestion des comptes, des transactions, de la lecture et l'écriture des fichiers des comptes et transactions.
    /// </summary>
    class Banque : Object //Classe de base de toutes les classes définies dans ce programme.
                          // Il n'est pas nécessaire d'indiquer l'héritage de la classe Objet puisqu'elle est implicite.
                          // J'ai décidé de l'indiquer pour mieux comprendre.
    {
        public Banque()
        {
            _listeDesComptes = new List<Compte>();// l'objet est créé. De la mémoire est allouée. L'instanciation est commencée.
                                                  // À partir d'ici la variable peut être utilisée, elle existe, car elle a été créé avec l'instruction "new"
                                                  // Une fois initialisée, l'instanciation aura été complétée.
            #region Lecture du fichier des comptes
            try
            {
                string fichierComptes = "comptes.txt";
                // Ouverture du canalRead pour l'accès au fichier "comptes.txt"
                using (StreamReader canalRead = new StreamReader(fichierComptes))
                {
                    // Lit la première ligne qui identifie le compte
                    string ligne = canalRead.ReadLine();
                    int numeroCompte = 0;

                    while (ligne != null)
                    {
                        try
                        {
                            List<string> donnees = new List<string>(ligne.Split(';'));
                            if (donnees.Count >= 4)
                            {
                                // Extrait les valeurs individuelles de la ligne

                                string type = donnees[0];
                                string numero = donnees[1];

                                string prenom = donnees[2];
                                string nom = donnees[3];
                                string typeComptePossible = "ECR";
                                int limiteCredit = 0;

                                if (donnees.Count < 4)
                                {
                                    throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                                }

                                if (donnees.Count == 5)
                                {
                                    limiteCredit = Convert.ToInt32(donnees[4]);
                                }
                                if (donnees.Count > 5)
                                {
                                    throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                                }

                                if (donnees[0].Length > 1 || !typeComptePossible.Contains(donnees[0]))
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide; le type de compte n'est pas conforme.");
                                }
                                if (donnees[1].Length < 3)
                                {
                                    throw new Exception("Erreur, le numéro de compte est invalide.");
                                }

                                numeroCompte = Convert.ToInt32(donnees[1]);

                                if (numeroCompte < 101)
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide; le numéro de compte est en erreur");
                                }

                                foreach (Compte item in _listeDesComptes)
                                {
                                    if (item.NumeroDeCompte == numeroCompte)
                                    {
                                        throw new Exception("Erreur le fichier n'est pas valide, il y a deux numéro de comptes identiques.");
                                    }
                                }

                                switch (type)
                                {
                                    case "C": _listeDesComptes.Add(new CompteCheque(type, prenom, nom, numeroCompte)); break;
                                    case "E": _listeDesComptes.Add(new CompteEpargne(type, prenom, nom, numeroCompte)); break;
                                    case "R": _listeDesComptes.Add(new CompteCredit(type, prenom, nom, limiteCredit, numeroCompte)); break;
                                    default: throw new Exception("Type de compte invalide");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // On ignore silencieusement les erreurs, on passe à la ligne suivante
                        }

                        ligne = canalRead.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier comptes.txt sera créé");
                Program.Pause();
            }
            #endregion

            _listeDesComptes.Sort();// Sort() ici réfère à ICompare qui est associé à la classe Compte. ICompare redéfinit la façon que le Sort() fonctionne.

            #region Lecture du fichier des transactions
            try
            {
                string fichierTransactions = "transactions.txt";
                // Ouverture du canalRead pour l'accès au fichier "transactions.txt"
                using (StreamReader canalRead = new StreamReader(fichierTransactions))
                {
                    // Lit la première ligne qui identifie la transaction
                    string ligne = canalRead.ReadLine();
                    int numeroCompte = 0;// Déclaration et initialisation d'une variable de travail.

                    while (ligne != null)// Tant que la ligne lue n'est pas nulle
                    {
                        try
                        {
                            List<string> donnees = new List<string>(ligne.Split(';')); // Extrait les valeurs individuelles de la ligne
                            // en séparant l'information de la ligne à chaque ; et mets ces informations dans des cellules différentes de la liste.
                            if (donnees.Count < 4)// Si le nombre d'éléments de la liste est inférieur à 4, il y a erreur.
                            {
                                throw new Exception("Erreur: Le fichier contient une ligne où il manque une information.");
                            }
                            if (donnees.Count > 4)// Si le nombre d'éléments de la liste est supérieur à 5, il y a erreur.
                            {
                                throw new Exception("Erreur: Le fichier contient une ligne qui a trop d'information.");
                            }
                            if (donnees.Count == 4)// Si le nombre d'éléments de la liste est égal à 4
                                // Cette ligne est probablement redondante, après avoir passé les deux conditions précédentes il est implicite que cette condition est respectée.
                            {
                                string type = donnees[1];// Déclaration et initialisation des variables de travail.
                                double montant = Convert.ToDouble(donnees[2]);
                                string date = donnees[3];
                                string typeTransactionPossible = "DR";
                                numeroCompte = Convert.ToInt32(donnees[0]);
                                if (numeroCompte < 101)// Le plus petit numéro de compte possible c'est 101
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide; le numéro de compte est en erreur");
                                }
                                if (type.Length > 1 || !typeTransactionPossible.Contains(type))// 
                                {
                                    throw new Exception("Erreur le fichier n'est pas valide; le type de transaction possible n'est pas conforme.");
                                }
                                switch (type)// Selon le type de transaction
                                {
                                    case "D": Deposer(numeroCompte, montant, false); break;
                                    // nouvelleTransaction est true pour les opérations de l'utilisateur, est false pour la lecture du fichier
                                    case "R": Retirer(numeroCompte, montant, false); break;
                                    default: throw new Exception("Type de transaction invalide");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // On ignore silencieusement les erreurs, on passe à la ligne suivante
                        }
                        ligne = canalRead.ReadLine(); // Lecture de la ligne suivante dans le fichier
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Un fichier transactions.txt sera créé");
                Program.Pause();
            }
            #endregion
        }

        #region public int AjouterCompte(string type, string prenom, string nom, double montant)
        /// <summary>
        /// Méthode pour l'ajout de comptes banquaires de différents types: Cheques, Épargne, Crédit
        /// </summary>
        /// <param name="type">type de compte: C pour CompteCheque, E pour CompteEpargne et R pour CompteCredit</param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="montant">montant positif initial, 0 pour les CompteCredit</param>
        /// <returns>Retourne le numéro de compte du nouveau compte ajouté</returns>
        public int AjouterCompte(string type, string prenom, string nom, double montant)
        {
            int min = 500;// Pour les « Comptes-Crédit », pour simuler l’enquête de crédit qu’une vraie banque ferait à l’ouverture,
            //le programme génère un nombre aléatoire, multiple de 100$, entre 500$ et 3000$, qui sera la limite de crédit du compte. 
            int max = 3000;
            int multiple = 100;

            Compte nouveauCompte;// Déclaration d'un nouvel objet Compte
            switch (type)// Tout dépendant du type de compte, un constructeur spécifique est utilisé
            {
                case "C": nouveauCompte = new CompteCheque(type, prenom, nom); break;
                case "E": nouveauCompte = new CompteEpargne(type, prenom, nom); break;
                case "R":
                    {
                        int limiteCredit = _generateurAleatoire.Next(min / multiple, max / multiple + 1) * multiple;
                        // Une limite de crédit aléatoire est généré. (Voir information plus haut concernant les variables.)
                        nouveauCompte = new CompteCredit(type, prenom, nom, limiteCredit);// Constructeur pour l'ajout d'un compte de crédit.
                    }
                    break;
                default: throw new Exception("Type de compte invalide");
            }

            _listeDesComptes.Add(nouveauCompte);

            if (montant > 0)
            {
                // Faire une transaction de dépôt dans le compte du montant initial
                Deposer(nouveauCompte.NumeroDeCompte, montant);// Le montant initial entré lors de l'ouverture du compte, (0 pour les comptes de crédits)
                // est déposé vers les transactions pour être écrit (enregistré) dans le fichier transaction.txt. 
            }
            SauvegarderCompte(nouveauCompte);// À chaque ajout d'un nouveau compte, les données du compte sont écrite(enregistré) dans le fichier comptes.txt

            return nouveauCompte.NumeroDeCompte;// La propriété NumeroDeCompte est défini public dans Compte pour être accessible dans Banque
            // La méthode retourne le numéro du nouveau compte ouvert.
        }
        #endregion

        #region         public double CalculerInterets(int numeroCompte)
        /// <summary>
        /// Cette méthode calcule le montant d'intérêt courant du compte. Le solde est inchangé par cette opération.
        /// Le taux d'intérêt dépend du type de compte. Concernant les comptes crédits, le taux varie selon le solde du compte. 
        /// </summary>
        /// <param name="numeroCompte">Seul le numéro de compte est nécessaire pour cette opération</param>
        /// <returns>Le taux d'intérêt est retourné.</returns>
        public double CalculerInterets(int numeroCompte)
        {
            double interet = 0;// Déclaration et initialisation de la variable de travail.
            foreach (Compte item in _listeDesComptes)// Pour chaque compte de la liste des comptes
            {
                if (item.NumeroDeCompte == numeroCompte)// Si le numéro fourni est égal à un des numéros de la liste
                {
                    if (item.Type == "C")// S'il est du type chèques 
                    {
                        interet = 0.001 * item.Solde;// Le taux d'intérêt est de 0,1% et le montant de l'intérêt est calculé en conséquence.
                    }
                    if (item.Type == "E")// S'il est du type épargne
                    {
                        interet = 0.01 * item.Solde;// Le taux d'intérêt est de 1,0% et le montant de l'intérêt est calculé en conséquence.
                    }
                    if (item.Type == "R")// S'il est du type crédit
                    {
                        if (item.Solde < 0)//Quand le solde est négatif
                        {
                            interet = 0.045 * item.Solde;// Le taux d'intérêt est de 4,5% et le montant de l'intérêt est calculé en conséquence.
                        }
                        else//Quand le solde est positif
                        {
                            interet = 0;// Le montant de l'intérêt est 0.
                        }
                    }
                }
            }
            return interet;// Retourne le  montant de intérêt.
        }
        #endregion

        #region public double Deposer(int numeroCompte, double montant)
        /// <summary>
        /// Méthode qui transmet l'information du dépot au constructeur de la classe Dépot
        /// De même qu'elle transmet le dépot pour la sauvegarde dans le fichier transactions.txt, lorsqu'il s'agit d'une nouvelle transaction.
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        /// <param name="nouvelleTransaction">Booléen qui dirige la sauvegarde vers le fichier transactions.txt lorsque true.</param>
        /// <returns></returns>
        public double Deposer(int numeroCompte, double montant, bool nouvelleTransaction = true) 
            // nouvelleTransaction est true pour les opérations de l'utilisateur, est false pour la lecture du fichier
        {
            foreach (Compte item in _listeDesComptes)
            {
                if (item.NumeroDeCompte == numeroCompte)
                {
                    if (nouvelleTransaction)// S'il s'agit d'une nouvelle transaction, le constructeur Depot est appelé et la transaction sauvegardée dans le fichier des transactions.
                    {
                        Depot d = new Depot(numeroCompte, montant);
                        SauvegarderTransaction(d);
                    }
                    item.Deposer(montant);// Le comportement est le même pour tous les types de comptes, le montant fourni est additionné au solde actuel du compte. 
                    return item.Solde;// Le nouveau solde est retourné.
                }
            }
            throw new Exception("Le compte n'existe pas");// Si le numéro de compte n'existe pas ce message est affiché.
            // Cette ligne n'est sûrement pas nécessaire puisque l'existence du compte a été vérifiée avant que le menu de transactions ne soit offert à l'utilisateur.
        }
        #endregion

        #region public List<string> ListeDeComptes()
        /// <summary>
        /// Cette option permet d’afficher la liste des comptes créés et gérés par la banque. 
        /// Si aucun compte n’existe, un message en informe l’utilisateur.
        /// </summary>
        /// <returns>Retoure la liste qui indique le numéro du compte, son type, le nom et le prénom du propriétaire.
        /// Dans le cas des « Comptes-Crédit », la limite de crédit est affichée.  </returns>
        public List<string> ListeDeComptes()
        {
            List<string> liste = new List<string>();// Une liste de type string est déclarée et instanciée (la place en mémoire est alouée).
            _listeDesComptes.Sort();
            // La liste est triée par ordre alphabétique des noms. Pour les noms identiques, le tri est fait sur les prénoms.
            // Si la même personne (nom et prénom) possède plus d’un compte, le tri est fait sur le numéro du compte. 

            foreach (var item in _listeDesComptes)
            {
                liste.Add(item.ToString());
            }
            return liste;
        }
        #endregion

        #region public double Retirer(int numeroCompte, double montant, bool nouvelleTransaction = true)
        /// <summary>
        /// Méthode qui transmet l'information du retrait au constructeur de la classe Retrait
        /// De même qu'elle transmet le retrait pour la sauvegarde dans le fichier transactions.txt, lorsqu'il s'agit d'une nouvelle transaction.
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="montant"></param>
        /// <param name="nouvelleTransaction">Booléen qui dirige la sauvegarde vers le fichier transactions.txt lorsque true.</param>
        /// <returns></returns>
        public double Retirer(int numeroCompte, double montant, bool nouvelleTransaction = true)
        {
            foreach (Compte item in _listeDesComptes)
            {
                if (item.NumeroDeCompte == numeroCompte)
                {
                    if (nouvelleTransaction == true)// S'il s'agit d'une nouvelle transaction, le constructeur Retrait est appelé et la transaction sauvegardée dans le fichier des transactions.
                    {
                        // TODO
                        Retrait r = new Retrait(numeroCompte, montant);
                        SauvegarderTransaction(r);
                    }
                    item.Retirer(montant);// Le comportement est le même pour tous les types de comptes, le montant fourni est soustrait au solde actuel du compte. 
                    return item.Solde;// Le nouveau solde est retourné.
                }
            }
            throw new Exception("Le compte n'existe pas");// Si le numéro de compte n'existe pas ce message est affiché.
            // Cette ligne n'est sûrement pas nécessaire puisque l'existence du compte a été vérifiée avant que le menu de transactions ne soit offert à l'utilisateur.
        }
        #endregion

        #region        private void SauvegarderCompte(Compte nouveauCompte)
        /// <summary>
        /// Méthode qui appele la méthode de sauvegarde qui transmet le canal d'écriture pour l'enregistrement du nouveau compte ouvert, dans le fichier comptes.txt.
        /// </summary>
        /// <param name="nouveaucompte"></param>
        private void SauvegarderCompte(Compte nouveaucompte)
        {
            string fichier = "comptes.txt";//Déclaration et initialisation de la variable fichier.
            // Ouverture du canalEcriture pour l'écriture dans le fichier "comptes.txt"
            using (StreamWriter canalEcriture = new StreamWriter(fichier, true))// true est utilisé pour que le nouveau compte soit ajouté aux comptes existants.
            {
                nouveaucompte.Sauvegarder(canalEcriture);
            }
        }
        #endregion

        #region        public void SauvegarderTransaction(Transaction transaction)
        /// <summary>
        /// Méthode qui transmet le canal d'écriture pour l'écriture (la sauvegarder) d'une transaction dans le fichier transaction.txt
        /// C'est l'appel de la méthode abstraite Sauvegarder.
        /// </summary>
        /// <param name="transaction"></param>
        public void SauvegarderTransaction(Transaction transaction)
        {
            string fichier = "transactions.txt";
            // Ouverture du canalEcriture pour l'écriture dans le fichier "transactions.txt"
            using (StreamWriter canalEcriture = new StreamWriter(fichier, true))// true est utilisé pour que la transaction soit ajoutée aux transactions existantes.
            {
                transaction.Sauvegarder(canalEcriture);
            }
        }
        #endregion

        #region public double Solde(int numeroCompte)
        /// <summary>
        /// Cette méthode retourne le solde actuel du compte. 
        /// </summary>
        /// <param name="numeroCompte">Seul le numéro de compte est nécessaire.</param>
        /// <returns>Le solde du compte est retourné.</returns>
        public double Solde(int numeroCompte)
        {
            foreach (Compte item in _listeDesComptes)// La liste des comptes est déroulée ..
            {
                if (item.NumeroDeCompte == numeroCompte)// jusqu'à ce que le numéro de compte désiré soit trouvé
                {
                    return item.Solde;// Alors le solde est retourné.
                }
            }
            throw new Exception("Le compte n'existe pas");// Sinon un message d'erreur est retourné.
            // Par contre, cette ligne n'a probablement pas sa raison d'être, puisqu'avant que le menu du compte, (dont fait parti Afficher Solde,)
            // ne soit afficher, l'existence du numéro du compte a été valider. 
        }
        #endregion

        #region public void ValiderExistence(int numeroCompte)
        /// <summary>
        /// Pour que le programme affiche le menu du compte, il doit avant valider que le compte existe. La présente méthode vérifie que le numéro de compte existe.
        /// Si le numéro ne fait pas parti de la liste des comptes, un message d’erreur informe l’utilisateur et le programme retourne au menu principal .
        /// </summary>
        /// <param name="numeroCompte">Un numéro de compte doit être fourni.</param>
        public void ValiderExistence(int numeroCompte)
        {
            bool existe = false;
            foreach (Compte item in _listeDesComptes)// passe la liste des comptes et compare le numéro de compte fourni
            {
                if (item.NumeroDeCompte == numeroCompte)
                {
                    existe = true;// Oui, un numéro de la liste existe.
                }
            }
            if (existe == false)// Si la valeur de existe est toujours à false, c'est qu'aucun compte du numéro dommé n'existe.
            {
                throw new Exception($"Erreur: Le compte numéro {numeroCompte} n'existe pas.");// Alors un message d'erreur est affiché.
            }
        }
        #endregion

        private readonly List<Compte> _listeDesComptes;// Ici le nom de la variable (attribut) est défini. 
                                              // À ce stade ci: - il n'y a pas encore de mémoire d'allouée dans l'ordinateur pour cette variable. L'allocation n'a pas encore été faite.
                                              //                - l'objet n'a pas non plus été initialisé, donc ne peut être utilisé tant que son constructeur n'est pas appelé.
                                              
        private readonly static Random _generateurAleatoire = new Random();// Attribut utilisé pour la génération de limites de crédit aléatoires.
    }
}
