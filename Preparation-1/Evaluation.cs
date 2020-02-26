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
                throw new Exception("Erreur: Pondération incorrecte");
            }

            _titre = titre;
            _ponderation = Convert.ToInt32(ponderation);
            _dateHeure = DateTime.Parse(dateHeure);

        }

        protected string _titre;
        protected int _ponderation;
        protected DateTime _dateHeure;
    }
}
