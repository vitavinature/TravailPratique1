using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Patient : Personne
    {
        public Patient(string prenom, string nom, int assMaladie, string matriculeMedecin, string deces) : base(prenom, nom)
        {
            _assMaladie = assMaladie;

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
        public int _matriculeMedecin;
        public DateTime _dateDeces = new DateTime();
        public readonly DateTime _nonDecede = new DateTime(3000, 1, 1);
    }
}
