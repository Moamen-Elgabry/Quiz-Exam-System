using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{

    enum UserType
    {
        Admin,
        Teacher,
        Student
    }

    internal class Token
    {
        private UserType _type;
        private string _name;
        private string _email;
        private DateTime _loggedAt;


        public Token(User user)
        {
            if(user is Admin)
            {
                _type = UserType.Admin;

            }else if(user is Teacher)
            {
                _type = UserType.Teacher;

            }
            else if(user is Student)
            {
                _type = UserType.Student;

            }
            else
            {
                throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.InvalidUser));
            }

            Name = user.Name;
            Email = user.Email;
            LoggedAt = DateTime.Now;
        }

        public UserType Type
        {
            get => _type;
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

                _name = value;
            }
        }

        public string Email
        {
            get => _email;

           private  set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.EmailMandatory));
                }

                _email = value;
            }
        }



        public DateTime LoggedAt
        {
            get => _loggedAt;

           private set
            {
                _loggedAt = value;
            }
        }


    }
}