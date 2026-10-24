using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class Quiz 
    {
        private int id;
        private string _name;
        private TimeSpan _duration;
        private Course course;
        const int answersPerQuestion = 1;
        private List<Question> questions = new List<Question>();
        private List<Attempt> attempts = new List<Attempt>();

    
        const int percent = 100;
        private Quiz(string name, TimeSpan duration , Course course , List<Question> questions)
        {
            Name = name;
            Duration = duration;
            Course = course;
            Questions = questions;
            
        }

        public string Name
        {
            get => _name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.NameMandatory));
                }

                _name = value.Trim();
            }     
        }

        public TimeSpan Duration
        {
            get => _duration;

            private set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidDuration));
                }

                _duration = value;
            }
        }

        public Course Course
        {
            get => course;

            private set
            {
                if(value is null)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseMandatory));
                }
                course = value;
            }

        }

        public List<Question> Questions
        {
            get => new List<Question>(questions);

            private set
            {
                if(value is null || value.Count == 0)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuestionsMandatory));
                }

                questions = value;
            }
        }

        public List<Attempt> Attempts
        {
            get => new List<Attempt>(attempts);
        }

        public static Quiz CreateQuiz(string name, TimeSpan duration , Course course , List<Question> questions)
        {

            return new Quiz(name, duration , course , questions);
        }


        public double CalculateScore(List<AttemptAnswer> studentAnswers)
        {
            double score = 0;

                foreach (AttemptAnswer answer in studentAnswers)
                {

                    if (answer.State == AnswerState.Correct)
                    {
                        score += 1;
                    }
 
            }

            return (score * percent) / Questions.Count;
        }

        public Attempt Submit(Student student, List<AttemptAnswer> studentAnswers, DateTime takenDate, TimeSpan actualDuration)
        {

            if (studentAnswers is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.AnswerMandatory));
            }

            List<Question> quizQuestions = Questions;

            if (quizQuestions.Count != studentAnswers.Count)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.IncorrectStudentAnswers));
            }

            foreach (Question question in quizQuestions)
            {
                int matches = 0;

                foreach (AttemptAnswer answer in studentAnswers)
                {
                    if (answer.Question == question)
                    {
                        matches++;
                    }
                }

                if (matches != answersPerQuestion)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.IncorrectStudentAnswers));
                }
            }

            double score = CalculateScore(studentAnswers);

            Attempt attempt = new Attempt(takenDate, actualDuration, score, student, studentAnswers);

            attempts.Add(attempt);

            return attempt;
        }

        public bool HasBeenTaken()
        {
            return attempts.Count > 0;
        }

        public int CountStudents()
        {
            List<Student> students = new List<Student>();

            foreach (Attempt attempt in attempts)
            {
                if (!students.Contains(attempt.Student))
                {
                    students.Add(attempt.Student);
                }
            }

            return students.Count;
        }

        public void AddQuestion(Question question)
        {
            if (question is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuestionsMandatory));
            }

            if (HasBeenTaken())
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuizAlreadyTaken));
            }

            questions.Add(question);
        }

        public void RemoveQuestion(int index)
        {
            if (HasBeenTaken())
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuizAlreadyTaken));
            }

            if (index < 0 || index >= questions.Count)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.OutOfRange, 1, questions.Count));
            }

            if (questions.Count == 1)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CannotRemoveLastQuestion));
            }

            questions.RemoveAt(index);
        }

    }
}
