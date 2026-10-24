using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class TrueFalse : Question
    {
        public const string True = "true";
        public const string False = "false";
        private TrueFalse(string description , string answer) :base(description , answer)
        {

        }
        public override bool CheckAnswer(string answer)
        {

            if(string.IsNullOrWhiteSpace(answer))
            {
                return false;

            }

            answer = answer.Trim().ToLower();

            if(answer == Answer)
            {
                return true;
            }

            return false;
        }

        public static TrueFalse CreateQuestion(string description, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.AnswerMandatory));

            }

            answer = TrueFalse.ValidateAnswer(answer);

            if (answer == string.Empty)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidAnswer));
            }

            return new TrueFalse(description, answer);
        }

        public static string ValidateAnswer(string answer)
        {
            answer = answer.Trim().ToLower();

            if ((answer != TrueFalse.True) && answer != TrueFalse.False)
            {
                return string.Empty;
            }

            return answer;
        }

    }
}
