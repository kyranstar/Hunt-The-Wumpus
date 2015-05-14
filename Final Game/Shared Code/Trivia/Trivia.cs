using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class Trivia
    {
        private List<String> _hints = new List<String>();

        public List<String> Hints
        {
            get { return _hints; }
        }

        private List<Question> _questionsToAsk;
        private List<Question> _questionsAlreadyAsked;
        
        public Trivia()
        {
            Log.Info("Creating Trivia...");
            _hints.AddRange(new string[] { "hint1", "hint2", "hint3" });
            _questionsToAsk.Add(new Question("question1", new List<string> { "1", "2", "3", "4" }, "1"));
            _questionsToAsk.Add(new Question("question2", new List<string> { "1", "2", "3", "4" }, "2"));
            _questionsToAsk.Add(new Question("question3", new List<string> { "1", "2", "3", "4" }, "3"));
            _questionsToAsk.Add(new Question("question4", new List<string> { "1", "2", "3", "4" }, "4"));
            _questionsToAsk.Add(new Question("question5", new List<string> { "5", "6", "7", "8" }, "5"));
            _questionsToAsk.Add(new Question("question6", new List<string> { "5", "6", "7", "8" }, "6"));
            _questionsToAsk.Add(new Question("question7", new List<string> { "5", "6", "7", "8" }, "7"));
            _questionsToAsk.Add(new Question("question8", new List<string> { "5", "6", "7", "8" }, "8"));
        }

        public TriviaSet CreateTriviaSet(int NumberOfQuestions)
        {
            Random random = new Random();
            Question question;
            List<Question> InputList = new List<Question>();
            int j;
            for(int i = 0; i<NumberOfQuestions; i++)
            {
                j = random.Next(0, _questionsToAsk.Count);
                question = _questionsToAsk[j];
                InputList.Add(question);
                _questionsAlreadyAsked.Add(question);
                _questionsToAsk.Remove(question);
            }
            return new TriviaSet(InputList);
        }
        
    }
}
