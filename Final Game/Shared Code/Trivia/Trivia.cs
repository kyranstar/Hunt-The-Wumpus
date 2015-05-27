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

            // TODO: Add system for getting contextual hint
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

            /*
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
			*/
			StandbySet.Add(new Question("From what source do solar panels generate electricity?", new List<string> { "The Sun", "Gas", "Coal", "Wood" }, "The Sun"));
			StandbySet.Add(new Question("In what year did the Apple iPhone first become available?", new List<string> { "2005", "2006", "2007" }, "2007"));
			StandbySet.Add(new Question("In terms of computing, what does CPU stand for?", new List<string> { "Computer Power Unifier", "Central Processing Unit", "Certified Professional Undergrad", "Can't Power Up" }, "Central Processing Unit"));
			StandbySet.Add(new Question("In what year was Nintendo founded?", new List<string> { "1983", "1946", "1928", "1889" }, "1889"));
			StandbySet.Add(new Question("The Hubble Space Telescope is named after which American astronomer?", new List<string> { "Albert Einstein", "Charles Darwin", "Edwin Hubble", "Hubert Hubble" }, "Edwin Hubble"));
			StandbySet.Add(new Question("For what reason can humans not see infrared light?", new List<string> { "Its wavelength is too long.", "Its wavelength is too short.", "It isn't bright enough", "Its frequency is too high." }, "Its wavelength is too long."));
			StandbySet.Add(new Question("What company publishes the Firefox web browser?", new List<string> { "Google", "Microsoft", "Mozilla", "Apple" }, "Mozilla"));
			StandbySet.Add(new Question("Which metal is the best electrical conductor?", new List<string> { "Metal doesn't conduct electricity.", "Gold", "Steel", "Iron" }, "Gold"));
			StandbySet.Add(new Question("The technologically advanced humanoid robot ASIMO is made by which car company?", new List<string> { "Ford", "GM", "Toyota", "Honda" }, "Honda"));
			StandbySet.Add(new Question("Upon what scientific principal are nuclear bombs based?", new List<string> { "Big bomb go boom.", "Atomic Fission", "The Sun", "Ohm's law" }, "Atomic Fission"));
			StandbySet.Add(new Question("In terms of computing, what does ROM stand for?", new List<string> { "Read Only Memory", "Random Order Mixer", "Radioactive Object Maker", "Rational Objective Multiplier" }, "Read Only Memory"));
			StandbySet.Add(new Question("What form of digital media did the original PlayStation use for game storage?", new List<string> { "Cartridges", "Papyrus", "USB Sticks", "CDs" }, "CDs"));
			StandbySet.Add(new Question("From what source does the earth get the majority of its energy?", new List<string> { "Mexico", "The Sun", "Natural Gas", "Oil" }, "The Sun"));
			StandbySet.Add(new Question("What does the company name \"IBM\" stand for?", new List<string> { "Internet Buying Metacorp", "Internally Bursting Molecule", "International Business Machines", "International Banking Machine" }, "International Business Machines"));
			StandbySet.Add(new Question("Along with whom did Bill Gates found Microsoft?", new List<string> { "Paul Allen", "Steve Jobs", "Tim Cook", "Larry Page" }, "Paul Allen"));
			StandbySet.Add(new Question("What science fiction writer wrote the three laws of robotics?", new List<string> { "Isaac Newton", "Isaac Asimov", "Stephen King", "J.K. Rowling" }, "Isaac Asimov"));
			StandbySet.Add(new Question("What does the abbreviation WWW stand for?", new List<string> { "World Wide Web", "World Without Windows", "Washing Without Windex", "Wishing With Wells" }, "World Wide Web"));
			StandbySet.Add(new Question("Nano, Shuffle, Classic and Touch are variations of what?", new List<string> { "The Microsoft Zune", "The Apple Ipod", "Dance Moves" }, "The Apple Ipod"));
			StandbySet.Add(new Question("In biology, what does DNA stand for?", new List<string> { "Deoxyribonucleic Acid", "Does Not Apply", "Do Not Abbreviate", "Definitely Not an Acronym" }, "Deoxyribonucleic Acid"));
			StandbySet.Add(new Question("What does the \"S\" at the end of \"HTTPS\" signify in a web address?", new List<string> { "The website is saying you're stupid", "The browser is confused.", "The connection is secure.", "The connection is simple." }, "The connection is secure."));
            _questionsToAsk = StandbySet;
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
