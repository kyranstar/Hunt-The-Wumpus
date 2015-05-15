using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public static class Trivia
    {
        private static List<String> _hints = new List<String>();

        public static List<String> Hints
        {
            get { return _hints; }
        }

        private static List<Question> _questionsToAsk = new List<Question>();
        private static List<Question> _questionsAlreadyAsked = new List<Question>();
        
        static Trivia()
        {
            Log.Info("Creating Trivia...");
            _hints.AddRange(new string[] { "hint1", "hint2", "hint3" });
            _questionsToAsk.Add(new Question("What color is red paint?", new List<string> { "Red", "Blue", "Orange", "Rainbow" }, "Red"));
            _questionsToAsk.Add(new Question("What color is blue paint?", new List<string> { "Blue", "Orange", "Red", "It depends..." }, "Blue"));
            _questionsToAsk.Add(new Question("What color is orange paint?", new List<string> { "Red", "Orange", "Blue", "Purple" }, "Red"));
            _questionsToAsk.Add(new Question("What color is white paint paint?", new List<string> { "White", "Blue", "Green", "Red" }, "White"));
            _questionsToAsk.Add(new Question("What color is violet paint?", new List<string> { "Violet", "Orange", "Rainbow", "Red" }, "Red"));
        }

        public static TriviaSet CreateTriviaSet(int NumberOfQuestions, TriviaSet.QuestionUpdateHandler NewQuestionHandler = null)
        {
            Random random = new Random();
            List<Question> inputList = new List<Question>();
            for(int i = 0; i<NumberOfQuestions; i++)
            {
                int j = random.Next(0, _questionsToAsk.Count);
                Question question = _questionsToAsk[j];
                inputList.Add(question);

                _questionsAlreadyAsked.Add(question);
                _questionsToAsk.Remove(question);

                if(_questionsToAsk.Count <= 0)
                {
                    _questionsToAsk.AddRange(_questionsAlreadyAsked);
                    _questionsAlreadyAsked.Clear();
                }
            }

            return new TriviaSet(inputList, NewQuestionHandler);
        }
        
    }
}
