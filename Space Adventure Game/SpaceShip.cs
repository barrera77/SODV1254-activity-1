using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Adventure_Game
{
    internal class SpaceShip
    {
        public string Name { get; set; }
        public int Fuel {  get; set; }
        public int MaxFuelCapacity { get; private set; }
        public double CargoCapacity { get; private set; }
        public List<Cargo> CargoList { get; private set; }
        public Planet Location { get; private set; }

      
        public void Fly(Planet destination)
        {
            if(destination == null)
            {
                throw new ArgumentNullException(nameof(destination), "Destination cannot be null");
            }
            if(Fuel <= 0)
            {
                throw new InvalidOperationException("Not enough fuel for the flight");
            }

            Location = destination;
        }

        public void Refuel(int amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Fuel amount must be greater than 0");
            }
            if(Fuel + amount > MaxFuelCapacity)
            {
                Fuel = MaxFuelCapacity;
                Console.Write($"{Name} has been refueled by {amount} units. Current fuel: {Fuel} units.");

            }
            else
            {
                Fuel += amount;
                Console.WriteLine($"{Name} has been refueled by {amount} units. Current fuel: {Fuel} units.");
            }
        }

        public void LoadCargo(Cargo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }

            double currentCargoWeight = GetTotalCargoWeight();

            if (currentCargoWeight + item.Weight > CargoCapacity)
            {
                throw new InvalidOperationException("Not enough cargo capacity to add this item to the load");
            }
            else
            {
                CargoList.Add(item);
                               
            }
        }

        public void UnloadCargo(Cargo item)
        {
            if(item ==null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }
            if(!CargoList.Contains(item))
            {
                throw new InvalidOperationException("Item Doenst Exist");
            }
            else
            {
                CargoList.Remove(item);                
            }
        }

        //Constructor
        public SpaceShip(string name, int fuel, int maxFuelCapacity, double cargoCapacity, Planet initialLocation)
        {
            Name = name;
            Fuel = fuel;
            MaxFuelCapacity = maxFuelCapacity;
            CargoCapacity = cargoCapacity;
            CargoList = new List<Cargo>();
            Location = initialLocation;
        }

        //Helper methods
        private double GetTotalCargoWeight()
        {
            double totalCargoWeight = 0;

            foreach(var item in CargoList)
            {
                totalCargoWeight += item.Weight;    
            }
            return totalCargoWeight;
        }
        public override string ToString()
        {
            return $"SpaceShip: {Name}, Location: {Location.Name}, Fuel: {Fuel}/{MaxFuelCapacity} units, Cargo Capacity: {CargoCapacity} tons";
        }
    }
}
