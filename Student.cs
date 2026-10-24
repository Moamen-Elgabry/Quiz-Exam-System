using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class Student : User 
    {


        private List<Course> courses = new List<Course>();
        private Student(string name, string email, string password) : base(name, email, password)
        {
          
        }

       public static Student Register(string name, string email, string password)
        {

            return new Student(name, email, password);
        }

        public List<Course> Courses
        {
            get => new List<Course>(courses);
        }
     
        public void EnrollInCourse(Course course)
        {
            if(course is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
            }

            if (course.Teacher is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.UnAssignedTeacher));
            }

            if (course.Students.Contains(this))
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.StudentAlreadyEnrolled));

            }

            courses.Add(course);

            course.AddStudent(this);

        } 

        public List<string> GetAttemptSummaries(Quiz quiz)
        {
            List<string> attemptSummaries = new List<string>();

            if (quiz is null)
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
            }

            foreach(Attempt attempt in quiz.Attempts)
            {
                if(attempt.Student == this)
                {
                    attemptSummaries.Add(attempt.AttemptSummary());
                }
            }

            return attemptSummaries;
        }


    }
}