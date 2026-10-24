using Quiz_Exam_System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal abstract class User
    {
        protected int id;
        protected string _name;
        protected string _email;
        protected string _password;

        public int Id { get => id; }
      
        public string Name
        {
            get => _name;

            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.NameMandatory)); 
                }

                _name = value.Trim();
            }
        }

        public string Email
        {
            get => _email;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.EmailMandatory));
                }

                _email = value;
            }
        }



        protected User(string name, string email,string password)
        {
            Name = name;
            Email = email;

            if (string.IsNullOrWhiteSpace(password))
             {
             throw new ArgumentException(Messages.GetErrorMessage(ErrorKey.PasswordMandatory));
             }
                _password = password;
        }

        public bool VerifyPassword(string password)
        {
            if (_password.Equals(password)) return true;
            
            return false;
        } 

    }
}
