using Preparation_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique1
{
    class Medecin : Personne
    {
        public Medecin(string prenom, string nom, int matricule, string retraite, ref int nombreMedecinActif) : base(prenom, nom)
        {
            //_nonRetraite = new DateTime(3000, 1, 1);
            _nonRetraite = DateTime.Parse("3000/1/1");

            //DateTime dateRetraite = Convert.ToDateTime(retraite);
            DateTime dateRetraite = DateTime.Parse(retraite);

            if (dateRetraite != _nonRetraite)
            {
                _dateRetraite = dateRetraite;
            }
            else
            {
                _dateRetraite = _nonRetraite;
            }

            _matricule = matricule;

            _nombreMedecinActif = nombreMedecinActif;
        }

        public void AjouterPatient(int patient)
        {
            if (_dateRetraite == _nonRetraite)
            {
                _ListePatient.Add(patient);
            }
        }

        public void EnleverPatient(int patient)
        {
            _ListePatient.Remove(patient);

        }
        public void Afficher()
        {
            Console.Write($"{_matricule} {_prenom} {_nom}, ");
            {
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
        }

        public int _matricule;
        public DateTime _dateRetraite = new DateTime();
        public DateTime _nonRetraite = new DateTime();
        public int _nombreMedecinActif;
        public List<int> _ListePatient = new List<int>();
    }

}
