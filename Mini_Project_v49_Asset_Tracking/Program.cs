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
        private static bool runProgram = true;

        static void Main(string[] args)
        {
            addSampleData(); // Loads sample data with different assets

            while (runProgram)
            {
                PrintMenu(); // Shows menu with choices for user to perform

                switch (Simplify(Console.ReadLine())) // Based on user's choice - do different things

                {
                    case "1":

                        AddNewAsset(); // Adds new asset to the list
                        break;

                    case "2":
                        ShowAssetList(); // Shows the complete list with all assets
                        break;

                    case "3":

                        ExitProgram(); // Closes the program
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You must enter a number between 1-3");
                        Console.ResetColor();
                        break;
                }

            }

        }

        private static void addSampleData()
        {
            AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 14 Pro", Configuration = "1TB, Deep Purple ", PurchaseDate = "2022-10-19", Price = 14495F, Currency = "SEK", Office = "Sweden" });
            AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 14 Pro Max", Configuration = "256GB, Gold", PurchaseDate = "2022-09-22", Price = 17995F, Currency = "NOK", Office = "Norway" });
            AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 12", Configuration = "128GB, White", PurchaseDate = "2022-02-28", Price = 27595F, Currency = "NOK", Office = "Norway" });
            AssetList.Add(new Asset { Category = "Phone", Brand = "Samsung", Model = "S22 Ultra", Configuration = "256GB, Purple", PurchaseDate = "2022-05-15", Price = 23795F, Currency = "DKK", Office = "Denmark"});
            AssetList.Add(new Asset { Category = "Computer", Brand = "Apple", Model = "MacBook Pro", Configuration = "14 inch, Space Black, M4, 16GB RAM, 512GB SSD", PurchaseDate = "2024-11-29", Price = 22995F, Currency = "SEK", Office = "Sweden" });
            AssetList.Add(new Asset { Category = "Computer", Brand = "Apple", Model = "iMac", Configuration = "24 inch, Silver, M4, 24GB RAM, 512GB SSD ", PurchaseDate = "2024-02-10", Price = 27595F, Currency = "SEK", Office = "Sweden" });
            AssetList.Add(new Asset { Category = "Computer", Brand = "Samsung", Model = "Galaxy Book 4", Configuration = "15,6 inch, Silver, Intel Core, 8GB RAM, 512GB SSD ", PurchaseDate = "2023-04-29", Price = 4995F, Currency = "DKK", Office = "Denmark" });
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

        public static string Simplify(string input)
        {
            return input.Trim().ToUpper();
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
            runProgram = false;
        }

        public static void ShowAssetList()
        {
            Console.WriteLine("Office\t\tCategory\t\tBrand\t\tModel\t\tConfiguration\t\tPurchaseDate\t\tPrice\t\tCurrency"); // Column names
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (Asset asset in AssetList.OrderBy(x => x.Office).ThenBy(x => DateTime.ParseExact(x.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
            {
                DateTime purchaseDate = DateTime.ParseExact(asset.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime warrantyEndDate = purchaseDate.AddYears(3);

                if (warrantyEndDate.AddMonths(-3) < DateTime.Now) // Shows red in asset list if product has less than 3 months left of the 3 year lifecycle
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (warrantyEndDate.AddMonths(-6) < DateTime.Now) // Shows yellow in asset list if product has less than 3 months left of the 3 year lifecycle
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                };
                Console.Write(asset.Office + "\t\t");
                Console.Write(asset.Category + "\t\t");
                Console.Write(asset.Brand + "\t\t");
                Console.Write(asset.Model + "\t\t");
                Console.Write(asset.Configuration + "\t\t");
                Console.Write(asset.PurchaseDate + "\t\t");
                Console.Write(asset.Price + "\t\t");
                Console.Write(asset.Currency + "\t\t\n");
                Console.ResetColor();
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public static void AddNewAsset() // Creates a new asset based on user input - and then adds it to the asset list
        {

            while (true)
            {
                Asset asset = new Asset();

                Console.Write("Type in the category: ");
                asset.Category = Console.ReadLine();

                Console.Write("Type in the brand: ");
                asset.Brand = Console.ReadLine();

                Console.WriteLine("Type in the model (product name): ");
                asset.Model = Console.ReadLine();

                Console.Write("Type in the configuration: ");
                asset.Configuration = Console.ReadLine();

                Console.WriteLine("Type in the purchase date in the following format YYYY-MM-DD: ");
                asset.PurchaseDate = Console.ReadLine();

                bool validDate = ValidateDate(asset.PurchaseDate);

                if (validDate)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("This is a valid date");
                    Console.ResetColor();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You need to enter the purchase date in the following format YYYY-MM-DD ");
                    Console.ResetColor();
                }


                Console.WriteLine("Type in the price in following format 0.00: ");
                asset.Price = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("Type in the currency (in real name or short name) :");
                asset.Currency = Console.ReadLine();

                Console.WriteLine("Type in which office this belongs to: ");
                asset.Office = Console.ReadLine();

                AssetList.Add(asset); // Adds the new asset to the asset list

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Product successfully added!");
                Console.ResetColor();

                Console.WriteLine("Would you like to add more products? Y/N");

                if (Simplify(Console.ReadLine()) == "N")
                {
                    {
                        ShowAssetList();
                        break;
                    }

                }
            }
        }
    }
}