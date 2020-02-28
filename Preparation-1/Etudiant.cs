﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Etudiant : Personne
    {
        public Etudiant(string prenom, string nom, string matricule) : base (prenom, nom)
        {
            _matricule = Convert.ToInt32(matricule);

            if (_matricule < 1000000 || _matricule > 9999999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule est en erreur");
            }
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
