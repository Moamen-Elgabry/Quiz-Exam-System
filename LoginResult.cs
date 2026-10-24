using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal class LoginResult
    {
        public LoginState State { get; set; }
        public Token Token { get; set; }


        public LoginResult()
        {
            Token = null;
        }

    }
}