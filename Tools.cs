using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Exam_System
{
    internal static class Tools
    {

        public static string CleanUpEmail(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Error : Invalid Input");
            }

            string CleanedEmail = email.Trim().ToLower();

            return CleanedEmail;
        }


        public static TimeSpan ReadDuration(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (enter 'm' for minutes or 'h' for hours): ");
                string unit = Console.ReadLine().Trim().ToLower();

                if (unit != "m" && unit != "h")
                {
                    Console.WriteLine("Invalid unit. Use 'm' or 'h'.");
                    continue;
                }
                Console.Write("Enter value: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value) || value <= 0)
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.MustBePositive));
                    continue;
                }
                switch (unit)
                {
                    case "m":
                        return TimeSpan.FromMinutes(value);

                    case "h":
                        return TimeSpan.FromHours(value);

                    default:
                        Console.WriteLine("Invalid unit. Use 'm' or 'h'.");
                        break;
                }

            }
        }

        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                string line =
                    Console.ReadLine();

                int value;

                bool parsed =
                    int.TryParse(line, out value);

                if (!parsed)
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.InvalidNumber));

                    continue;
                }

                if (value <= 0)
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.MustBePositive));

                    continue;
                }

                return value;
            }
        }
        public static string ReadNonEmptyLine(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                string line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.FieldRequired));

                    continue;
                }

                return line;
            }
        }

        public static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);

                string line =
                    Console.ReadLine();

                int value;

                bool parsed =
                    int.TryParse(line, out value);

                if (!parsed)
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.InvalidNumber));

                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine(Messages.GetErrorMessage(ErrorKey.OutOfRange, min, max));

                    continue;
                }

                return value;
            }
        }





    }
}
