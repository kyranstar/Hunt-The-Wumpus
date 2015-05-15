using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class TriviaSet
    {
        public delegate void QuestionUpdateHandler(object sender, EventArgs e);
        public event QuestionUpdateHandler OnNewQuestion;

        private int _currentQuestionIndex;
        private int _numberCorrect;
        public int NumberCorrect
        {
            get{ return _numberCorrect; }
        }
        public int QuestionCounter
        {
            get { return _currentQuestionIndex; }
        }
        private List<Question> _qlist;

        public List<Question> QList
        {
            get { return _qlist; }
        }

        public Question CurrentQuestion
        {
            get
            {
                if (IsComplete)
                    return null;

                return _qlist[_currentQuestionIndex];
            }
        }

        public bool IsComplete
        {
            get
            {
                return _currentQuestionIndex < 0 || _currentQuestionIndex >= _qlist.Count;
            }
        }

        public TriviaSet(List<Question> QList, QuestionUpdateHandler NewQuestionHandler)
        {
            _qlist = QList;

            if(NewQuestionHandler != null)
                this.OnNewQuestion += NewQuestionHandler;

            RaiseNewQuestion();
        }

        public bool SubmitAnswer(string PlayerAnswer)
        {
            if (IsComplete)
                // TODO: What do we want to do here? Exception?
                return false;

            bool Correct = CurrentQuestion.IsCorrect(PlayerAnswer);
            if (Correct)
                _numberCorrect++;

            _currentQuestionIndex++;
            RaiseNewQuestion();
            return Correct;
        }

        private void RaiseNewQuestion()
        {
            if(OnNewQuestion != null)
                OnNewQuestion(this, new EventArgs());
        }
    }
}
