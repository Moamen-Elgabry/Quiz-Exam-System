using System;
using System.Collections.Generic;
using System.Text;
                                                                                                                                                                                            
namespace Quiz_Exam_System
{
    internal class Teacher : User
    {
        private string _title;

        private Teacher(string name,string title, string email, string password) : base(name,email,password)
        {
            Title = title;
        }

        public string Title
        {
            get => _title;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.TitleMandatory));
                }

                _title = value.Trim();
            }

        }

        public static Teacher Register(string name, string title, string email, string password)
        {

            return new Teacher(name, title, email, password);
        }


        public void AssignToCourse(Course course)
        {
            if(course.Teacher is null)
            {
                course.AssignTeacher(this);
            }
            else
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.TeacherAlreadyAssigned));

            }


        }

        public List<Course> ViewAssignedCourses(List <Course> courses)
        {
            List<Course> assignedCourses = new List<Course>();
            if(courses == null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseMandatory));
            }

            foreach(Course course in courses)
            {
                if(course.Teacher == this)
                {
                    assignedCourses.Add(course);
                }
            }


            return assignedCourses;
        }


        public List<Course> ViewUnAssignedCourses(List<Course> courses)
        {
            List<Course> unAssignedCourses = new List<Course>();
            if (courses == null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseMandatory));
            }

            foreach (Course course in courses)
            {
                if (course.Teacher is null)
                {
                    unAssignedCourses.Add(course);
                }
            }


            return unAssignedCourses;
        }


        public void ReleaseCourse(Course course)
        {
            if(course is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                
            }

            course.ReleaseTeacher(this);
        }

        public Quiz CreateQuiz(Course course, string name, TimeSpan duration , List<Question> questions)
        {
            if (course is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseNotFound));

            }


            if (course.Teacher != this)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.NotYourCourse));
            }


            Quiz quiz = Quiz.CreateQuiz(name, duration , course, questions);


            course.AddQuiz(quiz);

            return quiz;
        }

        public void AddQuestionToQuiz(Quiz quiz, Question question)
        {
            CheckOwnership(quiz);

            quiz.AddQuestion(question);
        }

        public void RemoveQuestionFromQuiz(Quiz quiz, int index)
        {
            CheckOwnership(quiz);

            quiz.RemoveQuestion(index);
        }

        private void CheckOwnership(Quiz quiz)
        {
            if (quiz is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.QuizNotFound));
            }

            if (quiz.Course.Teacher != this)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.NotYourCourse));
            }
        }

    }
}
