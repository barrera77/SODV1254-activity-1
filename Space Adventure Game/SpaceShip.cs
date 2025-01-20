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
        public double Fuel {  get; set; }
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

        public void Refuel(double amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Fuel amount must be greater than 0");
            }
            else
            {
                Fuel += amount;
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
        public SpaceShip(string name, double fuel, double cargoCapacity, Planet initialLocation)
        {
            Name = name;
            Fuel = fuel;
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
            return $"SpaceShip: {Name}, Location: {Location.Name}, Fuel: {Fuel} units, Cargo Capacity: {CargoCapacity} tons";
        }
    }
}
