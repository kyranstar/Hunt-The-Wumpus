using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.Trivia
{
    public class Question
    {
        private string _questionText;

        public string QuestionText
        {
            get { return _questionText; }
        }

        private List<string> _answerChoices = new List<string>();

        public List<string> AnswerChoices
        {
            get { return _answerChoices; }
        }

        private string _correctAnswer;
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
        }

        public Question(string QuestionTextInput, List<string> AnswerChoicesInput, string CorrectAnswerInput)
        {
            _questionText = QuestionTextInput;
            _answerChoices = AnswerChoicesInput;
            _correctAnswer = CorrectAnswerInput;
        }
        
        public bool IsCorrect(string PlayerAnswer)
        {
            // TODO: Store the correct answer as something other than a string (for resiliency)
            return _correctAnswer.Equals(PlayerAnswer, StringComparison.Ordinal);
        }


    }
}