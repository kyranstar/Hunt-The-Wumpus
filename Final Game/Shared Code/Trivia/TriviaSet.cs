using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class TriviaSet
    {
        private int _questionCounter;
        private int _numberCorrect;
        public int NumberCorrect
        {
            get{return _numberCorrect;}
        }
        public int QuestionCounter
        {
            get { return _questionCounter; }
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
        public void IsQuestionCorrect(Question question, String PlayerAnswer)
        {
            if (question.IsCorrect(PlayerAnswer))
            {
                _numberCorrect++;
            }
        }
    }
}
