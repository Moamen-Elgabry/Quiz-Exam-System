using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class AttemptAnswer
    {
        private Question question;
        private string studentAnswer;
        private AnswerState state = AnswerState.Wrong;

        public AttemptAnswer(Question question ,string studentAnswer)
        {
            Question = question;

            StudentAnswer = studentAnswer;

            if (question.CheckAnswer(StudentAnswer))
            {
                state = AnswerState.Correct;
            }
        }


        public Question Question
        {
            get => question;

            private set
            {
                if (value is null)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuestionMandatory));
                }
                question = value;
            }
        }


        public string StudentAnswer
        {
            get => studentAnswer;

            private set
            {
                if (value is null)
                {
                    studentAnswer = string.Empty;
                    return;
                }

                studentAnswer = value.Trim();
            }

        }

        public AnswerState State => state;
        
            
        

    }
}
