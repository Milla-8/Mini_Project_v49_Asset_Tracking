using System;
using System.Globalization;

namespace Mini_Project_v49_Asset_Tracking
{
    public class Asset
    {
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Configuration { get; set; }
        public string PurchaseDate { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }

        public Asset()
        {
        }

        public static void addSampleData()
        {
            Program.AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 14 Pro", Configuration = "1TB, Deep Purple ", PurchaseDate = "2022-10-19", Price = 14495F, Currency = "SEK", Office = "Sweden" });
            Program.AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 14 Pro Max", Configuration = "256GB, Gold", PurchaseDate = "2022-09-22", Price = 17995F, Currency = "NOK", Office = "Norway" });
            Program.AssetList.Add(new Asset { Category = "Phone", Brand = "Apple", Model = "iPhone 12", Configuration = "128GB, White", PurchaseDate = "2022-02-28", Price = 27595F, Currency = "NOK", Office = "Norway" });
            Program.AssetList.Add(new Asset { Category = "Phone", Brand = "Samsung", Model = "S22 Ultra", Configuration = "256GB, Purple", PurchaseDate = "2022-05-15", Price = 23795F, Currency = "DKK", Office = "Denmark" });
            Program.AssetList.Add(new Asset { Category = "Computer", Brand = "Apple", Model = "MacBook Pro", Configuration = "14 inch, Space Black, M4, 16GB RAM, 512GB SSD", PurchaseDate = "2024-11-29", Price = 22995F, Currency = "SEK", Office = "Sweden" });
            Program.AssetList.Add(new Asset { Category = "Computer", Brand = "Apple", Model = "iMac", Configuration = "24 inch, Silver, M4, 24GB RAM, 512GB SSD ", PurchaseDate = "2024-02-10", Price = 27595F, Currency = "SEK", Office = "Sweden" });
            Program.AssetList.Add(new Asset { Category = "Computer", Brand = "Samsung", Model = "Galaxy Book 4", Configuration = "15,6 inch, Silver, Intel Core, 8GB RAM, 512GB SSD ", PurchaseDate = "2023-04-29", Price = 4995F, Currency = "DKK", Office = "Denmark" });
        }

        public static void ShowAssetList()
        {
            Console.WriteLine("Office\t\tCategory\t\tBrand\t\tModel\t\tConfiguration\t\tPurchaseDate\t\tPrice\t\tCurrency"); // Column names
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (Asset asset in Program.AssetList.OrderBy(x => x.Office).ThenBy(x => DateTime.ParseExact(x.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
            {
                DateTime purchaseDate = DateTime.ParseExact(asset.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime warrantyEndDate = purchaseDate.AddYears(3);

                if (warrantyEndDate.AddMonths(-3) < DateTime.Now) // Asset shows red in asset list if product has less than 3 months left of the 3 year lifecycle
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (warrantyEndDate.AddMonths(-6) < DateTime.Now) // Asset shows yellow in asset list if product has less than 6 months left of the 3 year lifecycle
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

                bool validDate = Helpers.ValidateDate(asset.PurchaseDate);

                while (validDate == false) // Keep asking user to enter the date if entered in wrong format
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You need to enter the purchase date in the following format YYYY-MM-DD ");
                    Console.ResetColor();
                    asset.PurchaseDate = Console.ReadLine();
                    validDate = Helpers.ValidateDate(asset.PurchaseDate);

                    if (validDate) // Continue with creating asset when date is entered correctly
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("This is a valid date!");
                        Console.ResetColor();
                        break;
                    }
                }

                bool validInput = false;

                while (!validInput) // Keep asking user to enter the price if entered in wrong format
                {
                    Console.WriteLine("Type in the price in the following format 0.00: ");
                    string input = Console.ReadLine();

                    input = input.Replace(',', '.');

                    if (float.TryParse(input, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float price))
                    {
                        if (Math.Abs(price * 100 - Math.Round(price * 100)) < 0.01) // Check if price is in correct format
                        {
                            asset.Price = price; // Set price if entered value is valid
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("This is a valid price!");
                            Console.ResetColor();
                            validInput = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // Show error message to user if wrong format
                            Console.WriteLine("You need to enter a value in format 0.00: ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You need to enter a numeric value in format 0.00: ");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("Type in the currency (in real name or short name) :");
                asset.Currency = Console.ReadLine();

                Console.WriteLine("Type in which office this belongs to: ");
                asset.Office = Console.ReadLine();

                Program.AssetList.Add(asset); // Adds the new asset to the asset list

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Product successfully added!");
                Console.ResetColor();

                Console.WriteLine("Would you like to add more products? Y/N");

                if (Helpers.Simplify(Console.ReadLine()) == "N")
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
