using System;
using System.Collections.Generic;
using System.Text;



namespace TPSynthese
{
    /// <summary>
    /// Classe principale du système de gestion bancaire.
    /// Gère les menus et l'exécution générale du programme.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Méthode principale du programme
        /// </summary>
        static void Main()
        {
            try
            {
                // Instantiation d'un objet de la classe courrante.
                Program p = new Program();
                // Appel de la méthode principale de l'objet Program.
                // En ayant un objet, les méthodes et attributs de la classe n'ont pas à être static
                p.Executer();
                Pause("Fin du programme");
            }
            catch (Exception e)
            {
                // On termine proprement le programme en cas d'erreur non gérée
                Pause("Fin du programme dûe à une exception: " + e.Message);
            }
        }


        /// <summary>
        /// Arrête l'exécution du programme pour permettre à l'utilisateur de lire la console.
        /// Attend une action de l'utilisateur avant de continuer.
        /// Doit être static car utilisée par la méthode <c>Main</c>
        /// </summary>
        /// <param name="s">Chaine de caractères optionnelle. Si elle est fournie, elle est affichée dans la console.</param>
        public static void Pause(string s = null)
        {
            Console.WriteLine();
            if (s != null)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("Appuyez sur une touche pour continuer");
            Console.ReadKey(true);
        }


        /// <summary>
        /// Constructeur
        /// </summary>
        private Program()
        {
            _laBanque = new Banque();  // TODO - Définition du constructeur de la classe Banque
        }
//*********************************************** %% **********************************************************

        /// <summary>
        /// Utilitaire pour afficher un titre dans le haut de la console
        /// </summary>
        /// <param name="titre">Le titre à afficher</param>
        private void AfficherTitre(string titre)
        {
            StringBuilder ligne = new StringBuilder();
            ligne.Insert(0, "=", 80);

            StringBuilder texte = new StringBuilder("= ");
            texte.Append(titre.PadRight(77));  // 77 = 80 - "= " au début (2) et "=" à la fin (1)
            texte.Append("=");

            Console.Clear();
            Console.WriteLine("{0}\n{1}\n{0}\n", ligne, texte);
        }


        /// <summary>
        /// Boucle principale du programme
        /// </summary>
        private void Executer()
        {
            while (true)
            {
                string choix = AfficherMenuPrincipal();
                switch (choix)
                {
                    case "O": OuvrirCompte(); break;
                    case "L": ListerComptes(); break;
                    case "A": DemanderCompte(); break;
                    case "Q":
                        return;
                    default:
                        Pause("Choix invalide");
                        break;
                }
            }
        }


        /// <summary>
        /// Affiche le menu principal utilisé par le méthode <c>Executer</c>
        /// </summary>
        /// <returns>Le choix de l'utilisateur en lettres majuscules</returns>
        private string AfficherMenuPrincipal()
        {
            AfficherTitre("Banque");
            Console.WriteLine(" O) Ouvrir un nouveau compte");
            Console.WriteLine(" L) Lister les comptes");
            Console.WriteLine(" A) Accéder à un compte");
            Console.WriteLine(" Q) Quitter");
            Console.Write("\n> ");
            return Console.ReadLine().ToUpper();
        }


        /// <summary>
        /// Option "O" du menu principal
        /// Crée un nouveau compte dans la banque
        /// </summary>
        private void OuvrirCompte()
        {
            AfficherTitre("Ouverture de compte");

            string type = DemanderType();

            // Les prénom et nom sont de simples chaines de caractères. Aucune validation n'est faite.
            Console.Write("Indiquez le prénom du propriétaire: ");
            string prenom = Console.ReadLine();
            Console.Write("Indiquez le nom du propriétaire: ");
            string nom = Console.ReadLine();

            double montant = 0;
            if (type != "R")
            {
                // Les comptes de crédit sont créés avec un solde initial de zéro
                montant = DemanderMontant("initial");
            }

            // 'AjouterCompte' retourne le numéro du nouveau compte créé
            int numero = _laBanque.AjouterCompte(type, prenom, nom, montant);  // TODO - Définition de la méthode 'AjouterCompte' dans la classe Banque
            Pause("Le compte " + numero + " a été ajouté");
        }
        //************************************************ %% *********************************************************


