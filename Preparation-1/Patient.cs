using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Patient : Personne
    {
        public Patient(string prenom, string nom, string matricule, string deces) : base (prenom, nom)
        {
            int numeroDuPatient = Convert.ToInt32(matricule);

            if (numeroDuPatient < 1000 || numeroDuPatient > 9999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");
            }

            DateTime dateDeces = Convert.ToDateTime(deces);
            if (dateDeces = "2050-12-31")
            {
                throw new Exception("Erreur le fichier n'est pas valide; le matricule du patient est en erreur");

            }

            _matricule = numeroDuPatient;

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
