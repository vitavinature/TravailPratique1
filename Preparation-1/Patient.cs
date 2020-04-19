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
            int numeroAssMaladie = Convert.ToInt32(assMaladie);

            if (numeroAssMaladie < 1000 || numeroAssMaladie > 9999)
            {
                throw new Exception("Erreur le fichier n'est pas valide; le numéro d'assurance maladie du patient est en erreur");
            }
            else
            {
                _assMaladie = numeroAssMaladie;
            }

            _matriculeMedecin = Convert.ToInt32(matriculeMedecin);

            DateTime dateDeces = Convert.ToDateTime(deces);

            if (dateDeces != _nonDecede)
            {
                _dateDeces = dateDeces;

            }
            else
            {
                _dateDeces = _nonDecede;
            }


        }


        public void Afficher()
        {

            Console.Write($"{_assMaladie} {_prenom} {_nom}, ");
            if (_dateDeces != _nonDecede)
            {
                Console.Write("Décédé");
            }
            else
            {
                Console.Write($"Medecin: {_matriculeMedecin} ");
            }
        }

        public readonly int _assMaladie;
        public readonly int _matriculeMedecin;
        private readonly DateTime _dateDeces = new DateTime();
        protected readonly DateTime _nonDecede = new DateTime(3000, 1, 1);
    }

}