        /// <summary>
        /// Demande un type de compte voulu à l'utilisateur.
        /// Boucle et redemande tant que le type donné est invalide
        /// </summary>
        /// <returns>La chaine représentant le type choisi: "C", "E" ou "R"</returns>
        private string DemanderType()
        {
            while (true)
            {
                Console.Write("Indiquer le type de compte voulu, (C) Chèques, (E) Épargne, (R) cRédit: ");
                string ligne = Console.ReadLine().ToUpper();
                if (ligne == "C" || ligne == "E" || ligne == "R")
                {
                    return ligne;
                }
                Console.WriteLine("Choix invalide");
            }
        }


        /// <summary>
        /// Demande un montant à l'utilisateur
        /// Boucle et redemande tant que le montant donné est invalide
        /// </summary>
        /// <param name="titre">Une indication sur l'utilité du montant affiché dans la console</param>
        /// <returns></returns>
        private double DemanderMontant(string titre)
        {
            while (true)
            {
                try
                {
                    Console.Write("Indiquer le montant {0}: ", titre);
                    double montant = Convert.ToDouble(Console.ReadLine());
                    if (montant <= 0)
                    {
                        throw new Exception();
                    }
                    return montant;
                }
                catch
                {
                    Console.WriteLine("Montant invalide");
                }
            }
        }

        #region        private void ListerComptes()
        /// <summary>
        /// Option "L" du menu principal
        /// Affiche la liste de tous les comptes de la banque
        /// </summary>
        private void ListerComptes()
        {
            AfficherTitre("Liste des comptes");

            // Obtient la liste à afficher sous forme d'une liste de chaine de caractère
            List<string> liste = _laBanque.ListeDeComptes();  // TODO - Définition de la méthode 'ListeDeComptes' dans la classe Banque

//***************************************************** %% ****************************************************

            if (liste.Count == 0)
            {
                Pause("Aucun compte n'a encore été créé!");
                return;
            }

            foreach (var ligne in liste)
            {
                Console.WriteLine(ligne);
            }

            Pause();
        }
        #endregion

        #region        private void DemanderCompte()
        /// <summary>
        /// Option "A" du menu principal
        /// Demande le numéro du compte à l'utilisateur.
        /// Si le numéro est valide et correspond à un compte existant, continue à la méthode <c>AccederCompte</c>
        /// Sinon, se termine pour retourner au menu principal
        /// </summary>
        private void DemanderCompte()
        {
            Console.Write("\n\nNuméro du compte: ");
            string ligne = Console.ReadLine();

            try
            {
                int numeroCompte = Convert.ToInt32(ligne);
                // ValiderExistence va lancer un ArgumentException si le compte donné n'existe pas dans la banque
                _laBanque.ValiderExistence(numeroCompte);  // TODO - Définition de la méthode 'ValiderExistence' dans la classe Banque

//*************************************************** %% ******************************************************

                // Si aucune exception n'a été lancée, le compte existe.
                AccederCompte(numeroCompte);
            }
            catch (FormatException)
            {
                Pause("Numéro invalide");
            }
            catch (ArgumentException)
            {
                Pause("Ce compte n'existe pas");
            }
        }
        #endregion

        #region         private void AccederCompte(int numeroCompte)
        /// <summary>
        /// Suite de l'option "A" du menu principal
        /// Appellée par <c>DemanderCompte</c> une fois la validation faite
        /// Boucle dans le sous-menu tant que l'utilisateur ne choisi pas de retourner au menu principal
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte à accéder</param>
        private void AccederCompte(int numeroCompte)
        {
            while (true)
            {
                string choix = AfficherMenuCompte(numeroCompte);
                switch (choix)
                {
                    case "S": AfficherSolde(numeroCompte); break;
                    case "T": EffectuerTransaction(numeroCompte); break;
                    case "Q": return;
                    default:
                        Pause("Choix invalide");
                        break;
                }
            }
        }
        #endregion

        #region        private string AfficherMenuCompte(int numeroCompte)
        /// <summary>
        /// Affiche le menu utilisé par la méthode <c>AccederCompte</c>
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte auquel le programme accède actuellement</param>
        /// <returns>Le choix de l'utilisateur en lettres majuscules</returns>
        private string AfficherMenuCompte(int numeroCompte)
        {
            AfficherTitre("Compte " + numeroCompte);
            Console.WriteLine(" S) Afficher solde");
            Console.WriteLine(" T) Effectuer des transactions");
            Console.WriteLine(" Q) Retour au menu principal");
            Console.Write("\n> ");
            return Console.ReadLine().ToUpper();
        }
        #endregion

