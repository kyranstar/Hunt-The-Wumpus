using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.Trivia;

namespace HuntTheWumpus.Platform.Desktop.Trivia
{
    class TriviaLoader
    {
        static XmlSerializer Loader = new XmlSerializer(typeof(List<Question>));
        /*
         * I need help in the line ahead of this:
         * Basically, I tried to use the base directory of our project to create the file path.
         * If done correctly, this should place/retrieve Trivia.xml in the Trivia folder.
         * The problem I'm getting seems to be that the directory is too long.
         * 
         * Your problem might also be down in the main method, where I have put instructions down below.
         * */
        static String FilePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("\\Final Game") + 12) + "Shared Code\\Trivia" + "Trivia.xml";
        static Stream Streamer = FileUtils.GetFileStream(FilePath);

        //What you want to get from outside this class.
        static List<Question> questionPack = new List<Question>();
        static List<Question> QuestionPack
        {
            get { return questionPack; }
        }

        public static void Main(string[] args)
        {
            /*
             * I used this method to serialize/create a sample question set.
             * To use this method, right click desktop and change your Startup Object to this.
             * When it's created Trivia.xml, switch the Startup Object back.
             * */
            List<Question> YouGotSerialized = new List<Question>();
            List<String> answer1 = new List<string>{"Olympia", "Salem", "Sacramento", "Augusta"};
            Question first = new Question("What is the capital of Maine?", answer1, "Augusta");
            List<String> answer2 = new List<string>{"10", "20", "30", "40"};
            Question second = new Question("10 + 10 = ?", answer2, "20");
            YouGotSerialized.Add(first);
            YouGotSerialized.Add(second);
            Loader.Serialize(Streamer, YouGotSerialized);
        }

        public TriviaLoader()
        {
            //Standard Constructor that deserializes the data.
            questionPack = (List<Question>)Loader.Deserialize(Streamer);
        }


    }
}
