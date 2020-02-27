using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_1
{
    class Evaluation
    {
        public Evaluation(string titre, string ponderation, string dateHeure)
        {
            if (Convert.ToInt32(ponderation) < 0 || Convert.ToInt32(ponderation) > 100)
            {
                throw new Exception("Pondération invalide, doit être entre 0 et 100");
            }

            _titre = titre;
            _ponderation = Convert.ToInt32(ponderation);
            _dateHeure = DateTime.Parse(dateHeure);

        }
        public void Afficher()
        {
            Console.WriteLine($"{_titre} ({_ponderation}%) {_dateHeure}");
        }

        private string _titre;
        protected int _ponderation;
        protected DateTime _dateHeure;
    }
}
