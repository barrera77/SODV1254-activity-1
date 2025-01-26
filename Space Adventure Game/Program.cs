using ConsoleTables;
using System;
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
        static string captainName;

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
                new Cargo("Food Supplies Crate", 150),
                new Cargo("Fuel Tank", 50),
                new Cargo("Medical Supplies crate", 50),
                new Cargo("Weapons Box", 300),
                new Cargo("Ammunition Box", 50)
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
                { ("Mars", "Jupiter"), (350, 0.7, 0.5) },
                { ("Jupiter", "Mars"), (350, 0.7, 0.5) },
                { ("Earth", "Neptune"), (550, 1, 2) },
                { ("Neptune", "Earth"), (550, 1, 2) },
                { ("Jupiter", "Neptune"), (450, 1.0, 2.0) },
                { ("Neptune", "Jupiter"), (450, 1.0, 2.0) },
                { ("Mars", "Neptune"), (500, 1, 1.5) },
                { ("Neptune", "Mars"), (500, 1, 1.5) }
            };

            //Create cargo quantity for the items and assign cargo to the planets
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
                Console.WriteLine("\nWELCOME! You are about to begin an intergalactic adventure.\n");

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
                        captainName = Console.ReadLine();

                        HandleSelectedShipOption(shipOption);

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
        static void HandleSelectedShipOption(int shipOption)
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                PrintScreenHeader();

                //Print the chosen ship stats and request player to chose the destination
                Console.WriteLine($"\nHello Captain {captainName}, you have chosen the {shipsList[shipOption - 1].Name}.");
                PrintColorText($"\nThese are your current ship stats:\n\n", ConsoleColor.Yellow);

                ShipSpecs(shipOption);
                PrintColorText("Ready for the trip?\n", ConsoleColor.Red);

             
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
            string[] columnNames = { "Name", "Fuel Lvl", "Max Cargo", "Current Cargo", "Current Weight", "Location" };
            var table = new ConsoleTable(columnNames);
            double cargoWeight = 0;

            foreach (var item in shipsList[shipOtion - 1].CargoList)
            {
                cargoWeight += item.Weight;
            }

            table.AddRow(
                shipsList[shipOtion - 1].Name,
                shipsList[shipOtion - 1].Fuel + "/" + shipsList[shipOtion - 1].MaxFuelCapacity,
                shipsList[shipOtion - 1].CargoCapacity,
                shipsList[shipOtion - 1].CargoList.Count == 0 ? "Empty" : shipsList[shipOtion - 1].CargoList.Count + " items",
                cargoWeight,
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

           PrintColorText($"\nTravel Requirements from {start} to {destination}:\n\n", ConsoleColor.Yellow);
            var table = new ConsoleTable("Fuel", "Medical Supplies", "Food Supplies");

            table.AddRow($"{ requirements.fuelUnits} units", $"{requirements.medicalCrates} crate(s)", $"{requirements.foodCrates} crate(s)");
            table.Write(Format.Minimal);

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
            
            while (isPlaying)
            {
                string currentLocation = shipsList[start - 1].Location.Name;
                string destination = planetsList[destinationOption - 1].Name;

                bool meetRequirements = MeetsTheRequirements(currentLocation, destination);

                Console.Clear();
                PrintScreenHeader();
                bool isValudRoute = DisplayTravelRequirements(currentLocation, destination);

                if (!isValudRoute)
                {
                    isPlaying = true;
                    return;                    
                }

                PrintColorText("\nYour current ship stats:\n\n", ConsoleColor.Yellow);
                ShipSpecs(start);

                if(!meetRequirements)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Looks like you will need some supplies.\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congrats! you meet the minimum requirements for this trip.\n");
                    Console.ResetColor();
                }                

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
                        
                        GoToLaunchPlatform(start, destinationOption, meetRequirements); 
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

        /// <summary>
        /// Evaluate if the player meets the travel requirements
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <param name="currentFuelLvl"></param>
        /// <param name="currentMedSupplies"></param>
        /// <param name="currentFoodSupplies"></param>
        /// <returns></returns>
        static bool MeetsTheRequirements(string start, string destination)
        {
          
            if (!travelRequirements.ContainsKey((start, destination)))
            {
                Console.WriteLine($"No travel requirements found for route from {start} to {destination}.");
                return false;
            }

            var requirements = travelRequirements[(start, destination)];
            double neededFuelUnits = requirements.fuelUnits;
            double neededMedSupplies = requirements.medicalCrates;
            double neededFoodSupplies = requirements.foodCrates;

            // Find the current ship by its location
            SpaceShip currentShip = shipsList.FirstOrDefault(ship => ship.Location.Name == start);
            if (currentShip == null)
            {
                Console.WriteLine($"No ship found at {start}.");
                return false;
            }
                        
            double currentFuelUnits = currentShip.Fuel;
            double currentFoodSupplies = 0;
            double currentMedSupplies = 0;                       
         
            foreach (var cargo in currentShip.CargoList)
            {
                switch (cargo.Name)
                {
                    case "Fuel Tank":
                        currentFuelUnits += cargo.Weight;
                        break;
                    case "Food Supplies Crate":
                        currentFoodSupplies += cargo.Weight;
                        break;
                    case "Medical Supplies crate":
                        currentMedSupplies += cargo.Weight;
                        break;
                }
            }
                       
            // Check if requirements are met
            return currentFuelUnits >= neededFuelUnits &&
                   currentFoodSupplies >= neededFoodSupplies &&
                   currentMedSupplies >= neededMedSupplies;
        }

        /// <summary>
        /// Go to the simulated fuel station
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destinationOption"></param>
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

                Console.ForegroundColor = ConsoleColor.Blue;
                PrintCentered($"{currentLocation.ToUpper()} - F U E L  S T A T I O N\n");
                Console.ResetColor();

                PrintColorText($"\nTravel Fuel Stats for {shipsList[start - 1].Name}: \n\n", ConsoleColor.Yellow);
                
                table.AddRow(fuelLvl, maxFuelLvl, $"{requiredFuel}");

                table.Write(Format.Minimal);

                bool response = IsValidResponse("\nDo you want to start fueling your ship? (y/n): ");

                if(response)
                {
                    PrintColorText("\nPress any key to start filling up your tank: ", ConsoleColor.Red);
                    Console.ReadKey();

                    Console.WriteLine("\n");
                    GetFuel(fuelLvl, maxFuelLvl, requiredFuel, start - 1);
                    return;
                }
                else
                {
                    return;
                }            
            }         
        }

        /// <summary>
        /// Get fuel for teh sip
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="requiredFuel"></param>
        /// <param name="ship"></param>
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

        /// <summary>
        /// Get supplies for the ship
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destinationOption"></param>
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
                        {
                            // Get the quantity to buy
                            var selectedCargo = planetsList[planetIndex].AvailableCargo.ElementAt(itemIndex - 1).Key;
                            int maxQty = planetsList[planetIndex].AvailableCargo[selectedCargo];
                            int qty = ValidateOption("\nPlease enter the quantity: ", 1, maxQty);

                            PrintColorText($"\nYou selected {qty} unit(s) of {selectedCargo.Name}.\n", ConsoleColor.Red);

                            // Calculate the total weight of the items
                            double itemWeight = selectedCargo.Weight;
                            double totalItemsWeight = itemWeight * qty;

                            // Calculate the ship's current cargo weight
                            double currentCargoWeight = shipsList[start - 1].CargoList.Sum(cargo => cargo.Weight);

                            // Get the ship's max cargo capacity
                            double maxCargo = shipsList[start - 1].CargoCapacity;

                            if (currentCargoWeight + totalItemsWeight > maxCargo)
                            {
                                Console.WriteLine($"The total weight of the items exceeds the max cargo capacity of {maxCargo}. Unable to add to the ship.");
                            }
                            else
                            {
                                // Deduct the purchased quantity from the planet's stock using RemoveCargo
                                planetsList[planetIndex].RemoveCargo(selectedCargo, qty);

                                // Create a new Cargo object with the total weight and add it to the ship
                                shipsList[start - 1].CargoList.Add(new Cargo(selectedCargo.Name, totalItemsWeight));

                                PrintColorText($"\nSuccessfully added {qty} unit(s) of {selectedCargo.Name} (Total Weight: {totalItemsWeight}) to your ship's cargo.\n\n", ConsoleColor.Green);
                            }

                            bool proceed = IsValidResponse("Do you wish to buy anything else (y/n)? ");

                            if (proceed)
                            {                               
                                continue;
                            }
                            else
                            {
                                // Stop the teleprompter 
                                stopTeleprompter = true;
                                teleprompterThread.Join();
                                return;
                            }
                        }

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
            }

            // Stop the teleprompter when the user exits
            stopTeleprompter = true;
            teleprompterThread.Join(); // Wait for the teleprompter thread to stop
        }

        /// <summary>
        /// Simulate a teleprompter for the store page
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
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

        /// <summary>
        /// Create the page header for the store
        /// </summary>
        static void PrintStoreHeader()
        {            
            Console.SetCursorPosition(0, 1);         
            PrintScreenHeader();
            Console.Write("\n");

        }

        /// <summary>
        /// Simulate the launch platform
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destinationOption"></param>
        static void GoToLaunchPlatform(int start, int destinationOption, bool meetRequirements)
        {
            Console.Clear();
            PrintScreenHeader();

            bool requirmentsMet;
            bool isPlaying = false;

            string initialLocation = shipsList[start -1].Location.Name;
            string Destination = planetsList[destinationOption -1].Name;

            if (!meetRequirements)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                PrintCentered("W A R N I N G");
                PrintCentered($"Captain {captainName} You don't meet the minimum requirments for the trip from {initialLocation} to {Destination}.");
                Console.ResetColor();
                Console.WriteLine("\nI strongly advise to reconsider, we have lost many ships that way.");
                bool proceed = IsValidResponse("Do you want to at least get enough supplies to make it to the closest station (y/n)? ");

                if (!proceed)
                {
                    bool confirmation = IsValidResponse("Are you sure you want to continue (y/n)? ");

                    if (confirmation)
                    {
                        Console.WriteLine("Please proceed at your own risk. Good Luck!!!");
                        StartTrip(start, destinationOption, meetRequirements);
                    }
                    else
                    {
                        Console.WriteLine("Great choice! Let's prepare adequately.");
                        return; // Exit this method and allow them to prepare
                    }
                }
                else
                {
                    // If they agree to stock up, guide them to the supplies store
                    Console.Write("Good decision, Captain! Redirecting you to the supplies store...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                PrintCentered("C O N G R A T U L A T I O N S !!!\n");
                Console.ResetColor();
                PrintCentered($"Welcome to our launch platform Captain {captainName}, are you ready for your trip from {initialLocation} to {Destination}.");
                
                bool proceed = IsValidResponse("\nReady for Launch? ");

                if (!proceed)
                {
                    Console.WriteLine("Ok, we will be waiting for when you are ready");
                    return;
                }
                else
                {

                    Console.WriteLine("B O N  V O Y A G E!!");
                    StartTrip(start, destinationOption, meetRequirements);
                }
            }
        }

        /// <summary>
        /// Start the trip to the destination
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        static void StartTrip(int start, int destination, bool meetRequirements)
        {
            Console.Clear();
            PrintScreenHeader();

            Console.Write("Prepare for launch, please press any key to continue: ");
            Console.ReadKey();

            LaunchCountdown();

            // Rocket ASCII Art
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

            if (meetRequirements)
            {
                AnimateRocketLaunchToSuccessfulTrip(rocketArt);
            }
            else
            {
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


        /// <summary>
        /// Simulate a rocket launch animation when ship takes off meeting all requirements
        /// </summary>
        /// <param name="rocketArt"></param>
        static void AnimateRocketLaunchToSuccessfulTrip(string[] rocketArt)
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

            string[] destinationArt = new string[]
            {
                "*                 *                  *              *",
                "                                                      *             *",
                "                        *            *                             ___",
                "  *               *                                          |     | |",
                "        *              _________##                 *        / \\    | |",
                "                      @\\\\\\\\\\\\\\\\\\##    *     |              |--o|===|-|",
                "  *                  @@@\\\\\\\\\\\\\\\\##\\       \\|/|/            |---|   |B|",
                "                    @@ @@\\\\\\\\\\\\\\\\\\\\\\    \\|\\\\|//|/     *   /     \\  |V|",
                "             *     @@@@@@@\\\\\\\\\\\\\\\\\\\\\\    \\|\\|/|/         |  C    | |C|",
                "                  @@@@@@@@@----------|    \\\\|//          |  A    |=| |",
                "       __         @@ @@@ @@__________|     \\|/           |  N    | | |",
                "  ____|_@|_       @@@@@@@@@__________|     \\|/           |_______| |_|",
                "=|__ _____ |=     @@@@ .@@@__________|      |             |@| |@|  | |",
                "____0_____0__\\|/__@@@@__@@@__________|_\\|/__|___\\|/__\\|/___________|_|_"
            };


            // After animation, display a final message
            Console.Clear();
            Console.WriteLine("\n\nThe rocket has successfully launched into space! 🚀");
            Thread.Sleep(2000); // Pause to allow the user to see the message

            SuccessfulArrival(destinationArt);
        }

        /// <summary>
        /// Display the rocket centered on the screen
        /// </summary>
        /// <param name="rocketArt"></param>
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

        static void SuccessfulArrival(string[] destinationArt)
        {
            Console.Clear();
            PrintScreenHeader();
            Console.WriteLine();
            PrintCentered("Congratulations Captain, you have successfully arrived at your destination!\n");
            Console.WriteLine();

            
            PrintCentered("G A M E  O V E R");

            foreach (string line in destinationArt)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            QuitGame();
        }

        /// <summary>
        /// Simulate a rocket crash animation when ship takes off without enough resources
        /// </summary>
        /// <param name="rocketArt"></param>
        static void AnimateRocketCrashInRealTime(string[] rocketArt)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            PrintCentered("Dont say I didnt warn you!");         
            Thread.Sleep(2000);
            Console.ResetColor();

            int rocketHeight = rocketArt.Length; // Height of the rocket
            int consoleHeight = Console.WindowHeight; // Height of the console window
            int consoleWidth = Console.WindowWidth; // Width of the console window
            int startY = consoleHeight - rocketHeight; // Start position for the rocket
            int centerX = (consoleWidth - rocketArt[0].Length) / 2; // Calculate horizontal center
            Random random = new Random();

            // Rocket Ascension Animation
            for (int y = startY; y >= -rocketHeight / 2; y--) // Allow the rocket to ascend halfway off the screen
            {
                Console.Clear();

                // Print the rocket at the current position
                for (int i = 0; i < rocketArt.Length; i++)
                {
                    int lineY = y + i; // Calculate the line's vertical position

                    // Print only if the line is within the visible screen
                    if (lineY >= 0 && lineY < consoleHeight)
                    {
                        Console.SetCursorPosition(centerX, lineY); // Align the rocket horizontally
                        Console.WriteLine(rocketArt[i]);
                    }
                }

                Thread.Sleep(100); // Add delay to simulate smooth animation

                // Initiate explosion when half of the rocket is off the screen
                if (y == -rocketHeight / 2)
                {
                    Console.Clear();

                    // Display crash explosion
                    Console.SetCursorPosition(centerX, 0); // Position explosion at the top
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("💥 BOOM! 💥");
                    Console.ResetColor();

                    // Scatter debris
                    for (int i = 0; i < 50; i++) // Number of debris pieces to scatter
                    {
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

                    string[] reaperArt = new string[]
{
    "              ,____",
    "              |---.\\",
    "      ___     |    `",
    "     / .-\\  ./=)",
    "    |  |\"|_/\\/|",
    "    ;  |-;| /_|",
    "   / \\_| |/ \\ |",
    "  /      \\/\\( |",
    "  |   /  |` ) |",
    "  /   \\_/     |",
    " /--._/  \\    |",
    " `/|)    |    |",
    "   /     |    |",
    "  .'      |   |",
    " /         \\  |",
    "(_. -.__.__./ \\"
};


                    // After scattering, display a final message
                    Console.SetCursorPosition(0, consoleHeight - 1); // Move to the bottom of the screen
                    Console.ResetColor();
                    //foreach(var line in reaperArt)
                    //{
                    //    Console.WriteLine(line);
                    //}
                    AnimateReaperMovingRight(reaperArt);

                    PrintScreenHeader();
                    Console.WriteLine("\n\nThe rocket has broken apart, due to the  lack of resources Captain Manuel and the rest of the crew are lost!!!!!!. Press any key to continue...");
                    Console.ReadKey();
                    QuitGame();
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


        static void AnimateReaperMovingRight(string[] reaperArt)
        {
            Console.Clear();

            int consoleWidth = Console.WindowWidth; // Width of the console
            int consoleHeight = Console.WindowHeight; // Height of the console
            int startX = 0; // Starting position on the left
            int centerY = (consoleHeight - reaperArt.Length) / 2; // Center the reaper vertically

            // Animation loop
            for (int x = startX; x <= consoleWidth; x++)
            {
                Console.Clear();

                // Draw the reaper at the current position
                for (int i = 0; i < reaperArt.Length; i++)
                {
                    int lineY = centerY + i; // Calculate the vertical position for each line

                    // Ensure the line is within the screen bounds
                    if (lineY >= 0 && lineY < consoleHeight && x + reaperArt[i].Length < consoleWidth)
                    {
                        Console.SetCursorPosition(x, lineY); // Set the position for the current line
                        Console.WriteLine(reaperArt[i]);
                    }
                }

                Thread.Sleep(50); // Delay to create the animation effect
            }

            Console.Clear(); // Clear the screen after the reaper disappears
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

        /// <summary>
        /// Evaluate if the user input is valid
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static bool IsValidResponse(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string response = Console.ReadLine();

                // Check if response is valid
                if (string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("Answer cannot be null or empty. Please try again.");
                    continue;
                }

                response = response.Trim().ToLower(); // Normalize input

                if (response == "y")
                {
                    return true; // Yes = true
                }
                else if (response == "n")
                {
                    return false; // No = false
                }
                else
                {
                    Console.WriteLine("Invalid answer. Please reply with 'y' or 'n'.");
                }
            }
        }

        /// <summary>
        /// Print text in color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        static void PrintColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color; 
            Console.Write(text);
            Console.ResetColor();
        }

        #endregion
    }
}
