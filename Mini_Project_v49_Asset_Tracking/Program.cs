using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Runtime.ConstrainedExecution;

namespace Mini_Project_v49_Asset_Tracking
{
    class Program
    {
        public static List<Asset> AssetList = new List<Asset>(); // Creates list that holds all assets
        public static bool runProgram = true;

        static void Main(string[] args)
        {
            Asset.addSampleData(); // Loads sample data with different assets

            while (runProgram)
            {
                Helpers.PrintMenu(); // Shows menu with choices for user to perform

                switch (Helpers.Simplify(Console.ReadLine())) // Based on user's choice - do different things

                {
                    case "1":

                        Asset.AddNewAsset(); // Adds new asset to the list
                        break;

                    case "2":
                        Asset.ShowAssetList(); // Shows the complete list with all assets
                        break;

                    case "3":

                        Helpers.ExitProgram(); // Closes the program
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You must enter a number between 1-3");
                        Console.ResetColor();
                        break;
                }

            }

        }
    }
}
