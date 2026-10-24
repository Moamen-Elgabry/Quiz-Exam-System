using System;
using System.Collections.Generic;
using System.Text;


namespace Quiz_Exam_System
{
    enum QuestionType
    {
        ShortAnswer = 1,
        TrueFalse,
        MultipleChoice
    }

    internal abstract class Question
    {
        protected int id;
        protected string _description;
        protected string _answer;

        protected Question(string description, string answer)
        {
            Description = description;
            Answer = answer;
        }

        public abstract bool CheckAnswer(string answer);
        
       public string Description
        {
            get => _description;

           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.DescriptionMandatory));
                }

                _description = value.Trim();
            }
        }

        public string Answer
        {
            get => _answer;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.AnswerMandatory));
                }

                _answer = value.Trim().ToLower();
            }
        }



    }
}
