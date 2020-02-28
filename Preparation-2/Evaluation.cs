using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparation_2
{
    class Evaluation
    {
        public Evaluation(string titre, string ponderation, string date)
        {

            int ponder = Convert.ToInt32(ponderation);

            if (ponder < 0 || ponder > 100)
            {
                throw new Exception("La pondération doit être comprise entre 0 et 100.");
            }
            _date = DateTime.Parse(date);
            _titre = titre;
            _ponderation = ponder;



        }

        public void Afficher()
        {
            Console.WriteLine($"{_titre} ({_ponderation}%) {_date}");
        }

        protected readonly string _titre;
        protected readonly int _ponderation;
        protected readonly DateTime _date;

    }
}
