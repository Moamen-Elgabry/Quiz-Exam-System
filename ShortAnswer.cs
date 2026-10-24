using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class ShortAnswer : Question
    {
        private ShortAnswer(string description , string answer) : base(description , answer)
        {

        }

        public override bool CheckAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
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



        public static ShortAnswer CreateQuestion(string description, string answer)
        {

            return new ShortAnswer(description, answer);
        }




    }
}
