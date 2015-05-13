using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.Trivia
{
    class Trivia
    {
        private List<String> _hints = new List<String>();

        public List<String> Hints
        {
            get { return _hints; }
        }

        private List<String> _possibleQuestions;
        
        public Trivia()
        {
            Log.Info("Creating Trivia...");
            _hints.AddRange(new string[] {"hint1", "hint2", "hint3"});
            _possibleQuestions.AddRange(new string[] { "question1", "question2", "question3" });

        }
        public Question CreateNewQuestion()
        {
            return new Question (_possibleQuestions[0]
                , new List<string>(new string[] {"wrong", "wrong", "right", "wrong"})
                , "right");
        }
    }
}
