namespace TaskOne
{
    public class Program
    {
        public void Run()
        {
            Tile selectedTile; // The selected tile
            int height; // The wall height
            int width; // The wall width 
            int area; // The wall area
            int requiredBoxes; // Number of required tile boxes
            double totalCost; // Total cost of tiling walls

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
                Console.WriteLine("List of available tiles: ");
                Console.WriteLine("+-----+--------------------------+-------------+");
                Console.WriteLine("| ID  | Tile Description         | Tile Price  |");
                Console.WriteLine("+-----+--------------------------+-------------+");

                for (int i = 0; i < tileInformation.Count; i++)
                {
                    Constants.Tiles[i] = new Tile((i + 1).ToString().PadLeft(3, '0'), tileInformation.ElementAt(i).Key, tileInformation.ElementAt(i).Value);
                    Console.WriteLine($"| {Constants.Tiles[i].Id} |" + " " + Constants.Tiles[i].Description.PadRight("-------------------------".Length) + "|" + " " + $"${Constants.Tiles[i].Price.ToString("0.00")}".PadRight("------------".Length) + "|");
                }

                Console.WriteLine("+-----+--------------------------+-------------+");
                Console.WriteLine();

                Console.WriteLine("Tile Customisation");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Enter the ID of the tile you would like to use: ");
                string tileId = Console.ReadLine()!;

                while (!Constants.Tiles.Where(t => t.Id == tileId).Any())
                {
                    Console.Write("Please enter a valid tile ID:");
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
                requiredBoxes = area / 1;
                totalCost = selectedTile.Price * requiredBoxes;

                Console.WriteLine();
                Console.WriteLine("Tile Customisation Settings");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Wall area: {area}m\u00B2");
                Console.WriteLine($"Number of boxes required: {requiredBoxes}");
                Console.WriteLine($"Total cost: ${totalCost.ToString("0.00")}");
            }

        }
    }

    /// <summary> Represents list constants to be altered throughout the program.</summary>
    public class Constants
    {
        /// <summary>List of tiles available.</summary>
        public static Tile[] Tiles = new Tile[10];
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
        /// <summary>The height of the wall.</summary>
        public int Height;

        /// <summary>The width of the wall.</summary>
        public int Width;

        /// <summary>The area of the wall.</summary>
        public int Area;
        
        /// <summary>The id of the tile type assigned to the wall.</summary>
        public string TileId;

        /// <summary>The total cost of tiles for the wall.</summary>
        public double TotalCost;

        public Wall(int height, int width, string tileId)
        {
            this.Height = height;
            this.Width = width;
            this.Area = height * width;
            this.TileId = tileId;
            this.TotalCost = Constants.Tiles.Where(t => t.Id == tileId).First().Price * (this.Area / 1);
        }
    }

}