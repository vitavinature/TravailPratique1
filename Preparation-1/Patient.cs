using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Patient : Personne
    {
        public Patient(string prenom, string nom, string matricule, string deces) : base(prenom, nom)
        {
            DateTime nonDecede = new DateTime(300, 1, 1);

            int numeroDuPatient = Convert.ToInt32(matricule);

            if (numeroDuPatient < 1000 || numeroDuPatient > 9999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");
            }
            DateTime dateDeces = Convert.ToDateTime(deces);

           /* if (dateDeces != nonDecede)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");

            }
            */

            _matricule = numeroDuPatient;
            _dateDeces = dateDeces;

        }


        public void Afficher()
        {
            Console.WriteLine($"{_prenom} {_nom} ({_matricule})");
        }

        private readonly int _matricule;
        private readonly DateTime _dateDeces = new DateTime();
    }

}
