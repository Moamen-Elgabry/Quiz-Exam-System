using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Quiz_Exam_System
{
    // Create, Manage and Evaluate Quizzes for Students

    // Allows Teachers to Create Questions and Quizzes 
    
    // System automatically Evaluates Student Performance

    enum LoginState
    {
        EmailNotExist,
        NotCorrectPassword,
        Success
    }
  
    enum StartPage
    {
        Login = 1,
        Register
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            QuizExamSystem system = new QuizExamSystem();


            system.Run();
           

        }
    }
}