using ConsoleTables;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;

namespace Space_Adventure_Game
{
    internal class Program
    {
        //Control when to stop the teleprompter
        static bool stopTeleprompter = false;

        static List<Planet>? planetsList;
        static List<Cargo>? cargoItemsList;
        static List<SpaceShip>? shipsList;
        static Dictionary<(string, string), (int fuelUnits, double medicalCrates, double foodCrates)> travelRequirements;

        static void Main(string[] args)
        {
            //Initialize objects
            InitializeGameObjects();

            //Print the intro screen
            IntroScreen();

            //Print the game's main screen
            GameScreen();
            Console.ReadLine();
        }


        #region Main Logic Methods

        /// <summary>
        /// Create and initialize game objects
        /// </summary>
        static void InitializeGameObjects()
        {
            planetsList = new List<Planet>()
            {
                new Planet("Earth", true),
                new Planet("Mars", false),
                new Planet("Neptune", true),
                new Planet("Jupiter", true)
            };

            cargoItemsList = new List<Cargo>()
            {
                new Cargo("Food Supplies Crate", 200),
                new Cargo("Fuel Tank", 50),
                new Cargo("Medical Supplies crate", 300),
                new Cargo("Weapons Box", 300),
                new Cargo("Ammunition Box", 150)
            };

            shipsList = new List<SpaceShip>()
            {
                new SpaceShip("Explorer", 60, 150, 1200, planetsList[0]),
                new SpaceShip("Voyager", 45, 200, 1050, planetsList[2]),
                new SpaceShip("Combatant", 100, 100, 900, planetsList[1]),
                new SpaceShip("Pioneer", 40, 150, 1000, planetsList[3]),
            };

            travelRequirements = new Dictionary<(string, string), (int, double, double)>
            {
                { ("Earth", "Mars"), (200, 0.5, 0.5) },
                { ("Mars", "Earth"), (200, 0.5, 0.5) },
                { ("Earth", "Jupiter"), (400, 1.0, 1.0) },
                { ("Jupiter", "Earth"), (400, 1.0, 1.0) },
                { ("Mars", "Jupiter"), (350, 0.8, 0.7) },
                { ("Jupiter", "Mars"), (350, 0.8, 0.7) },
                { ("Earth", "Neptune"), (550, 1.5, 2.0) },
                { ("Neptune", "Earth"), (550, 1.5, 2.0) },
                { ("Jupiter", "Neptune"), (450, 1.0, 1.5) },
                { ("Neptune", "Jupiter"), (450, 1.0, 1.5) },
                { ("Mars", "Neptune"), (500, 1.2, 1.8) },
                { ("Neptune", "Mars"), (500, 1.2, 1.8) }
            };

            //Create cargo quantity for the items
            Random random = new Random();          

            foreach (var planet in planetsList)
            {          
                foreach (var item in cargoItemsList)
                {
                    //Assign random number of items to the planets
                    int cargoCount = random.Next(9, 15);

                    //if the planet is Mars the fuel stock should 0 since there is no fuel station                    
                    if(planet.Name == "Mars" && item.Name == "Fuel Tank")
                    {
                        continue;
                    }
                    planet.AddCargo(new Cargo(item.Name, item.Weight), cargoCount);

                }
            }
        }

        /// <summary>
        /// Creates and prints the intro screen
        /// </summary>
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
            foreach (var line in gameTitle)
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
        /// Create home screen
        /// </summary>
        static void GameScreen()
        {
            Console.Clear();
            PrintScreenHeader();

            //get the ships menu
            MainMenu();
        }

        /// <summary>
        /// Creates the ships menu handles the options
        /// </summary>
        static void MainMenu()
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                PrintScreenHeader();
                Console.WriteLine("\nWELCOME!\n");

                //Print the menu options
                for (int i = 0; i < shipsList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.- {shipsList[i].Name}");
                }
                Console.WriteLine($"{shipsList.Count + 1}.- View Ships Stats");
                Console.WriteLine($"{shipsList.Count + 2}.- Quit Game");

