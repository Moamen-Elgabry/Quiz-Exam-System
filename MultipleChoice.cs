using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class MultipleChoice : Question
    {
        private List<string> options = new List<string>();

        public const int minOptions = 3;
        public const int maxOptions = 5;

        private MultipleChoice(string description , string answer , List <string> options) : base(description , answer)
        {

            if (options == null)
            {

                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.OptionsRequired));

            }

            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.AnswerMandatory));

            }

            if (options.Count < minOptions || options.Count > maxOptions)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidRange));
            }

            Options = options;
        }

        public override bool CheckAnswer(string answer)
        {

            if (string.IsNullOrWhiteSpace(answer))
            {
                return false;

            }

            answer = answer.Trim().ToLower();

            if (answer == Answer)
            { 

                return true;
            }

            return false;
  
        }

        public List<string> Options
        {
            get => options;

            private set
            {

                options = value;
            }
        }

        public static MultipleChoice CreateQuestion(string description , string answer , List <string> options)
        {

            answer = answer.Trim().ToLower();

            foreach (string option in options)
            {
                
                if (option.Trim().ToLower() == answer )
                {
                    return new MultipleChoice(description, answer, options);

                }
            }

                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidAnswer));
        }

    }
}