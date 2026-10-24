using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{

    enum StudentState
    {
        Passed,
        Failed
    }

    enum AnswerState
    {
        Correct,
        Wrong
    }

    internal class Attempt
    {
        private int id;
        private DateTime _takenDate;
        private TimeSpan _actualDuration;
        private double _score;
        const double maxScore = 100;
        const double minScore = 0;
        const double passingScore = 50;
        private Student student;
        private List<AttemptAnswer> answers;


        public Attempt(DateTime takenDate, TimeSpan actualDuration, double score, Student student, List<AttemptAnswer> answers)
        {
            TakenDate = takenDate;
            ActualDuration = actualDuration;
            Score = score;
            Student = student;
            Answers = answers;
        }

        public int Id
        {
            get => id;
        }

        public DateTime TakenDate
        {
            get => _takenDate;

            private set
            {
               
                _takenDate = value;
            }
        }

        public TimeSpan ActualDuration
        {
            get => _actualDuration;

            private set
            {
                if(value < TimeSpan.Zero)
                {
                    
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CannotBeNegative));
                    
                }

                _actualDuration = value;
            }
        }

        public double Score
        {
            get => _score;

            private set
            {
                if(value is < minScore or > maxScore)
                {
                
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.OutOfRange, minScore ,maxScore));
                
                }

                _score = value;
            }
        }
             
        public bool IsPassed
        {
            get
            {
                if (Score < passingScore)
                {
                    return false;
                }

                return true;
            }
        }

        public Student Student
        {
            get => student;

            private set
            {
                if(value is null)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.StudentRequired));
                }

                student = value;
            }
        }

        public List<AttemptAnswer> Answers
        {
            get => new List<AttemptAnswer >(answers);

            private set
            {
                if (value is null)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.AnswerMandatory));
                }
                answers = value;
            }
        }

        public string AttemptSummary()
        {
            string summary;

            summary = Score +"/ " +  (IsPassed ? nameof(StudentState.Passed) : nameof(StudentState.Failed));

            return summary;
        }

    }
}
