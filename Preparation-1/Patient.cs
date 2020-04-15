using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Patient : Personne
    {
        public Patient(string prenom, string nom, string assMaladie, string matriculeMedecin, string deces) : base(prenom, nom)
        {
            DateTime nonDecede = new DateTime(300, 1, 1);

            int numeroDuPatient = Convert.ToInt32(assMaladie);

            if (numeroDuPatient < 1000 || numeroDuPatient > 9999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");
            }
            int numeroDuMedecin = Convert.ToInt32(matriculeMedecin);


            DateTime dateDeces = Convert.ToDateTime(deces);

           /* if (dateDeces != nonDecede)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");

            }
            */

            _assMaladie = numeroDuPatient;
            _matriculeMedecin = numeroDuMedecin;
            _dateDeces = dateDeces;

        }


        public void Afficher()
        {
            Console.Write($"{_matriculeMedecin} {_prenom} {_nom}, ");
            if (true)
            {

            }
        }

        private readonly int _assMaladie;
        protected readonly int _matriculeMedecin;
        private readonly DateTime _dateDeces = new DateTime();
    }

}
