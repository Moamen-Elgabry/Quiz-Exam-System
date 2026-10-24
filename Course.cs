using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Quiz_Exam_System
{
    internal class Course
    {
        private string _name;
        private string _category;
        private TimeSpan _duration;
        private Teacher _teacher; 
        private int _noOfLessons;
        private string _createdBy;

        private List<Quiz> quizzes = new List<Quiz>();
        private List<Lesson> lessons = new List<Lesson>();
        private List<Student> students = new List<Student>();


        private Course(string name, string category , TimeSpan duration ,int noOfLessons , string createdBy)
        {
            Name = name;
            Category = category;
            Duration = duration;
            NoOfLessons = noOfLessons;
            CreatedBy = createdBy;
            
        }


        public string Name
        {
            get => _name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseNameMandatory));
                }

                _name = value.Trim();
            }
        }


        public string Category
        {
            get => _category;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseCategoryMandatory));
                }

                _category = value.Trim();
            }
        }

        public TimeSpan Duration
        {
            get => _duration;

            private set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.DurationMandatory));
                }

                _duration = value;
            }
        }


        public int NoOfLessons
        {
            get => _noOfLessons;

            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.MustBePositive));
                }

                _noOfLessons = value;
            }
        } 

        public string CreatedBy
        {
            get => _createdBy;

            private set => _createdBy = value;
        }


        public Teacher Teacher
        {
            get => _teacher;

            private set
            {
                _teacher = value;
            }
        }


        public List<Student> Students
        {
            get => new List<Student>(students);

            private set
            {

            }
        }
        public List<Quiz> Quizzes
        {
            get => quizzes;

            private set
            {

            }
        }

        public static Course AddCourse(string name, string category, TimeSpan duration, int noOfLessons, Token token)
        {

            if (token is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidUser));
            }

            if (token.Type != UserType.Admin)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.UnAuthorizedAction));
            }

            return new Course(name, category, duration, noOfLessons, token.Name);
        }


        public void AddStudent(Student student)
        {
            if(student is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.StudentRequired));

            }

            if (Students.Contains(student))
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.StudentAlreadyEnrolled));
            }

            students.Add(student);

        }


        //public void AddLesson(Lesson lesson)
        //{
        //    if (lesson is null)
        //    {
        //        throw new ArgumentException("Error : Invalid Input ");
        //    }

        //    lessons.Add(lesson);
        //}


        public void AddQuiz(Quiz quiz)
        {
            if (quiz is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.FieldRequired));
            }

            quizzes.Add(quiz);
        }

        public void AssignTeacher(Teacher teacher)
        {
            if(teacher is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidUser));
            }

            Teacher = teacher;
        }
        
        public void ReleaseTeacher(Teacher teacher)
        {
            if(teacher is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidUser));

            }


            if(Teacher != teacher)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.NotYourCourse));
            }

            if(Students.Count > 0)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseHasStudents));
            }


            Teacher = null;

        }

        public Quiz FindQuiz(string name)
        {
            foreach(Quiz quiz in Quizzes)
            {
                if(quiz.Name.ToLower() == name.Trim().ToLower())
                {
                    return quiz;
                }
            }

            return null;
        }
    }
}
