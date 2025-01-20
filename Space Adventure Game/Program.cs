using System.Text;

namespace Space_Adventure_Game
{
    internal class Program
    {
        static List<Planet>? planetsList;
        static List<Cargo>? cargoItemsList;
        static List<SpaceShip>? shipsList;

        static void Main(string[] args)
        {
            //Initialize objects
            InitializeGameObjects();

            IntroScreen();            

            MainScreen();
            Console.ReadLine();
        }



        #region helper methods

        /// <summary>
        /// Create home screen
        /// </summary>
        static void MainScreen()
        {
            Console.Clear();
            PrintScreenHeader();            

            //Print ships menu list
            Console.WriteLine("\n\n");
            for (int i = 0; i < shipsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.- {shipsList[i].Name}");
            }

            int shipOption = ValidateOption("\nChoose your Spaceship: ", 1, shipsList.Count);

            Console.Clear();
            PrintScreenHeader();

            Console.WriteLine($"\nHello {shipsList[shipOption-1].Name} your current location is {shipsList[shipOption - 1].Location.Name}");
            
            //Print ships menu list
            Console.WriteLine("\n");
            for (int i = 0; i < shipsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.- {planetsList[i].Name}");
            }

            int destinationOption = ValidateOption("\nChoose your destination: ", 1, planetsList.Count);




        }

        static void IntroScreen()
        {
            string[] gameTitle = new string[]
            {     
                @"",
                @"                                ^                                            ",
                @" _____                         / \      _                 _                  ",
                @"/  ___|                       / _ \    | |               | |                 ",
                @"\ `--. _ __   __ _  ___ ___  / |_| \ __| |_   _____ _ __ | |_ _   _ _ __ ___ ",
                @" `--. \ '_ \ / _` |/ __/ _ \ |  _  |/ _` \ \ / / _ \ '_ \| __| | | | '__/ _ \",
                @"/\__/ / |_) | (_| | (_|  __/ | | | | (_| |\ V /  __/ | | | |_| |_| | | |  __/",
                @"\____/| .__/ \__,_|\___\___| \_| |_/\__,_| \_/ \___|_| |_|\__|\__,_|_|  \___|",
                @"      | |                    /|\ /|\                                         ",
                @"      |_|                                                                    ",

            };

            //Print screen header
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCentered("***** W E L C O M E  T O: *****");
            Console.ResetColor();
                        
            
            Console.ForegroundColor = ConsoleColor.Magenta;  
            foreach(var line in gameTitle)
            {
                PrintCentered(line);
            };
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            PrintCentered("Get ready for an intergalactic adventure!");
            Console.ResetColor();

            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintCentered(">  1  P L A Y E R");
            Console.ResetColor();

            Console.WriteLine("\n");
            PrintCentered("S T A R T  N E W  G A M E");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n");
            PrintCentered("CREDIT  01");
            Console.WriteLine("\n\n");
            Console.ResetColor();

            Console.OutputEncoding = Encoding.UTF8;
            PrintCentered("\u00A9 2025 <\u0414/> Manuel Alva");

            Console.Write("\nPress any key to start...");
            Console.ReadKey();            

        }

        /// <summary>
        /// Create Game objects
        /// </summary>
        static void InitializeGameObjects()
        {
            planetsList = new List<Planet>()
            {
                new Planet("Earth", true),
                new Planet("Mars", false),
                new Planet("Neptuno", true),
                new Planet("Jupiter", false)
            };

            cargoItemsList = new List<Cargo>()
            {
                new Cargo("Food Supplies Crate", 200),
                new Cargo("Fuel Tank", 500),
                new Cargo("Medical Supplies", 300),
                new Cargo("Weapons Box", 600),
                new Cargo("Ammunition Box", 150)
            };

            shipsList = new List<SpaceShip>()
            {
                new SpaceShip("Explorer", 60, 1200, planetsList[0]),
                new SpaceShip("Voyager", 45, 1050, planetsList[2]),
                new SpaceShip("Combatant", 35, 900, planetsList[1]),
                new SpaceShip("Pioneer", 40, 750, planetsList[2]),
            };
        }      

        /// <summary>
        /// validates the user input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Validated menu option</returns>
        static int ValidateOption(string prompt, int min, int max)
        {
            bool isValidOption = false;
            int option = 0;

            Console.Write(prompt);

            while (!isValidOption)
            {
               string input = Console.ReadLine();

                isValidOption = int.TryParse(input, out option) && option >= min && option <= max ;

                if (!isValidOption)
                {
                    Console.WriteLine("Invalid input. Please enter an option from the menu");
                }
            }

            return option;

        }

        /// <summary>
        /// Center text in the x axis
        /// </summary>
        /// <param name="text"></param>
        static void PrintCentered(string text)
        {
            int consoleWidth = Console.WindowWidth;

            int leftPadding = (consoleWidth - text.Length) / 2;

            Console.WriteLine(text.PadLeft(leftPadding + text.Length));
        }

        static void PrintScreenHeader()
        {
            //Print screen header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
            PrintCentered("***** Space Adventure Game ****");
            Console.ResetColor();
        }
        #endregion
    }
}
