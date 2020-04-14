using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Medecin : Personne
    {
        public Medecin(string prenom, string nom, string matricule, string retraite) : base (prenom, nom)
        {
            DateTime nonRetraite = new DateTime(3000,1,1);
            int numeroDuMedecin = Convert.ToInt32(matricule);

            if (numeroDuMedecin < 100 || numeroDuMedecin > 999)         
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du médecin est en erreur");
            }
            DateTime dateRetraite = Convert.ToDateTime(retraite);

            if (dateRetraite != nonRetraite)
            {
                _dateRetraite = nonRetraite;
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");

            }
            else
            {
                _dateRetraite = dateRetraite;

            }

            _matricule = numeroDuMedecin;
        }

        public void Afficher()
        {
            Console.WriteLine($"{_prenom} {_nom} ({_matricule})");
        }

        private readonly int _matricule;
        protected readonly DateTime _dateRetraite = new DateTime();
    }

}
