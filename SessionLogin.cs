using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class SessionLogin
    {

        public static LoginResult Login(Dictionary<string, User> usersLog , string email, string password)
        {
            var loginresult = new LoginResult();
                            
            User user;


            loginresult.State = LoginState.EmailNotExist;

            if (string.IsNullOrWhiteSpace(email) == false)
            {
            
                email = Tools.CleanUpEmail(email);
            
                if (usersLog.TryGetValue(email, out user))
                {

                    if (user.VerifyPassword(password) == false)
                    {

                        loginresult.State = LoginState.NotCorrectPassword;

                    }
                    else
                    {
                        loginresult.State = LoginState.Success;

                        loginresult.Token = new Token(user);
                    }

                }
                

            }

            return loginresult;
        }

    }
}
