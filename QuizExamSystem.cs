using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
 
namespace Quiz_Exam_System
{

    public enum SystemStatus
    {
        Running,
        Stopped
    }

    enum RegistrationMenu
    {
        Teacher = 1,
        Student
    }

    enum MainMenu
    {
        Login = 1,
        Register,
        Exit
    }


    enum AdminList
    {
        AddCourses = 1,
        ViewAllCourses,
        ViewAllTeachers,
        ViewAllStudents,
        Logout
    }

    enum TeacherList
    {
        AssignToCourse = 1,
        ViewAssignedCourses,
        ViewUnassignedCourses,
        ReleaseCourse,
        ViewQuizDetails,
        CreateQuiz,
        EditQuiz,
        Logout
    }

    enum StudentList
    {
        ViewAllCourses = 1,
        EnrollInCourse,
        ViewEnrolledCourse,
        TakeQuiz,
        ViewAllQuizzes,
        ViewQuiz,
        Logout
    }


    enum AddQuestionMenu
    {
        AddAnother = 1,
        Finish
    }

    enum EditQuizMenu
    {
        AddQuestion = 1,
        RemoveQuestion,
        Done
    }


    internal class QuizExamSystem
    {
        private SystemStatus _systemStatus;

        List<Course> courses = new List<Course>();
        Dictionary<string, User> usersLog = new Dictionary<string, User>();
        Admin admin = Admin.admin;
        Token token;

        public QuizExamSystem()
        {
            usersLog.Add(admin.Email, admin);

        }
    
