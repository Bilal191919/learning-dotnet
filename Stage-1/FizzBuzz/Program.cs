using System;

namespace Stage1_LogicBuilding
{
    class Program
    {   
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to the Professional Buzz Game (.NET Edition) ===");
            Console.WriteLine("Rules: Multiples of 3 = buzz, 5 = fizz, both = buzzfizz\n");

            int extraDivisor = 0;
            string extraWord = "";

            try
            {
                Console.Write("Enter an extra divisor (or 0 for none): ");
                string input = Console.ReadLine();
                extraDivisor = int.Parse(input);

                if (extraDivisor != 0)
                {
                    Console.Write("Enter the word for this divisor: ");
                    extraWord = Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input detection: Skipping extra rule.");
                extraDivisor = 0;
            }

            int n = 1;
            bool isRunning = true;

            while (isRunning)
            {
                string correctAnswer = "";

                if (n % 3 == 0) correctAnswer += "buzz";
                if (n % 5 == 0) correctAnswer += "fizz";
                
                if (extraDivisor != 0 && n % extraDivisor == 0)
                {
                    correctAnswer += extraWord;
                }

                if (string.IsNullOrEmpty(correctAnswer))
                {
                    correctAnswer = n.ToString();
                }

                Console.WriteLine("\nTarget Number: " + n);
                Console.Write("Your answer (or 'q' to quit): ");
                string userInp = Console.ReadLine()?.ToLower();

                if (userInp == "q")
                {
                    Console.WriteLine("Thanks for playing! Game Terminated.");
                    isRunning = false;
                }
                else if (userInp != correctAnswer)
                {
                    Console.WriteLine("Wrong! The correct answer was: " + correctAnswer);
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Correct!");
                    n++;
                }
            }
        }
    }
}
