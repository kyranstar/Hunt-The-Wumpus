using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;
using HuntTheWumpus.SharedCode.Helpers;
using System.Linq;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public static class Trivia
    {
        public static List<string> LockedHints
        {
            get; private set;
        }

        public static List<string> AvailableHints
        {
            get; private set;
        }

        private static List<Question> _questionsToAsk = new List<Question>();
        private static List<Question> _questionsAlreadyAsked = new List<Question>();
        
        static Trivia()
        {
            Log.Info("Creating Trivia...");

            LockedHints = new List<string>();
            AvailableHints = new List<string>();

            // TODO: Add system for getting contextual hint
            LockedHints.Add("Blue paint isn't orange.");
            LockedHints.Add("White paint isn't green.");
            LockedHints.Add("Orange paint isn't white.");
            LockedHints.Add("Insert hint here.");
            LockedHints.Add("Insert another hint here.");

            _questionsToAsk.Add(new Question("What color is red paint?", new List<string> { "Red", "Blue", "Orange", "Rainbow" }, "Red"));
            _questionsToAsk.Add(new Question("What color is blue paint?", new List<string> { "Blue", "Orange", "Red", "It depends..." }, "Blue"));
            _questionsToAsk.Add(new Question("What color is orange paint?", new List<string> { "Red", "Orange", "Blue", "Purple" }, "Orange"));
            _questionsToAsk.Add(new Question("What color is white paint?", new List<string> { "White", "Blue", "Green", "Red" }, "White"));
            _questionsToAsk.Add(new Question("What color is violet paint?", new List<string> { "Violet", "Orange", "Rainbow", "Red" }, "Violet"));
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

        public static void UnlockNewHint()
        {
            // TODO: Move this out of a static context (maybe into a HintSet or similar)?
            if (LockedHints.Count <= 0)
                // TODO: Do something different here -- this will allow duplicate hints
                LockedHints.AddRange(AvailableHints);

            string Hint = LockedHints.GetRandom();
            LockedHints.Remove(Hint);
            AvailableHints.Add(Hint);
        }
    }
}