        public void Run()
        {

            while (_systemStatus == SystemStatus.Running)
            {
                Console.WriteLine("\n============================================ Quiz / Exam System ============================================");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");

                MainMenu choice = (MainMenu)Tools.ReadIntInRange("Choose an option: ", 1, 3);

                switch (choice)
                {
                    case MainMenu.Login:

                        LoginUser(usersLog);

                        break;

                    case MainMenu.Register:

                        RegisterUser(usersLog);

                        break;

                    case MainMenu.Exit:

                        _systemStatus = SystemStatus.Stopped;

                        break;
                }
            }

            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.Goodbye));
        }


        private void RegisterUser(Dictionary<string, User> usersLog)
        {

            RegistrationMenu choice = RegistrationList();

            User user = null;

            switch (choice)
            {
                case RegistrationMenu.Teacher:

                    user = RegistrationTeacher(usersLog);

                    break;

                case RegistrationMenu.Student:

                    user = RegistrationStudent(usersLog);

                    break;
            }

            usersLog.Add(user!.Email, user);

            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.Registered));
        }


        private RegistrationMenu RegistrationList()
        {
            Console.WriteLine("\n============================================ Registration Page ============================================");
            Console.WriteLine("\n========== Choose Your Role: ============================================");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");

            RegistrationMenu choice = (RegistrationMenu)Tools.ReadIntInRange("Choose an option: ", 1, 2);


            return choice;
        }

        private Teacher RegistrationTeacher(Dictionary<string, User> usersLog)
        {

            string name = Tools.ReadNonEmptyLine("Enter Name: ");

            string title = Tools.ReadNonEmptyLine("Enter Title: ");

            string email;

            while (true)
            {
                 email = Tools.ReadNonEmptyLine("Enter Email: ");

                 email = Tools.CleanUpEmail(email);

                if (!usersLog.ContainsKey(email))
                {
                    break;
                }

                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.EmailTaken));
            }
            string password = Tools.ReadNonEmptyLine("Enter password: ");

            Teacher teacher = Teacher.Register(name, title, email, password);

            return teacher;

        }

        private Student RegistrationStudent(Dictionary<string, User> usersLog)
        {

            string name = Tools.ReadNonEmptyLine("Enter Name: ");

            string email;

            while (true)
            {
                email = Tools.ReadNonEmptyLine("Enter Email: ");

                email = Tools.CleanUpEmail(email);

                if (!usersLog.ContainsKey(email))
                {
                    break;
                }

                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.EmailTaken));
            }

            string password = Tools.ReadNonEmptyLine("Enter password: ");

            Student student = Student.Register(name, email, password);

            return student;

        }
        private void LoginUser(Dictionary<string, User> usersLog)
        {
            string email = Tools.ReadNonEmptyLine("Email: ");

            string password = Tools.ReadNonEmptyLine("Password: ");

            var result = SessionLogin.Login(usersLog, email, password);

            if ( result.State == LoginState.Success)
            {
                token = result.Token;

                switch (token.Type)
                {
                    case UserType.Admin:

                        AdminMenu((Admin)usersLog[token.Email]);

                        break;

                    case UserType.Teacher:

                        TeacherMenu((Teacher)usersLog[token.Email]);

                        break;

                    case UserType.Student:

                        StudentMenu((Student)usersLog[token.Email]);

                        break;
                }
            }
            else if (result.State == LoginState.EmailNotExist)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.AccountNotFound));

            }else if(result.State == LoginState.NotCorrectPassword)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.IncorrectPassword));
            }


            return;
        }

        private void AdminMenu(Admin admin)
        {
            while (token is not null)
            {
                Console.WriteLine("\n-------------------------------------- Admin Page --------------------------------------");
                Console.WriteLine("1. Add Courses");
                Console.WriteLine("2. View All Courses ");
                Console.WriteLine("3. View All Teachers ");
                Console.WriteLine("4. View All Students");
                Console.WriteLine("5. Logout");

                AdminList choice = (AdminList)Tools.ReadIntInRange("Choose an option: ", 1, 5);

                switch (choice)
                {
                    case AdminList.AddCourses:

                        AddCourse(admin, courses);
                        break;

                    case AdminList.ViewAllCourses:

                        admin.ViewAllCourses(courses);

                         break;

                    case AdminList.ViewAllTeachers:

                        admin.ViewAllTeachers(usersLog, courses);
                        break;

                    case AdminList.ViewAllStudents:

                        admin.ViewAllStudents(usersLog, courses);
                        break;

                    case AdminList.Logout:

                        token = null;

                         break;
                }
            }
        }

        private void TeacherMenu(Teacher teacher)
        {
            while (token is not null)
            {
                Console.WriteLine("\n-------------------------------------- Teacher Page --------------------------------------");
                Console.WriteLine("1. Assign To A Course");
                Console.WriteLine("2. View All Assigned Courses ");
                Console.WriteLine("3. View All Un-Assigned Courses ");
                Console.WriteLine("4. Release From A Course ");
                Console.WriteLine("5. View A Quiz Details ");
                Console.WriteLine("6. Make Quiz ");
                Console.WriteLine("7. Edit Quiz ");
                Console.WriteLine("8. Logout ");

                TeacherList choice = (TeacherList)Tools.ReadIntInRange("Choose an option: ", 1, 8);

                switch (choice)
                {
                    case TeacherList.AssignToCourse:

                        AssignTeacher(teacher); 
                        break;

                    case TeacherList.ViewAssignedCourses:

                        ViewAssignedCourses(courses, teacher);
                        break;

                    case TeacherList.ViewUnassignedCourses:

                        ViewUnAssignedCourses(courses, teacher);
                        break;

                    case TeacherList.ReleaseCourse:

                        ReleaseTeacher(teacher);
                        break;

                    case TeacherList.ViewQuizDetails:

                        ViewQuizDetails(teacher);

                        break;

                    case TeacherList.CreateQuiz:

                        MakeQuiz(teacher);
                        break;

                    case TeacherList.EditQuiz:

                        EditQuiz(teacher);
                        break;

                    case TeacherList.Logout:

                        token = null;

                        break;
                }
            }
        }


        private void StudentMenu(Student student)
        {
            while (token is not null)
            {
                Console.WriteLine("\n-------------------------------------- Student Page --------------------------------------");
                Console.WriteLine("1. View All Courses");
                Console.WriteLine("2. Enroll Me In A Course");
                Console.WriteLine("3. View Enrolled in Courses");
                Console.WriteLine("4. Take A Quiz ");
                Console.WriteLine("5. View All Quizzes ");
                Console.WriteLine("6. View Quiz ");
                Console.WriteLine("7. Logout ");

                StudentList choice = (StudentList)Tools.ReadIntInRange("Choose an option: ", 1, 7);

                switch (choice)
                {
                    case StudentList.ViewAllCourses:
                        ViewCoursesAsStudent();
                        break;

                    case StudentList.EnrollInCourse:

                        EnrollStudent(student);
                        break;

                    case StudentList.ViewEnrolledCourse:

                        ViewEnrolledCourses(student);
                        break;

                    case StudentList.TakeQuiz:

                        TakeQuiz(student);
                        break;

                    case StudentList.ViewAllQuizzes:

                        ViewTakenQuizzes(student);
                        break;

                    case StudentList.ViewQuiz:

                        ViewQuiz(student);
                        break;

                    case StudentList.Logout:

                        token = null;

                        break;
                }
            }
        }


        private void AddCourse(Admin admin, List <Course> courses)
        {
            string name = Tools.ReadNonEmptyLine("Enter Name: ");

            string category = Tools.ReadNonEmptyLine("Enter Category: ");

            TimeSpan duration = Tools.ReadDuration("Enter Duration: ");

            int no_OfLessons = Tools.ReadPositiveInt("Enter Number of Lessons: ");


            bool result = admin.AddCourse(courses, name, category, duration, no_OfLessons, token);

            Console.WriteLine( result ? Messages.GetSuccessMessage(SuccessKey.CourseAdded, name) : Messages.GetErrorMessage(ErrorKey.CourseCreationFailed));
        }

        private void AssignTeacher(Teacher teacher)
        {
            string name = Tools.ReadNonEmptyLine("Enter Course Name: ");
            Course course = null;

            foreach(Course courseElement in courses)
            {
                if(courseElement.Name == name)
                {
                    course = courseElement;
                    break;
                }
            }

            if (course is null)
            {

            Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            if(course.Teacher is not null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.TeacherAlreadyAssigned));
                return;
            }


                teacher.AssignToCourse(course);
                Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.CourseAssignedSuccessfully));

        }


        private void ViewAssignedCourses(List<Course> courses , Teacher teacher)
        {

            List<Course> assignedCourses =  teacher.ViewAssignedCourses(courses);

            if(assignedCourses.Count == 0)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoAssignedCourses));
                return;
            }

            foreach (Course course in assignedCourses)
            {
                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Category: {course.Category}");
                Console.WriteLine($"Duration: {course.Duration}");
                Console.WriteLine($"No. of Lessons: {course.NoOfLessons}");
                Console.WriteLine($"No. of Quizzes: {course.Quizzes.Count}");

            }
        }


        private void ViewUnAssignedCourses(List<Course> courses, Teacher teacher)
        {

            List<Course> unAssignedCourses = teacher.ViewUnAssignedCourses(courses);

            if (unAssignedCourses.Count == 0)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoUnAssignedCourses));
                return;
            }

            foreach (Course course in unAssignedCourses)
            {
                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Category: {course.Category}");
                Console.WriteLine($"Duration: {course.Duration}");
            }
        }



        private void ReleaseTeacher(Teacher teacher)
        {
            string name = Tools.ReadNonEmptyLine("Enter Course Name: ");
            Course course = null;

            foreach (Course courseElement in courses)
            {
                if (courseElement.Name == name && courseElement.Teacher ==teacher)
                {
                    course = courseElement;
                    break;
                }
            }

            if (course is null)
            {

                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            if (course.Teacher is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseHasNoTeacher));
                return;
            }

            if(course.Students.Count > 0)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseHasStudents));
                return;
            }


            teacher.ReleaseCourse(course);
            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.CourseReleasedSuccessfully));

        }


        private void MakeQuiz(Teacher teacher)
        {
            string name = Tools.ReadNonEmptyLine("Enter Course Name: ");
            Course course = null;

            List<Question> questions = new List<Question>();

            foreach (Course courseElement in courses)
            {
                if (courseElement.Name == name && courseElement.Teacher == teacher)
                {
                    course = courseElement;
                    break;
                }
            }

            if (course is null)
            {

                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }
            
            string quizName;

            while (true)
            {
                quizName = Tools.ReadNonEmptyLine("Enter Quiz Name: ").Trim().ToLower();

                bool isTaken = false; 

                foreach (Quiz quiz in course.Quizzes)
                {
                    if (quiz.Name.ToLower() == quizName)
                    {
                        Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizNameTaken));
                        isTaken = true;
                        break;
                    }
                }


                if (isTaken == false) break;
            }
      
            TimeSpan duration = Tools.ReadDuration("Enter Duration: ");

            Console.WriteLine("\n--------------| Create Questions ----------------------------------");


            AddQuestionMenu status = AddQuestionMenu.AddAnother;

            AddQuestionMenu choice;
            
            while (status != AddQuestionMenu.Finish)
            {
            
                Question question = CreateQuestion();


                questions.Add(question);
               
                Console.WriteLine("1. Add Another");
                Console.WriteLine("2. Finish");

                choice = (AddQuestionMenu)Tools.ReadIntInRange("Enter Choice Number: ", (int)AddQuestionMenu.AddAnother, (int)AddQuestionMenu.Finish);

                if (choice == AddQuestionMenu.AddAnother)
                {
                    continue;
                }

                if(choice == AddQuestionMenu.Finish)
                {
                       
                        status = AddQuestionMenu.Finish;
                       
                }
                    

                }

            teacher.CreateQuiz(course, quizName, duration, questions);

            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.QuizCreatedSuccessfully , quizName));

        }



        private Question CreateQuestion()
        {

            Console.WriteLine("1. Short Answer");
            Console.WriteLine("2. True / False");
            Console.WriteLine("3. Multiple Choice");

            QuestionType choice = (QuestionType)Tools.ReadIntInRange("Enter Choice Number: " , (int)QuestionType.ShortAnswer, (int)QuestionType.MultipleChoice);

            Question question = null;

            string description = Tools.ReadNonEmptyLine("Enter Question: ");

            switch (choice)
            {
                case QuestionType.ShortAnswer:

                    string answer = Tools.ReadNonEmptyLine("Enter Answer: ");

                    question = ShortAnswer.CreateQuestion(description, answer);
                    break;

                case QuestionType.TrueFalse:

                    while ((true))
                    {

                     answer = Tools.ReadNonEmptyLine("Enter Answer: ");

                        answer = TrueFalse.ValidateAnswer(answer);

                        if (answer == string.Empty)
                        {
                            Console.WriteLine(Messages.GetErrorMessage(ErrorKey.InvalidAnswer));

                            continue;
                        }

                        question = TrueFalse.CreateQuestion(description, answer);

                        break; 
                    }

                    break;
                
                case QuestionType.MultipleChoice:

                    int questionsRange = Tools.ReadIntInRange("Enter the options Number: ", 1 , MultipleChoice.maxOptions);

                    List<string> options = new List<string>();

                    for(int i = 0; i <  questionsRange; i++)
                    {
                    
                        string option = Tools.ReadNonEmptyLine($"Enter The Option no. { i + 1} : ");

                        options.Add(option);
                    }

                    foreach(string option in options)
                    {
                        Console.WriteLine((options.IndexOf(option)+1) + "- " + option);
                    }

                    int optionNumber = Tools.ReadIntInRange("Enter The Right Answer Option Number: ", 1, questionsRange);

                    answer = options[optionNumber - 1];

                    question = MultipleChoice.CreateQuestion(description, answer, options);
                    break;
                
            }

            return question;

        }


        private void ViewQuizDetails(Teacher teacher)
        {
            string courseName = Tools.ReadNonEmptyLine("Enter Course Name: ");
            Course course = null;

            foreach (Course courseElement in courses)
            {
                if (courseElement.Name == courseName && courseElement.Teacher == teacher)
                {
                    course = courseElement;
                    break;
                }
            }

            if (course is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            string quizName = Tools.ReadNonEmptyLine("Enter Quiz Name: ");

            Quiz quiz = course.FindQuiz(quizName);

            if (quiz is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizNotFound));
                return;
            }

            Console.WriteLine($"\nQuiz: {quiz.Name}");
            Console.WriteLine($"Duration: {quiz.Duration}");
            Console.WriteLine($"Students who took it: {quiz.CountStudents()}");
            Console.WriteLine("\nQuestions:");

            int number = 1;

            foreach (Question question in quiz.Questions)
            {
                Console.WriteLine($"\n{number}. {question.Description}");

                if (question is MultipleChoice multipleChoice)
                {
                    foreach (string option in multipleChoice.Options)
                    {
                        Console.WriteLine($" - {option}");
                    }
                }

                Console.WriteLine($" Correct answer: {question.Answer}");

                number++;
            }
        }


        private Course FindCourse(string name)
        {
            foreach (Course course in courses)
            {
                if (course.Name.ToLower() == name.Trim().ToLower())
                {
                    return course;
                }
            }

            return null;
        }

        private void PrintCourseDetails(Course course)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine($"Course: {course.Name}");
            Console.WriteLine("Teacher: " + (course.Teacher?.Name ?? Messages.GetErrorMessage(ErrorKey.UnAssignedTeacher)));
            Console.WriteLine($"Duration: {course.Duration}");
            Console.WriteLine($"No. of Lessons: {course.NoOfLessons}");
            Console.WriteLine($"Category: {course.Category}");
            Console.WriteLine($"No. of Quizzes: {course.Quizzes.Count}");
            Console.WriteLine("Quizzes:");

            foreach (Quiz quiz in course.Quizzes)
            {
                Console.WriteLine($" - {quiz.Name}");
            }
        }

        private void EditQuiz(Teacher teacher)
        {
            Course course = FindCourse(Tools.ReadNonEmptyLine("Enter Course Name: "));

            if (course is null || course.Teacher != teacher)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            Quiz quiz = course.FindQuiz(Tools.ReadNonEmptyLine("Enter Quiz Name: "));

            if (quiz is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizNotFound));
                return;
            }

            if (quiz.HasBeenTaken())
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizAlreadyTaken));
                return;
            }

            EditQuizMenu choice = EditQuizMenu.AddQuestion;

            while (choice != EditQuizMenu.Done)
            {
                Console.WriteLine("\n1. Add Question");
                Console.WriteLine("2. Remove Question");
                Console.WriteLine("3. Done");

                choice = (EditQuizMenu)Tools.ReadIntInRange("Choose an option: ", (int)EditQuizMenu.AddQuestion, (int)EditQuizMenu.Done);

                switch (choice)
                {
                    case EditQuizMenu.AddQuestion:

                        teacher.AddQuestionToQuiz(quiz, CreateQuestion());
                        Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.QuestionAdded));
                        break;

                    case EditQuizMenu.RemoveQuestion:

                        RemoveQuizQuestion(teacher, quiz);
                        break;
                }
            }
        }

        private void RemoveQuizQuestion(Teacher teacher, Quiz quiz)
        {
            List<Question> questions = quiz.Questions;

            if (questions.Count == 1)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CannotRemoveLastQuestion));
                return;
            }

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i].Description}");
            }

            int number = Tools.ReadIntInRange("Enter Question Number: ", 1, questions.Count);

            teacher.RemoveQuestionFromQuiz(quiz, number - 1);
            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.QuestionRemoved));
        }

        private void ViewCoursesAsStudent()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoCourses));
                return;
            }

            foreach (Course course in courses)
            {
                PrintCourseDetails(course);
            }
        }

        private void EnrollStudent(Student student)
        {
            Course course = FindCourse(Tools.ReadNonEmptyLine("Enter Course Name: "));

            if (course is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            if (course.Teacher is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.UnAssignedTeacher));
                return;
            }

            if (student.Courses.Contains(course))
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.StudentAlreadyEnrolled));
                return;
            }

            student.EnrollInCourse(course);
            Console.WriteLine(Messages.GetSuccessMessage(SuccessKey.EnrolledSuccessfully, course.Name));
        }

        private void ViewEnrolledCourses(Student student)
        {
            List<Course> enrolled = student.Courses;

            if (enrolled.Count == 0)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoEnrolledCourses));
                return;
            }

            foreach (Course course in enrolled)
            {
                PrintCourseDetails(course);
            }
        }

        private void ViewTakenQuizzes(Student student)
        {
            bool found = false;

            foreach (Course course in student.Courses)
            {
                foreach (Quiz quiz in course.Quizzes)
                {
                    List<string> summaries = student.GetAttemptSummaries(quiz);

                    if (summaries.Count == 0)
                    {
                        continue;
                    }

                    found = true;
                    Console.WriteLine($"\nQuiz: {quiz.Name} (Course: {course.Name})");

                    foreach (string summary in summaries)
                    {
                        Console.WriteLine($"Attempt: {summary}");
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoQuizzesTaken));
            }
        }

        private void ViewQuiz(Student student)
        {
            Course course = FindCourse(Tools.ReadNonEmptyLine("Enter Course Name: "));

            if (course is null || !student.Courses.Contains(course))
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            Quiz quiz = course.FindQuiz(Tools.ReadNonEmptyLine("Enter Quiz Name: "));

            if (quiz is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizNotFound));
                return;
            }

            bool found = false;

            foreach (Attempt attempt in quiz.Attempts)
            {
                if (attempt.Student != student)
                {
                    continue;
                }

                found = true;

                Console.WriteLine($"\nQuiz: {quiz.Name}");
                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Quiz Duration: {quiz.Duration}");
                Console.WriteLine($"Time Taken: {attempt.ActualDuration}");
                Console.WriteLine($"Score: {attempt.AttemptSummary()}");

                int number = 1;

                foreach (AttemptAnswer attemptAnswer in attempt.Answers)
                {
                    string studentAnswer = "(_No Answer_)";

                    Question question = attemptAnswer.Question;

                    if(!string.IsNullOrEmpty(attemptAnswer.StudentAnswer))
                    {
                        studentAnswer = attemptAnswer.StudentAnswer;
                    }

                    Console.WriteLine($"\n{number}. {question.Description}");
                    Console.WriteLine($"Your answer: {studentAnswer}");
                    Console.WriteLine($"Correct answer: {question.Answer}");

                    number++;
                }
            }

            if (!found)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.NoQuizzesTaken));
            }
        }

        private void TakeQuiz(Student student)
        {
            Course course = FindCourse(Tools.ReadNonEmptyLine("Enter Course Name: "));

            if (course is null || !student.Courses.Contains(course))
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.CourseNotFound));
                return;
            }

            Quiz quiz = course.FindQuiz(Tools.ReadNonEmptyLine("Enter Quiz Name: "));

            if (quiz is null)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.QuizNotFound));
                return;
            }

            List<Question> questions = quiz.Questions;
            List<AttemptAnswer> answers = new List<AttemptAnswer>();
            foreach(Question question in questions)
            {
                answers.Add(new AttemptAnswer(question, null));
            }
            DateTime startTime = DateTime.Now;

            bool finishedInTime = AskQuestions(quiz, questions, startTime, 0, answers);

            TimeSpan elapsed = DateTime.Now - startTime;
            TimeSpan actualDuration = elapsed > quiz.Duration ? quiz.Duration : elapsed;

            Attempt attempt = quiz.Submit(student, answers, startTime, actualDuration);

            if (!finishedInTime)
            {
                Console.WriteLine(Messages.GetErrorMessage(ErrorKey.TimeUp));
            }

            Console.WriteLine($"\nResult: {attempt.AttemptSummary()}");
        }

        private bool AskQuestions(Quiz quiz, List<Question> questions, DateTime startTime, int index, List<AttemptAnswer> answers)
        {
            if (index >= questions.Count)
            {
                return true;
            }

            TimeSpan remaining = quiz.Duration - (DateTime.Now - startTime);

            if (remaining <= TimeSpan.Zero)
            {

                return false;
            }

            Question question = questions[index];

            Console.WriteLine($"\nTime remaining: {(int)remaining.TotalSeconds} seconds");
            Console.WriteLine($"Question {index + 1} of {questions.Count}: {question.Description}");

            if (question is MultipleChoice multipleChoice)
            {
                for (int i = 0; i < multipleChoice.Options.Count; i++)
                {
                    Console.WriteLine($" {i + 1}- {multipleChoice.Options[i]}");
                }
            }

            if (question is TrueFalse)
            {
                Console.WriteLine(" (true / false)");
            }

            Console.Write("Your answer (press Enter to skip): ");
            string answer = Console.ReadLine();

            if (question is MultipleChoice choiceQuestion && int.TryParse(answer, out int optionNumber) && optionNumber >= 1 && optionNumber <= choiceQuestion.Options.Count)
            {
                answer = choiceQuestion.Options[optionNumber - 1];
            }
            remaining = quiz.Duration - (DateTime.Now - startTime);

            if (remaining <= TimeSpan.Zero)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(answer) is false)
            {
                AttemptAnswer studentAnswer = new AttemptAnswer(question, answer);

                answers[index] = studentAnswer;
            }
                
            return AskQuestions(quiz, questions, startTime, index + 1, answers);
        }

        

    }
}
