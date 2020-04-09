﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Medicin : Personne
    {
        public Medicin(string prenom, string nom, string matricule) : base (prenom, nom)
        {
            int numeroDuMedecin = Convert.ToInt32(matricule);

            if (numeroDuMedecin < 1000000 || numeroDuMedecin > 9999999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule est en erreur");
            }

            _matricule = numeroDuMedecin;
        }

        public void AjouterNote(double note)
        {
            if (_noteTotale + note > 100)
            {
                throw new Exception("Note totale trop grande");
            }
            _noteTotale += note;
        }
 
        public void Afficher()
        {
            Console.WriteLine($"{_prenom} {_nom} ({_matricule}), Note =  {_noteTotale}");
        }

        private readonly int _matricule;
        private double _noteTotale;
    }

}
