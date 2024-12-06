using System;
namespace Mini_Project_v49_Asset_Tracking
{
	public class Helpers
	{
		public Helpers()
		{
		}

        public static string Simplify(string input)
        {
            return input.Trim().ToUpper();
        }

        public static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Asset Tracking!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Add new Asset");
            Console.WriteLine("2: Show list of all assets (sorted on office then purchase date)");
            Console.WriteLine("3: Exit program");
            Console.ResetColor();
        }

        public static bool ValidateDate(string date) // See if user input is a valid date
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime validDate))
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public static void ExitProgram()
        {
            Console.WriteLine("Thank you for today!");
            Program.runProgram = false;
        }
    }

}

