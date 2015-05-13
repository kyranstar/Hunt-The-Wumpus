using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Trivia
{
    class TriviaSet
    {
        private int _counter;

        public int Counter
        {
            get { return _counter; }
        }
        private List<Question> _qlist;
        public List<Question> QList
        {
            get { return _qlist; }
        }

        public TriviaSet(List<Question> QList)
        {
            _qlist = QList;
        }
    }
}
