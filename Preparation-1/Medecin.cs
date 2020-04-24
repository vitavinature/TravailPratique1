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

        public void AjouterPatient(Patient patient)
        {
            if (_dateRetraite == _nonRetraite)
            {
                _ListePatient.Add(patient);
            }
        }

        public void EnleverPatient(Patient patient)
        {
            _ListePatient.Remove(patient);

        }
        public void Afficher2()
        {
            Console.WriteLine();
            Console.WriteLine("Medecin");
            Console.WriteLine("-------");
            Console.WriteLine($"Code d'identification: {_matricule}");
            Console.WriteLine($"Nom: {_prenom} {_nom}");
            if (_ListePatient.Count > 0)
            {
                Console.WriteLine("Patients:");
                foreach (Patient itemPatient in _ListePatient)
                {
                        itemPatient.Afficher2();
                }
            }
            else
            {
                if (_dateRetraite == _nonRetraite)
                {
                    Console.WriteLine("Aucun patients");
                }
                else
                {
                    Console.WriteLine($"Retraité le {_dateRetraite.ToLongDateString()}");
                }
            }
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
        public int Matricule { get { return _matricule; } }
        public DateTime DateRetraite { get { return _dateRetraite; } }
        public DateTime NonRetraite { get { return _nonRetraite; } }
        public int NombreMedecinActif { get { return _nombreMedecinActif; } }

        public List<Patient> ListePatient { get { return _ListePatient; } }

        private int _matricule;
        private DateTime _dateRetraite = new DateTime();
        private readonly DateTime _nonRetraite = new DateTime(3000,1,1);
        private int _nombreMedecinActif;
        private List<Patient> _ListePatient = new List<Patient>();
    }

}
