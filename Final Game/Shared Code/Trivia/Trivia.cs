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
        private static List<Question> StandbySet = new List<Question>();
        
        static Trivia()
        {
            Log.Info("Creating Trivia...");

            LockedHints = new List<string>();
            AvailableHints = new List<string>();

            LockedHints.Add("Blue paint isn't orange.");
            LockedHints.Add("Nintendo wasn't founded in the 20th century.");
            LockedHints.Add("Orange paint isn't white.");
            LockedHints.Add("The Sun is Earth's strongest energy source.");
            LockedHints.Add("White paint isn't green.");
            LockedHints.Add("Gold is a great conductor.");
            LockedHints.Add("Jumping jacks can be fun.");
            LockedHints.Add("RAM stand for Random Access Memory.");
            LockedHints.Add("Computers process information quickly.");
            LockedHints.Add("Tons of information can be found on the World Wide Web.");
            LockedHints.Add("Ohm's law has nothing to do with nuclear reactions.");
            LockedHints.Add("There's no such thing as the Zune Shuffle.");
            LockedHints.Add("Bill Gates was and still is friends with Paul Allen.");
            LockedHints.Add("Isaac Asimov's science fiction writing is cool.");
            LockedHints.Add("IBM != Internet Buying Metacorp.");
            LockedHints.Add("The Sun is Earth's strongest energy source.");

            CsvFile File = new CsvFile();
            File.Load("Data\\Trivia Questions.csv");
            _questionsToAsk = File
                .BindAs<BindableFlatQuestion>()
                .Select(q => q.ToQuestion())
                .ToList();

				
            StandbySet.Add(new Question("What color is red paint?", new List<string> { "Red", "Blue", "Orange", "Rainbow" }, "Red"));
            StandbySet.Add(new Question("What color is blue paint?", new List<string> { "Blue", "Orange", "Red", "It depends..." }, "Blue"));
            StandbySet.Add(new Question("What color is orange paint?", new List<string> { "Red", "Orange", "Blue", "Purple" }, "Orange"));
            StandbySet.Add(new Question("What color is white paint?", new List<string> { "White", "Blue", "Green", "Red" }, "White"));
            StandbySet.Add(new Question("What color is violet paint?", new List<string> { "Violet", "Orange", "Rainbow", "Red" }, "Violet"));
        }

        public static TriviaSet CreateTriviaSet(int NumberOfQuestions, TriviaSet.QuestionUpdateHandler NewQuestionHandler = null)
        {
            Random random = new Random();
            List<Question> inputList = new List<Question>();
            for(int i = 0; i<NumberOfQuestions; i++)
            {
                int j = random.Next(0, _questionsToAsk.Count);
                Question question = _questionsToAsk[j];
                question.RandomizeAnswerOrder();
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

        public static void ToggleTestingSet()
        {
            // TODO: Test this
            List<Question> Tmp = StandbySet;
            StandbySet = _questionsToAsk;
            StandbySet.AddRange(_questionsAlreadyAsked);
            _questionsAlreadyAsked.Clear();
            _questionsToAsk = Tmp;
        }
    }
}
