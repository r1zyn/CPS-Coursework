/// <summary>Third task.</summary>
namespace TaskThree
{
    /// <summary>Task three program.</summary>
    public class Program
    {
        /// <summary> Task three entry point.</summary>
        public void Run()
        {
            int wallAmount = 0; // Number of walls
            double totalArea = 0; // Total area of all the walls
            int totalBoxes = 0; // Total number of boxes required
            double totalCost = 0; // The overall cost of all the tiles
            double wastage = 0; // The wastage percentage as a decimal
            double wastedArea = 0; // The wasted tile area

            /// <summary>Tile information (description and prices).</summary>
            Dictionary<string, double> tileInformation = new Dictionary<string, double>()
            {
                { "Small black granite", 19.50 },
                { "Small grey marble", 25.95 },
                { "Small power blue", 35.75 },
                { "Medium sunset yellow", 12.50 },
                { "Medium berry red", 11.00 },
                { "Medium glitter purple", 52.95 },
                { "Large oak wood effect", 65.00 },
                { "Large black granite", 58.98 },
                { "Large bamboo effect", 85.00 },
                { "Extra-large white marble", 62.75 }
            };

            initTiles();

            /// <summary>Displays the tile information and prompts the user to input data needed for calculations.</summary>
            void initTiles()
            {
                // Display the available tiles
                Console.WriteLine("List of available tiles: ");
                Console.WriteLine("+-----+--------------------------+-------------+");
                Console.WriteLine("| ID  | Tile Description         | Tile Price  |");
                Console.WriteLine("+-----+--------------------------+-------------+");

                for (int i = 0; i < tileInformation.Count; i++)
                {
                    Constants.Tiles.Add(new Tile((i + 1).ToString().PadLeft(3, '0'), tileInformation.ElementAt(i).Key, tileInformation.ElementAt(i).Value));
                    Console.WriteLine($"| {Constants.Tiles[i].Id} |" + " " + Constants.Tiles[i].Description.PadRight("-------------------------".Length) + "|" + " " + $"${Constants.Tiles[i].Price.ToString("0.00")}".PadRight("------------".Length) + "|");
                }

                Console.WriteLine("+-----+--------------------------+-------------+");
                Console.WriteLine();


                // Tile customisation
                Console.WriteLine("Tile Customisation");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Enter the number of walls to be tiled: ");
                bool wallAmountIsInt = int.TryParse(Console.ReadLine(), out wallAmount);

                while (!wallAmountIsInt)
                {
                    Console.Write("Enter a valid number of walls (must be an integer): ");
                    wallAmountIsInt = int.TryParse(Console.ReadLine(), out wallAmount);
                }

                Console.Write("Enter a tile wastage percentage (0-100): ");
                bool wastageIsDouble = double.TryParse(Console.ReadLine(), out wastage);

                while (!wastageIsDouble)
                {
                    Console.Write("Enter a valid wastage percentage (must be a number): ");
                    wastageIsDouble = double.TryParse(Console.ReadLine(), out wastage);
                }

                while (wastage < 0 || wastage > 100)
                {
                    Console.Write("Enter a valid wastage percentage (must be between 0 and 100): ");
                    wastageIsDouble = double.TryParse(Console.ReadLine(), out wastage);

                    while (!wastageIsDouble)
                    {
                        Console.Write("Enter a valid wastage percentage (must be a number): ");
                        wastageIsDouble = double.TryParse(Console.ReadLine(), out wastage);
                    }
                }

                wastage = wastage / 100;
                Console.WriteLine();

                for (int i = 0; i < wallAmount; i++)
                {
                    int height;
                    int width;
                    double area;
                    int requiredBoxes;
                    double cost;
                    Tile selectedTile;

                    Console.WriteLine($"Tile No.{i + 1}");
                    Console.WriteLine("------------------------------------------------");

                    Console.Write("Enter the ID of the tile you would like to use: ");
                    string tileId = Console.ReadLine()!;

                    while (!Constants.Tiles.Where(t => t.Id == tileId).Any())
                    {
                        Console.Write("Please enter a valid tile ID: ");
                        tileId = Console.ReadLine()!;
                    }

                    selectedTile = Constants.Tiles.Where(t => t.Id == tileId).First();

                    Console.Write("Enter the height of the wall (metres): ");
                    bool wallIsInt = int.TryParse(Console.ReadLine(), out height);

                    while (!wallIsInt)
                    {
                        Console.Write("Please enter a valid height (must be an integer): ");
                        wallIsInt = int.TryParse(Console.ReadLine(), out height);
                    }

                    Console.Write("Enter the width of the wall (metres): ");
                    bool widthIsInt = int.TryParse(Console.ReadLine(), out width);

                    while (!widthIsInt)
                    {
                        Console.Write("Please enter a valid width (must be an integer): ");
                        widthIsInt = int.TryParse(Console.ReadLine(), out width);
                    }

                    area = height * width;
                    wastedArea = area * wastage;
                    area = area - wastedArea;
                    requiredBoxes = (int)Math.Abs(area / 1);
                    cost = selectedTile.Price * requiredBoxes;

                    totalArea += area;
                    totalBoxes += requiredBoxes;
                    totalCost += cost;

                    Constants.Walls.Add(new Wall(area, selectedTile));
                    Console.WriteLine();
                }


                Console.WriteLine();
                Console.WriteLine("Tile Information");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Total wall area: {Math.Round(totalArea, 2)}m\u00B2 (1dp)");
                Console.WriteLine($"Total number of boxes required: {totalBoxes}");
                Console.WriteLine($"Total cost: ${totalCost.ToString("0.00")}");
            }
        }
    }

    /// <summary> Represents list constants to be altered throughout the program.</summary>
    public class Constants
    {
        /// <summary>List of tiles available.</summary>
        public static List<Tile> Tiles = new List<Tile>();

        /// <summary>List of walls selected by the user.</summary>
        public static List<Wall> Walls = new List<Wall>();
    }

    /// <summary>Structure for a tile.</summary>
    public class Tile
    {
        /// <summary>The ID of the tile.</summary>
        public string Id;

        /// <summary>The description of the tile</summary>
        public string Description;

        /// <2summary>The price of a box of the tile.</sumary>
        public double Price;

        /// <summary>Tile class constructor to create a new instance of the Tile class.</summary>
        public Tile(string id, string description, double price)
        {
            this.Id = id;
            this.Description = description;
            this.Price = price;
        }
    }

    /// <summary>Structure for a wall.</summary>
    public class Wall
    {
        /// <summary>The area of the wall.</summary>
        public double Area;

        /// <summary>The tile type assigned to the wall.</summary>
        public Tile Tile;

        /// <summary>The total cost of tiles for the wall.</summary>
        public double TotalCost;

        public Wall(double area, Tile tile)
        {
            this.Area = area;
            this.Tile = tile;
            this.TotalCost = Constants.Tiles.Where(t => t.Id == tile.Id).First().Price * (this.Area / 1);
        }
    }
}