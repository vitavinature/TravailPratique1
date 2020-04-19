using System;
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

        public void AjouterPatient(int patient)
        {
            if (_dateRetraite == _nonRetraite)
            {
                 _ListePatient.Add(patient);
            }
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
                Console.Write($"Nombre de patients: {_ListePatient.Count}");
            }
            Console.WriteLine();
        }

        public int _matricule;
        public DateTime _dateRetraite = new DateTime();
        public DateTime _nonRetraite = new DateTime();
        
        public List<int> _ListePatient = new List<int>();
    }

}