                int shipOption = ValidateOption("\nChoose your Spaceship: ", 1, shipsList.Count + 2);

                switch (shipOption)
                {
                    case int option when option <= shipsList.Count:
                        //Request the name of the player
                        Console.Write("What is the name of the captain (optional): ");
                        string captainName = Console.ReadLine();

                        HandleSelectedShipOption(shipOption, captainName);

                        break;

                    case int option when option <= shipsList.Count + 1:

                        DisplayAllShipsStats();
                        Console.Write("Press any key to go back to main menu");
                        Console.ReadKey();

                        break;

                    case int option when option <= shipsList.Count + 2:

                        QuitGame();
                        isPlaying = false;

                        break;
                }
            }
        }

        /// <summary>
        /// Display the stats for all available ships
        /// </summary>
        static void DisplayAllShipsStats()
        {
            Console.Clear();
            PrintScreenHeader();
            Console.WriteLine("Ships Stats Page\n");

            //Print all the stats for available ships
            var table = new ConsoleTable("Name", "Fuel Lvl", "Max Cargo", "Current Cargo", "Location");
            foreach (var ship in shipsList)
            {
                table.AddRow(
               (ship.Name).PadRight(10),
               ship.Fuel + "/" + ship.MaxFuelCapacity,
               ship.CargoCapacity,
               ship.CargoList.Count == 0 ? "Empty" : ship.CargoList.Count,
               ship.Location.Name);
            }
            table.Write(Format.Minimal);           
        }

        /// <summary>
        /// Handle the ship selection
        /// </summary>
        /// <param name="shipOption"></param>
        /// <param name="captainName"></param>
        static void HandleSelectedShipOption(int shipOption, string captainName)
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                PrintScreenHeader();

                //Print the chosen ship stats and request player to chose the destination
                Console.WriteLine($"\nHello Captain {captainName}, you have chosen the {shipsList[shipOption - 1].Name}.");
                Console.WriteLine($"\nThese are your current ship stats:\n");

                ShipSpecs(shipOption);

                Console.WriteLine("Ready for an intergalactic trip?");

                //Print ships menu list            
                Console.WriteLine("\n");
                for (int i = 0; i < planetsList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.- {planetsList[i].Name}");
                }
                Console.WriteLine($"{planetsList.Count + 1}.- View Routes and Requirements");
                Console.WriteLine($"{planetsList.Count + 2}.- Back to Main Menu");
                Console.WriteLine($"{planetsList.Count + 3}.- Quit Game");

                int destinationOption = ValidateOption("\nChoose your destination: ", 1, planetsList.Count + 3);

                switch (destinationOption)
                {
                    case int option when option <= planetsList.Count:

                        HandleDestinationOption(shipOption, destinationOption);
                        Console.ReadLine();

                        break;

                    case int option when option == planetsList.Count + 1:

                        DisplayAllRoutesAndRequirements();
                        Console.Write("Press any key to go back to main menu");
                        Console.ReadKey();

                        break;

                    case int option when option == planetsList.Count + 2:
                        MainMenu();
                        return;

                    case int option when option == planetsList.Count + 3:
                        QuitGame();
                        isPlaying = false;

                        break;
                }
            }

        }

        /// <summary>
        /// Display the selected ship specs
        /// </summary>
        /// <param name="shipOtion"></param>
        static void ShipSpecs(int shipOtion)
        {
            string[] columnNames = { "Name", "Fuel Lvl", "Max Cargo", "Current Cargo", "Location" };
            var table = new ConsoleTable(columnNames);

            table.AddRow(
                shipsList[shipOtion - 1].Name,
                shipsList[shipOtion - 1].Fuel + "/" + shipsList[shipOtion - 1].MaxFuelCapacity,
                shipsList[shipOtion - 1].CargoCapacity,
                shipsList[shipOtion - 1].CargoList.Count == 0 ? "Empty" : shipsList[shipOtion - 1].CargoList.Count,
                shipsList[shipOtion - 1].Location.Name);

            table.Write(Format.Minimal);            
        }

        /// <summary>
        /// display the selected route travel requirements
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        static bool DisplayTravelRequirements(string start, string destination)
        {
            if (!travelRequirements.ContainsKey((start, destination)))
            {
                Console.Write($"\n\nNo predefined travel requirements for the route {start} to {destination}. Press any key to continue.");
                return false;
            }

            var requirements = travelRequirements[(start, destination)];

            Console.WriteLine($"\nTravel Requirements from {start} to {destination}:");
            Console.WriteLine($"- Fuel: {requirements.fuelUnits} units");
            Console.WriteLine($"- Medical Supplies: {requirements.medicalCrates} crate(s)");
            Console.WriteLine($"- Food Supplies: {requirements.foodCrates} crate(s)");

            return true;
        }

        /// <summary>
        /// Display all current available routes and the requirements
        /// </summary>
        static void DisplayAllRoutesAndRequirements()
        {
            Console.Clear();
            PrintScreenHeader();
            Console.WriteLine("Routes and Requirements Page\n");

            string[] columnNames = { "From", "To", "Fuel Units", "Medical Crates", "Food Crates" };

            var table = new ConsoleTable(columnNames);


            foreach (var route in travelRequirements)
            {
                var (start, destination) = route.Key;
                var (fuelTanks, medicalCrates, foodCrates) = route.Value;

                table.AddRow(start, destination, fuelTanks, medicalCrates, foodCrates);
            }

            table.Write(Format.Minimal);
        }

        /// <summary>
        /// Handle the chosen destination option
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destinationOption"></param>
        static void HandleDestinationOption(int start, int destinationOption)
        {
            bool isPlaying = true;

            while(isPlaying)
            {
                string currentLocation = shipsList[start - 1].Location.Name;
                string destination = planetsList[destinationOption - 1].Name;

                Console.Clear();
                PrintScreenHeader();
                bool isValudRoute = DisplayTravelRequirements(currentLocation, destination);

                if (!isValudRoute)
                {
                    isPlaying = true;
                    return;
                    
                }

                Console.WriteLine("\nYour current ship stats: ");
                ShipSpecs(start);

                Console.WriteLine("Looks like you will need some supplies.\n");                

                Console.WriteLine("1.- Load Fuel");
                Console.WriteLine("2.- Buy Supplies");
                Console.WriteLine("3.- Go to Launch Platform");
                Console.WriteLine("4.- Back to Destinations Menu");
                Console.WriteLine("5.- Back to Main Menu");
                Console.WriteLine("6.- Quit Game");

                int selectedOption = ValidateOption("\nWhat would do you like to do next?: ", 1, 6);

                switch (selectedOption)
                {
                    case 1:
                        if(!shipsList[start - 1].Location.RefuelingStation)
                        {
                            Console.Write($"\nSorry! {shipsList[start - 1].Location.Name} does not have fuel station or fuel supplies. Press any key to continue...");
                            Console.ReadKey();                            
                            break;
                        }
                        GoToFuelStation(start, destinationOption);

                        break;

                    case 2:
                        GoToSuppliesStore(start, destinationOption);

                        break;

                    case 3:
                        GoToLaunchPlatform(start, destinationOption);
                        break;

                    case 4:
                        Console.Write("Press any key to continue...");

                        return;

                    case 5:
                        MainMenu();

                        return;


                    case 6:
                        QuitGame();
                        isPlaying = false;

                        break;
                }

            }

        }

        static void GoToFuelStation(int start, int destinationOption)
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                string currentLocation = shipsList[start - 1].Location.Name;
                string destination = planetsList[destinationOption - 1].Name;

                int fuelLvl = shipsList[start - 1].Fuel;
                int maxFuelLvl = shipsList[start - 1].MaxFuelCapacity;

                if (!travelRequirements.ContainsKey((currentLocation, destination)))
                {
                    Console.WriteLine($"No predefined fuel requirements for the route {start} to {destination}.");
                    return;
                }

                var requirements = travelRequirements[(currentLocation, destination)];
                
                int requiredFuel = requirements.fuelUnits;

                Console.Clear();
                PrintScreenHeader();

                string[] columnNames = { "Fuel Lvl", "Max Fuel Capacity", "Required Fuel" };
                var table = new ConsoleTable(columnNames);

                Console.WriteLine("\n");
                PrintCentered($"{currentLocation.ToUpper()} - F U E L  S T A T I O N\n");
                
                table.AddRow(fuelLvl, maxFuelLvl, $"{requiredFuel}");

                table.Write(Format.Minimal);                

                Console.Write("Press any key to start filling up your tank: ");
                Console.ReadKey();

                Console.WriteLine("\n");
                GetFuel(fuelLvl, maxFuelLvl, requiredFuel, start -1);
                return;

                

            }         
        }

        static void GetFuel(int currentLevel, int maxLevel, int requiredFuel, int ship)
        {            
            int loadedFuel = 0; 

            int totalFuelToLoad = Math.Min(requiredFuel, maxLevel - currentLevel); 
            int steps = 50; 
            double fuelPerStep = (double)totalFuelToLoad / steps;
            int initialProgress = (int)((currentLevel * steps) / maxLevel);
                        
            // Calculate padding to center the bar
            int consoleWidth = Console.WindowWidth;
            int progressBarWidth = steps + 8; 
            int leftPadding = (consoleWidth - progressBarWidth) / 2;

            string barColor = "Red";
            
            PrintCentered("Loading fuel...");
            Console.WriteLine("\n");

            for (int i = initialProgress; i <= steps; i++)
            {
                int currentFuel = currentLevel + (int)(fuelPerStep * i);
                int percentage = (currentFuel * 100) / maxLevel;

               //Change teh progresss bar color according to the fuel level
                if (percentage < 50)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (percentage < 75)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                // Draw the progress bar
                Console.Write("\r" + new string(' ', leftPadding) + "[");
                Console.Write(new string('#', i));
                Console.Write(new string(' ', steps - i));
                Console.Write($"] {percentage}%");

                Thread.Sleep(200); 
            }

            Console.ResetColor();

            //add the loaded fuel to the current ship
            Console.WriteLine("\n");
            PrintCentered("Loading complete.");
            Console.WriteLine("\n");
            loadedFuel = maxLevel - currentLevel;            
            shipsList[ship].Refuel(loadedFuel);


            
            int remainingFuel = requiredFuel - maxLevel;
            if (remainingFuel > 0)
            {                
               Console.WriteLine($"The remaining {remainingFuel} units of can be bought at the supplies store.");
            }
            else
            {
                Console.WriteLine("\n");
                PrintCentered("Loading complete. The tank is now full!");
            }

            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }


        static void GoToSuppliesStore(int start, int destinationOption)
        {
            bool isPlaying = true;

            // Teleprompter settings
            string message = " Dear Travlers, please be reminded that currently Mars does not have refueling station or fuel supplies. If you are heading there . . .STOCK UP! ";
            int delay = 150;


            stopTeleprompter = false;
            // Start the teleprompter in a background thread
            Thread teleprompterThread = new Thread(() => CreateTeleprompter(message, delay));
            teleprompterThread.Start();
            

            while (isPlaying)
            {
                string currentLocation = shipsList[start - 1].Location.Name;
                string destination = planetsList[destinationOption - 1].Name;

                int planetIndex = planetsList.FindIndex(x => x.Name == currentLocation);

                // Render the UI
                Console.Clear();                          
                PrintStoreHeader();
                PrintCentered($"{currentLocation.ToUpper()} - S U P P L I E S\n");

                // Print the available cargo for the current planet
                planetsList[planetIndex].DisplayAvailableCargo();

                int numOfItems = planetsList[planetIndex].AvailableCargo.Count;

                Console.WriteLine($"  {numOfItems + 1 }.- Go back");
                Console.WriteLine($"  {numOfItems + 2 }.- Quit Game");


                // Move the cursor to the correct position for user interaction
                int cursorLine = Console.CursorTop;
                Console.SetCursorPosition(0, cursorLine);

                // Get the user's item selection
                int itemIndex = ValidateOption("\nPlease choose the item you wish to buy: ", 1, planetsList[planetIndex].AvailableCargo.Count + 2);

                int numOfOptions = planetsList[planetIndex].AvailableCargo.Count;
                switch (itemIndex)
                {
                    case int n when n >= 1 && n <= numOfOptions:

                        break;

                    case int n when n == numOfOptions + 1:
                        // Stop the teleprompter 
                        stopTeleprompter = true;
                        teleprompterThread.Join();
                        return;

                    case int n when n == numOfOptions + 2:                              
                        QuitGame();

                        // Stop the teleprompter 
                        stopTeleprompter = true;
                        teleprompterThread.Join();

                        break;                        
                }

                // Move the cursor again for quantity input
                cursorLine = Console.CursorTop;
                Console.SetCursorPosition(0, cursorLine);

                // Get the quantity to buy
                int maxQty = planetsList[planetIndex].AvailableCargo.ElementAt(itemIndex).Value;
                int qty = ValidateOption("Please enter the quantity: ", 1, maxQty);

                Console.WriteLine($"You selected {qty} of {planetsList[planetIndex].AvailableCargo.ElementAt(itemIndex).Key}.");
                Console.WriteLine("Press any key to continue or 'q' to quit.");

                // Ensure the cursor is positioned correctly for input
                cursorLine = Console.CursorTop;
                Console.SetCursorPosition(0, cursorLine);
                string input = Console.ReadLine();


               
            }

            // Stop the teleprompter when the user exits
            stopTeleprompter = true;
            teleprompterThread.Join(); // Wait for the teleprompter thread to stop
        }
   

        static void CreateTeleprompter(string message, int delay)
        {
            Console.CursorVisible = true; // Ensure the cursor remains visible

            // Add padding to the message for smooth scrolling
            string paddedMessage = message.PadLeft(message.Length + Console.WindowWidth, ' ')
                                        .PadRight(message.Length + 2 * Console.WindowWidth, ' ');

            int teleprompterLine = 0; // Reserve the first line for the teleprompter

            while (!stopTeleprompter)
            {
                for (int i = 0; i < paddedMessage.Length - Console.WindowWidth && !stopTeleprompter; i++)
                {
                    // Save the current cursor position
                    int currentCursorLeft = Console.CursorLeft;
                    int currentCursorTop = Console.CursorTop;

                    // Display the teleprompter message
                    Console.SetCursorPosition(0, teleprompterLine);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(paddedMessage.Substring(i, Console.WindowWidth));
                    Console.ResetColor();
                    // Restore the cursor position for user operations
                    Console.SetCursorPosition(currentCursorLeft, currentCursorTop);

                    Thread.Sleep(delay);
                }
            }

            // Clear the teleprompter line
            Console.SetCursorPosition(0, teleprompterLine);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.CursorVisible = true; // Restore cursor visibility
        }


        static void PrintStoreHeader()
        {            
            Console.SetCursorPosition(0, 1);         
            PrintScreenHeader();
            Console.Write("\n");

        }

        static void GoToLaunchPlatform(int start, int destinationOption)
        {
            Console.Clear();
            PrintScreenHeader();

            bool requirmentsMet;
            bool isPlaying = false;

            string initialLocation = shipsList[start -1].Location.Name;
            string Destination = planetsList[destinationOption -1].Name;


          
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning. You don't meet the minimum requirments for the trip from {initialLocation} to {Destination}");
                Console.ResetColor();
                Console.Write("Are you sure you want to continue (y/n)? ");
                string answer = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(answer))
                {
                    Console.WriteLine("Answer cannot be null");
                }
                if (answer.Trim().ToLower() != "y" || answer.Trim().ToLower() != "n")
                {
                    Console.WriteLine("Invalid answer please reply y or n");
                }
                if(answer.Trim().ToLower() == "y")
                {
                    Console.WriteLine("Please proceed at your own risk. Good Luck!!!");
                    StartTrip(start, destinationOption);
                }
          

            static void StartTrip(int start, int destination)
            {
                Console.Clear();
                PrintScreenHeader();

                Console.Write("Prepare for launch, please press any key to continue: ");
                Console.ReadKey();

                LaunchCountdown();

                // Rocket ASCII Art
                string[] rocketArt = new string[]
                {
                "       !",
                "       ^",
                "      / \\",
                "     /___\\",
                "    |=   =|",
                "    |  B  |",
                "    |  V  |",
                "    |  C  |",
                "    |     |",
                "    |     |",
                "   /|##!##|\\",
                "  / |##!##| \\",
                " /  |##!##|  \\",
                "|  / ^ | ^ \\  |",
                "| /  ( | )  \\ |",
                "|/   ( | )   \\|",
                "    ((   ))",
                "   ((  :  ))",
                "    ((   ))",
                "     (( ))",
                "      ( )",
                "       .",
                "       ."
                };

                AnimateRocketCrashInRealTime(rocketArt);


            }


        }

        /// <summary>
        /// Simulate Launch countdown
        /// </summary>
        static void LaunchCountdown()
        {
            int countdownStart = 5;

            Console.Clear();
            PrintScreenHeader();

            string[] rocketArt = 
              {
                "       !",
                "       ^",
                "      / \\",
                "     /___\\",
                "    |=   =|",
                "    |  B  |",
                "    |  V  |",
                "    |  C  |",
                "    |     |",
                "    |     |",
                "   /|##!##|\\",
                "  / |##!##| \\",
                " /  |##!##|  \\",
                "|  / ^ | ^ \\  |",
                "| /         \\ |",
                "|/           \\|",
              };
            DisplayCenteredRocket(rocketArt);


            Console.WriteLine("\nLaunching...");

            // Save the cursor's current position
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            // Countdown loop
            for (int i = countdownStart; i >= 0; i--)
            {
                Console.SetCursorPosition(left, top); 
                PrintCentered($" {i} seconds...   "); 
                Thread.Sleep(1000);
            }

           


            // Final message
            Console.SetCursorPosition(left, top);
            PrintCentered("Liftoff! 🚀          ");
            Thread.Sleep(1000);

        }


        static void AnimateRocketLaunch(string[] rocketArt)
        {
            Console.Clear();

            int rocketHeight = rocketArt.Length; // Height of the rocket
            int consoleHeight = Console.WindowHeight; // Height of the console window
            int consoleWidth = Console.WindowWidth; // Width of the console window
            int startY = consoleHeight - rocketHeight; // Start position for the rocket
            int centerX = (consoleWidth - rocketArt[0].Length) / 2; // Calculate horizontal center based on the longest line

            // Start the animation
            for (int y = startY; y >= -rocketHeight; y--) // Move up until the rocket disappears
            {
                Console.Clear();

                // Print the rocket at the current position
                for (int i = 0; i < rocketArt.Length; i++)
                {
                    int lineY = y + i; // Calculate the line's vertical position

                    if (lineY >= 0 && lineY < consoleHeight) // Print only if the line is within the visible screen
                    {
                        Console.SetCursorPosition(centerX, lineY); // Align the rocket consistently
                        Console.WriteLine(rocketArt[i]);
                    }
                }

                Thread.Sleep(100); // Add delay to simulate smooth animation
            }

            // After animation, display a final message
            Console.Clear();
            Console.WriteLine("\n\nThe rocket has successfully launched into space! 🚀");
            Thread.Sleep(2000); // Pause to allow the user to see the message
        }

      

        static void DisplayCenteredRocket(string[] rocketArt)
        {          
            int consoleHeight = Console.WindowHeight; // Total height of the console
            int consoleWidth = Console.WindowWidth; // Total width of the console
            int rocketHeight = rocketArt.Length; // Height of the rocket
            int rocketWidth = rocketArt[0].Length; // Width of the rocket

            int startY = (consoleHeight - rocketHeight) / 2; // Calculate vertical starting position
            int startX = (consoleWidth - rocketWidth) / 2; // Calculate horizontal starting position

            for (int i = 0; i < rocketArt.Length; i++)
            {
                // Set the cursor position for each line of the rocket
                Console.SetCursorPosition(startX, startY + i);
                Console.WriteLine(rocketArt[i]);
            }
        }


        static void AnimateRocketCrashInRealTime(string[] rocketArt)
        {
            Console.Clear();

            int rocketHeight = rocketArt.Length; // Height of the rocket
            int consoleHeight = Console.WindowHeight; // Height of the console window
            int consoleWidth = Console.WindowWidth; // Width of the console window
            int startY = consoleHeight - rocketHeight; // Start position for the rocket
            int centerX = (consoleWidth - rocketArt[0].Length) / 2; // Calculate horizontal center
            Random random = new Random();

            // Rocket Ascension Animation
            for (int y = startY; y >= 0; y--) // Move up until the top
            {
                Console.Clear();

                // Print the rocket at the current position
                for (int i = 0; i < rocketArt.Length; i++)
                {
                    int lineY = y + i; // Calculate the line's vertical position

                    if (lineY >= 0 && lineY < consoleHeight) // Print only if the line is within the visible screen
                    {
                        Console.SetCursorPosition(centerX, lineY); // Align the rocket consistently
                        Console.WriteLine(rocketArt[i]);
                    }
                }

                Thread.Sleep(100); // Add delay to simulate smooth animation

                // When the rocket reaches the top, initiate the crash
                if (y == 0)
                {
                    Console.Clear();

                    // Display crash explosion
                    Console.SetCursorPosition(centerX, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("💥 BOOM! 💥");
                    Console.ResetColor();

                    // Scatter debris
                    for (int i = 0; i < 50; i++) // Number of debris pieces to scatter
                    {
                        // Randomly scatter debris
                        Console.SetCursorPosition(
                            random.Next(0, consoleWidth), // Random horizontal position
                            random.Next(0, consoleHeight) // Random vertical position
                        );

                        Console.ForegroundColor = (ConsoleColor)random.Next(1, 16); // Random color
                        Console.Write(GetRandomDebris()); // Print a random debris character
                        Thread.Sleep(50); // Small delay between pieces

                        // Display "GAME OVER" in the center after scattering a few debris pieces
                        if (i == 25) // Midway through the debris scattering
                        {
                            Console.SetCursorPosition((consoleWidth - "GAME OVER".Length) / 2, consoleHeight / 2); // Center the message
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("GAME OVER");
                            Console.ResetColor();
                        }
                    }

                    // After scattering, display a final message
                    Console.SetCursorPosition(0, consoleHeight - 1); // Move to the bottom of the screen
                    Console.ResetColor();
                    Console.WriteLine("\n\nThe rocket has crashed, and the pieces have scattered. Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
        }

        // Helper Method: Returns a random debris character
        static char GetRandomDebris()
        {
            char[] debris = new char[] { '/', '\\', '*', '-', '+', '|', 'o', '@', '#', '$', '%' };
            Random random = new Random();
            return debris[random.Next(debris.Length)];
        }







        #endregion 


        #region Helper methods
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

        /// <summary>
        /// Create a main header for all pages
        /// </summary>
        static void PrintScreenHeader()
        {
            //Print screen header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n");
            PrintCentered("***** Space Adventure Game ****");
            Console.Write("\n");
            Console.ResetColor();
            
        }

        /// <summary>
        /// Allow user to quit the game at any time
        /// </summary>
        static void QuitGame()
        {
            Console.Write("Bye, bye thank you for playing!");
            Thread.Sleep(2000); // Pause for 2 seconds
            Environment.Exit(0);
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

                isValidOption = int.TryParse(input, out option) && option >= min && option <= max || option == (max + 1);

                if (!isValidOption)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
                }
            }

            return option;
        }

        


        #endregion





    }
}
