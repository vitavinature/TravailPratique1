﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Medecin : Personne
    {
        public Medecin(string prenom, string nom, string matricule, string retraite) : base(prenom, nom)
        {
            _nonRetraite = new DateTime(3000, 1, 1);
            int numeroDuMedecin = Convert.ToInt32(matricule);

            if (numeroDuMedecin < 100 || numeroDuMedecin > 999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du médecin est en erreur");
            }
            DateTime dateRetraite = Convert.ToDateTime(retraite);

            if (dateRetraite != _nonRetraite)
            {
                _dateRetraite = dateRetraite;
            }
            else
            {
                _dateRetraite = _nonRetraite;
            }

            _matricule = numeroDuMedecin;
        }

        public void AjouterMedecin(ref List<Medecin> Medecins)
        {
            Console.WriteLine("Ajouter d'un médecin");
            Console.WriteLine("--------------------");
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            List<string> donnees = new List<string>();
            donnees.Add(prenom);
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            donnees.Add(nom);
            Console.Write("Code d'identification: ");
            string code = Console.ReadLine();
            int idCode = Convert.ToInt32(code);
           
            foreach (Medecin item in Medecins)
            {
                if (idCode == item._matricule)
                {

                }
            }
            donnees.Add(nom);
        }
        public void Afficher()
        {
            Console.Write($"{_matricule} {_prenom} {_nom}, ");
            if (_dateRetraite != _nonRetraite)

            {
                Console.Write("Retraité");
            }
            else
            {
                Console.Write($"Nombre de patients: {_nombreDePatients}");
            }
            Console.WriteLine();
        }

        private readonly int _matricule;
        protected readonly DateTime _dateRetraite = new DateTime();
        protected readonly DateTime _nonRetraite = new DateTime();
        protected readonly int _nombreDePatients = 0;
    }

}
