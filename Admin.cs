using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class Admin : User
    {
        public readonly static Admin admin = new Admin(adminName,adminEmail,adminPass);

        private const string adminName = "Moamen";
        private const string adminEmail = "moamen@gmail.com";
        private const string adminPass = "Admin123";


        private Admin(string name ,string email, string password) : base(password)
        {
            Name = name;
            Email = email;

        }
        public bool AddCourse(List<Course> courses, string name, string category, TimeSpan duration, int noOfLessons, Token token)
        {
            Course course = Course.AddCourse(name, category, duration, noOfLessons, token);

            courses.Add(course);

            return true;
        }

        public void ViewAllCourses(List <Course> courses)
        {
            Console.WriteLine("\n================ All Courses =======================================");

            foreach (Course course in courses)
            {
                Console.WriteLine("\n==================================================");

                Console.WriteLine($"\nCourse : {course.Name}");
                Console.WriteLine("\nTeacher : " + (course.Teacher?.Name ?? Messages.GetErrorMessage(ErrorKey.UnAssignedTeacher)) );
                Console.WriteLine($"\nDuration : {course.Duration}");
                Console.WriteLine($"\nNo. Of Lessons : {course.NoOfLessons}");
                Console.WriteLine($"\nCategory : {course.Category}");
                Console.WriteLine($"\nNo. Of Students : {course.Students.Count}");
                Console.WriteLine($"\nNo. Of Quizzes : {course.Quizzes.Count}");
                Console.WriteLine($"\nList of Students :");

                foreach (Student student in course.Students)
                {

                Console.WriteLine($"\n - {student.Name}");

                }

                Console.WriteLine($"\nList of Quizzes :");
                foreach (Quiz quiz in course.Quizzes)
                {

                    Console.WriteLine($"\n - {quiz.Name}");

                }

            }

        }

        public void ViewAllTeachers(Dictionary<string, User> users, List<Course> courses)
        {
            Console.WriteLine("\n================ All Teachers =======================================");

            foreach (User user in users.Values)
            {
                if (user is not Teacher)
                {
                    continue;
                }

                Teacher teacher = (Teacher)user;

                List<Course> teacherCourses = teacher.ViewAssignedCourses(courses);
                List<string> quizNames = new List<string>();

                foreach (Course course in teacherCourses)
                {
                    foreach (Quiz quiz in course.Quizzes)
                    {
                        quizNames.Add(quiz.Name);
                    }
                }

                Console.WriteLine("\n==================================================");
                Console.WriteLine($"Name: {teacher.Name}");
                Console.WriteLine($"Email: {teacher.Email}");
                Console.WriteLine($"Title: {teacher.Title}");
                Console.WriteLine($"No. of Courses: {teacherCourses.Count}");
                Console.WriteLine($"No. of Quizzes Created: {quizNames.Count}");
                Console.WriteLine("Courses:");

                foreach (Course course in teacherCourses)
                {
                    Console.WriteLine($" - {course.Name} ({course.Category})");
                }

                Console.WriteLine("Quizzes:");

                foreach (string quizName in quizNames)
                {
                    Console.WriteLine($" - {quizName}");
                }
            }
        }

        public void ViewAllStudents(Dictionary<string, User> users, List<Course> courses)
        {
            Console.WriteLine("\n================ All Students =======================================");

            foreach (User user in users.Values)
            {
                if (user is not Student)
                {
                    continue;
                }

                Student student = (Student)user;

                List<string> quizNames = new List<string>();
                double totalScore = 0;
                int attemptsCount = 0;

                foreach (Course course in courses)
                {
                    foreach (Quiz quiz in course.Quizzes)
                    {
                        bool taken = false;

                        foreach (Attempt attempt in quiz.Attempts)
                        {
                            if (attempt.Student != student)
                            {
                                continue;
                            }

                            taken = true;
                            totalScore += attempt.Score;
                            attemptsCount++;
                        }

                        if (taken)
                        {
                            quizNames.Add(quiz.Name);
                        }
                    }
                }

                double grade = attemptsCount == 0 ? 0 : totalScore / attemptsCount;

                Console.WriteLine("\n==================================================");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Email: {student.Email}");
                Console.WriteLine($"Grade (average score): {grade:F1}");
                Console.WriteLine($"No. of Courses: {student.Courses.Count}");
                Console.WriteLine($"No. of Quizzes Taken: {quizNames.Count}");
                Console.WriteLine("Courses:");

                foreach (Course course in student.Courses)
                {
                    Console.WriteLine($" - {course.Name}");
                }

                Console.WriteLine("Quizzes:");

                foreach (string quizName in quizNames)
                {
                    Console.WriteLine($" - {quizName}");
                }
            }
        }

    }
}
