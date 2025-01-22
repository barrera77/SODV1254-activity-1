using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Adventure_Game
{
       
    internal class Planet
    {
        public string Name { get; set; }
        public Dictionary<Cargo, int> AvailableCargo { get; private set; }
        public bool RefuelingStation { get; set; }

        //Constructor
        public Planet(string name, bool refuelingStation)
        {
            Name = name;
            RefuelingStation = refuelingStation;
            AvailableCargo = new Dictionary<Cargo, int>();
        }

        /// <summary>
        /// Add Cargo items to the avilable cargo inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemQty"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddCargo(Cargo item, int itemQty)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }
            if(itemQty <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemQty), "Quantity must be greater than 0");
            }

            if(AvailableCargo.ContainsKey(item))
            {
                //If item exists add teh quantity
                AvailableCargo[item] += itemQty;    
            }
            else
            {
                //if item does not exist add new iyem with quantity
                AvailableCargo[item] = itemQty;
            }
        }

        /// <summary>
        /// Remove either items from teh inventory or just subtract quantities
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveCargo(Cargo item, int itemQty)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }
            if (!AvailableCargo.ContainsKey(item))
            {
                throw new InvalidOperationException("Item Doenst Exist");
            }
            if (itemQty <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemQty), "Quantity must be greater than 0");
            }
            if (AvailableCargo[item] < itemQty)
            {
                throw new InvalidOperationException("Not enough items qty to remove");
            }            
            if (AvailableCargo[item] == 0)
            {
                AvailableCargo.Remove(item);
            }
            else
            {
                AvailableCargo[item] -= itemQty;
            }
        }

        public void RefuelSpacehip(SpaceShip spaceShip, int amount)
        {
            if (spaceShip == null)
            {
                throw new ArgumentNullException(nameof(spaceShip), "Spaceship cannot be null");
            }
            if (!RefuelingStation)
            {
                throw new InvalidOperationException($"{Name} is not a refueling station");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Fuel amount must be greater than 0");
            }
            else
            {
                spaceShip.Fuel += amount;
            }

        }

        /// <summary>
        /// Display the current inventory in the planet
        /// </summary>
        public void DisplayAvailableCargo()
        {
            Console.WriteLine($"Available Cargo:");

            var table = new ConsoleTable("Item", "Quantity");

            foreach (var item in AvailableCargo)
            {
                table.AddRow(item.Key.Name, item.Value + " units");
            }

            Console.WriteLine(Name);
            table.Write();
            Console.WriteLine();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
