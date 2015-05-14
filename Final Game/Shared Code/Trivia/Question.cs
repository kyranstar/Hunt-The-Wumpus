using HuntTheWumpus.SharedCode.GameControl;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.Trivia
{
    class Question
    {
        private String _questionText;

        public String QuestionText
        {
            get { return _questionText; }
        }

        private List<String> _answerChoices = new List<String>();

        public List<String> AnswerChoices
        {
            get { return _answerChoices; }
        }

        private String _correctAnswer;
        public String CorrectAnswer
        {
            get { return _correctAnswer; }
        }

        public Question(String QuestionTextInput, List<String> AnswerChoicesInput, String CorrectAnswerInput)
        {
            _questionText = QuestionTextInput;
            _answerChoices = AnswerChoicesInput;
            _correctAnswer = CorrectAnswerInput;
        }
        
        public bool IsCorrect(String PlayerAnswer)
        {
            return _correctAnswer.Equals(PlayerAnswer, StringComparison.Ordinal);
        }


    }
}