        #region        private void AfficherSolde(int numeroCompte)
        /// <summary>
        /// Option "S" du menu compte
        /// Affiche le solde du compte donné
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte duquel afficher le solde</param>
        private void AfficherSolde(int numeroCompte)
        {
            Console.WriteLine("\n\nSolde du compte: {0,12:C}", _laBanque.Solde(numeroCompte));  // TODO - Définition de la méthode 'Solde' dans la classe Banque
            Pause();
        }
        #endregion
        //************************************************** %% *******************************************************

        #region        private void EffectuerTransaction(int numeroCompte)
        /// <summary>
        /// Option "T" du menu compte
        /// Boucle dans le sous-menu tant que l'utilisateur ne choisi pas de retourner au menu du compte
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte sur lequel effectuer une transaction</param>
        private void EffectuerTransaction(int numeroCompte)
        {
            while (true)
            {
                string choix = AfficherMenuTransaction(numeroCompte);
                switch (choix)
                {
                    case "D": Deposer(numeroCompte); break;
                    case "R": Retirer(numeroCompte); break;
                    case "I": CalculerInterets(numeroCompte); break;
                    case "Q": return;
                    default:
                        Pause("Choix invalide");
                        break;
                }
            }
        }
        #endregion

        #region        private string AfficherMenuTransaction(int numeroCompte)
        /// <summary>
        /// Affiche le menu utilisé par le méthode <c>EffectuerTransaction</c>
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte auquel le programme accède actuellement</param>
        /// <returns>Le choix de l'utilisateur en lettres majuscules</returns>
        private string AfficherMenuTransaction(int numeroCompte)
        {
            Console.Clear();
            AfficherTitre("Transactions sur le compte " + numeroCompte);
            Console.WriteLine(" D) Effectuer un dépôt");
            Console.WriteLine(" R) Effectuer un retrait");
            Console.WriteLine(" I) Calculer les intérêts");
            Console.WriteLine(" Q) Retour au menu principal");
            Console.Write("\n> ");
            return Console.ReadLine().ToUpper();
        }
        #endregion

        #region        private void Deposer(int numeroCompte)
        /// <summary>
        /// Option "D" du menu transaction
        /// Effectue un dépôt dans le compte donné
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte dans lequel effectuer un dépôt</param>
        private void Deposer(int numeroCompte)
        {
            double montant = DemanderMontant("du dépôt");

            // 'Deposer' retourne le nouveau solde après le dépôt
            double solde = _laBanque.Deposer(numeroCompte, montant);  // TODO - Définition de la méthode 'Deposer' dans la classe Banque

//*************************************************** %% ******************************************************

            Console.WriteLine("\nDépôt effectué, nouveau solde du compte: {0,12:C}", solde);
            Pause();
        }
        #endregion

        #region        private void Retirer(int numeroCompte)
        /// <summary>
        /// Option "R" du menu transaction
        /// Tente d'effectuer un retrait du le compte donné
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte duquel effectuer un retrait</param>
        private void Retirer(int numeroCompte)
        {
            double montant = DemanderMontant("du retrait");
            // 'Retirer' dans la banque va lancer une exception si les fonds sont insuffisant pour le retrait demandé
            try
            {
                // 'Retirer' retourne le nouveau solde après le retrait
                double solde = _laBanque.Retirer(numeroCompte, montant);  // TODO - Définition de la méthode 'Retirer' dans la classe Banque

//*************************************************** %% ******************************************************

                Console.WriteLine("\nRetrait effectué, nouveau solde du compte: {0,12:C}", solde);
                Pause();
            }
            catch (Exception e)
            {
                Pause("Retrait impossible, " + e.Message);
            }
        }
        #endregion

        #region        private void CalculerInterets(int numeroCompte)
        /// <summary>
        /// Option "I" du menu transaction
        /// Affiche les intérêts calculés sur le compte donné
        /// </summary>
        /// <param name="numeroCompte">Le numéro du compte duquel effectuer un retrait</param>
        private void CalculerInterets(int numeroCompte)
        {
            // 'CalculerInterets' retourne le montant d'intérêts calculé pour le compte
            Console.WriteLine("\nIntérêts sur le compte: {0,12:C}", _laBanque.CalculerInterets(numeroCompte));  // TODO - Définition de la méthode 'CalculerInterets' dans la classe Banque
            Pause();
        }
        //********************************************** %%% ***********************************************************
        #endregion

        /// <summary>
        /// La banque qui contient tous les comptes et dans laquelle toute les opérations sont effectuées
        /// </summary>
        private Banque _laBanque;  // TODO - Définition de la classe Banque
    }
//************************************************* %% ********************************************************

}