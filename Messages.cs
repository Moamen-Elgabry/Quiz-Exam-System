using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    public enum ErrorKey
    {
        // ---- input validation ----
        FieldRequired,
        InvalidNumber,
        OutOfRange,
        MustBePositive,
        CannotBeNegative,

        // ---- registration and login ----
        NameMandatory,
        EmailMandatory,
        PasswordMandatory,
        TitleMandatory,
        EmailTaken,
        AccountNotFound,
        IncorrectPassword,
        InvalidUser,
        UnAuthorizedAction,

        // ---- courses ----
        CourseMandatory,
        CourseNameMandatory,
        CourseCategoryMandatory,
        DurationMandatory,
        CourseNotFound,
        CourseCreationFailed,
        CourseHasNoTeacher,
        CourseHasStudents,
        NotYourCourse,
        TeacherAlreadyAssigned,
        UnAssignedTeacher,
        NoCourses,
        NoAssignedCourses,
        NoUnAssignedCourses,

        // ---- students and enrollment ----
        StudentRequired,
        StudentAlreadyEnrolled,
        NoEnrolledCourses,

        // ---- quizzes ----
        InvalidDuration,
        QuizNotFound,
        QuizNameTaken,
        QuizAlreadyTaken,
        NoQuizzesTaken,
        TimeUp,

        // ---- questions ----
        QuestionsMandatory,
        QuestionMandatory,
        CannotRemoveLastQuestion,
        DescriptionMandatory,
        AnswerMandatory,
        IncorrectStudentAnswers,
        InvalidAnswer,
        OptionsRequired,
        InvalidRange
    }

    public enum SuccessKey
    {
        Registered,
        CourseAdded,
        CourseAssignedSuccessfully,
        CourseReleasedSuccessfully,
        EnrolledSuccessfully,
        QuizCreatedSuccessfully,
        QuestionAdded,
        QuestionRemoved,
        Goodbye
    }

    internal static class Messages
    {
        private static readonly Dictionary<ErrorKey, string> _errors = new Dictionary<ErrorKey, string>
        {
            // ---- input validation ----
            { ErrorKey.FieldRequired, "Invalid input: field can't be empty." },
            { ErrorKey.InvalidNumber, "Invalid input: please enter a number." },
            { ErrorKey.OutOfRange, "Invalid input: enter a number between {0} and {1}." },
            { ErrorKey.MustBePositive, "Invalid input: value must be greater than zero." },
            { ErrorKey.CannotBeNegative, "Invalid input: value cannot be negative." },

            // ---- registration and login ----
            { ErrorKey.EmailMandatory, "Error: Email is mandatory." },
            { ErrorKey.PasswordMandatory, "Error: Password is mandatory." },
            { ErrorKey.TitleMandatory, "Error: Title is mandatory." },
            { ErrorKey.EmailTaken, "Error: email already exists." },
            { ErrorKey.AccountNotFound, "Error: no account with that email." },
            { ErrorKey.IncorrectPassword, "Error: incorrect password." },
            { ErrorKey.InvalidUser, "Error: invalid user." },
            { ErrorKey.UnAuthorizedAction, "Error: You are not an Admin." },

            // ---- courses ----
            { ErrorKey.CourseMandatory, "Error: course is mandatory." },
            { ErrorKey.CourseNameMandatory, "Error: course name is mandatory." },
            { ErrorKey.CourseCategoryMandatory, "Error: course category is mandatory." },
            { ErrorKey.DurationMandatory, "Error: Duration is mandatory." },
            { ErrorKey.CourseNotFound, "Error: no matching course." },
            { ErrorKey.CourseCreationFailed, "Error: course was not created." },
            { ErrorKey.CourseHasNoTeacher, "Error: course has no teacher." },
            { ErrorKey.CourseHasStudents, "Error: this course has enrolled students and cannot be released." },
            { ErrorKey.NotYourCourse, "Error: this is not your course." },
            { ErrorKey.TeacherAlreadyAssigned, "Error: a teacher is already assigned to this course." },
            { ErrorKey.UnAssignedTeacher, "Teacher has not been assigned yet." },
            { ErrorKey.NoCourses, "Error: there are no courses." },
            { ErrorKey.NoAssignedCourses, "Error: you have no assigned courses." },
            { ErrorKey.NoUnAssignedCourses, "Error: there are no unassigned courses." },

            // ---- students and enrollment ----
            { ErrorKey.StudentRequired, "Error: student is mandatory." },
            { ErrorKey.StudentAlreadyEnrolled, "Error: student is already enrolled in this course." },
            { ErrorKey.NoEnrolledCourses, "Error: you are not enrolled in any course." },

            // ---- quizzes ----
            { ErrorKey.NameMandatory, "Error: name is mandatory." },
            { ErrorKey.InvalidDuration, "Error: duration must be greater than zero." },
            { ErrorKey.QuizNotFound, "Error: no matching quiz." },
            { ErrorKey.QuizNameTaken, "Error: a quiz with this name already exists in this course." },
            { ErrorKey.QuizAlreadyTaken, "Error: this quiz has already been taken and cannot be edited." },
            { ErrorKey.NoQuizzesTaken, "Error: no quizzes have been taken." },
            { ErrorKey.TimeUp, "Time is up! The quiz has ended." },

            // ---- questions ----
            { ErrorKey.QuestionsMandatory, "Error: a quiz must have at least one question." },
            { ErrorKey.QuestionMandatory, "Error: Question is Mandatory" },
            { ErrorKey.CannotRemoveLastQuestion, "Error: a quiz cannot be left without questions." },
            { ErrorKey.DescriptionMandatory, "Error: description is mandatory." },
            { ErrorKey.AnswerMandatory, "Error: answer is mandatory." },
            { ErrorKey.IncorrectStudentAnswers, "Error: Incorrect Student Answers." },
            { ErrorKey.InvalidAnswer, "Invalid input: please enter a valid answer." },
            { ErrorKey.OptionsRequired, "Error: options cannot be empty." },
            { ErrorKey.InvalidRange, "Error: the number of options is out of range." }
        };

        private static readonly Dictionary<SuccessKey, string> _success = new Dictionary<SuccessKey, string>
        {
            { SuccessKey.Registered, "Registration successful." },
            { SuccessKey.CourseAdded, "Course '{0}' added successfully." },
            { SuccessKey.CourseAssignedSuccessfully, "Teacher is assigned to the course successfully." },
            { SuccessKey.CourseReleasedSuccessfully, "Teacher is released from the course successfully." },
            { SuccessKey.EnrolledSuccessfully, "Enrolled in the course successfully: {0}" },
            { SuccessKey.QuizCreatedSuccessfully, "Quiz {0} is created successfully." },
            { SuccessKey.QuestionAdded, "Question added successfully." },
            { SuccessKey.QuestionRemoved, "Question removed successfully." },
            { SuccessKey.Goodbye, "Goodbye!" }
        };

        public static string GetErrorMessage(ErrorKey key, params object[] args)
        {
            return string.Format(_errors[key], args);
        }

        public static string GetSuccessMessage(SuccessKey key, params object[] args)
        {
            return string.Format(_success[key], args);
        }
    }

}