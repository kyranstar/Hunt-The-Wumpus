using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Linq;
using System.Collections.Generic;
using HuntTheWumpus.SharedCode.Helpers;

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
            return _correctAnswer.Equals(PlayerAnswer, StringComparison.Ordinal);
        }

        public void RandomizeAnswerOrder()
        {
            AnswerChoices.Shuffle();
        }
    }

    public class BindableFlatQuestion
    {
        public string QuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public string AlternateAnswer1 { get; set; }
        public string AlternateAnswer2 { get; set; }
        public string AlternateAnswer3 { get; set; }

        public Question ToQuestion()
        {
            var AvailableAnswers = new List<string>()
                {
                    CorrectAnswer,
                    AlternateAnswer1,
                    AlternateAnswer2,
                    AlternateAnswer3
                };
            AvailableAnswers.Where(s => !string.IsNullOrEmpty(s));
            return new Question(
                QuestionText,
                AvailableAnswers,
                CorrectAnswer);
        }
    }
}