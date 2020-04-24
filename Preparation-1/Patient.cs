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
        public void Afficher2()
        {
            Console.WriteLine($"{_assMaladie} {_prenom} {_nom}");

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
        public int AssMaladie { get { return _assMaladie; } }
        public int MatriculeMedecin { get { return _matriculeMedecin; } }
        public DateTime DateDeces { get { return _dateDeces; } }
        public DateTime NonDecede { get { return _nonDecede; } }

        

        private  int _assMaladie;
        private int _matriculeMedecin;
        private DateTime _dateDeces = new DateTime();
        private readonly DateTime _nonDecede = new DateTime(3000,1,1);
    }
}
