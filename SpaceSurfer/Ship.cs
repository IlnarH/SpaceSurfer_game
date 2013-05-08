using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс корабля
    internal class Ship
    {
        internal int TotalHealth { get; private set; }
        internal int CurrentHealth { get; set; }
        internal int TotalFuel { get; private set; }
        internal int CurrentFuel { get; set; }
        internal Weapons[] shipWeapons;
        internal Armor ShipArmor { get; set; }
        internal int CurrentCapacity { get; set; }
        internal int TotalCapacity { get; private set; }
        internal int Maneuverability { get; set; }

        //Конструкторы
        internal Ship(int health, int fuel, int slotsForWeapon, int maneuverability)
        {
            TotalHealth = health;
            CurrentHealth = TotalHealth;
            TotalFuel = fuel;
            CurrentFuel = TotalFuel;
            shipWeapons = new Weapons[slotsForWeapon];
            for (int i = 0; i < slotsForWeapon; i++)
            {
                shipWeapons[i] = new NoWeapon();
            }
            ShipArmor = new NoArmor();
            TotalCapacity = health / 5;
            CurrentCapacity = 0;
            Maneuverability = maneuverability;
        }

        internal Ship(Ship oldShip)
        {

        }

        //Действия с кораблем
        internal void ShipActions(Player player)
        {
            while (true)
            {
                Console.Write("  Ship condition - 1, buy equipment - 2, cargo bay - 3 => ");
                switch (Console.ReadLine())
                {
                    case "0":
                        return;
                    case "1":
                        ShipCondition(player);
                        break;
                    case "2":
                        ShipEquipment.BuyEquipment(this, player);
                        break;
                    case "3":
                        InformationAboutCargoBay(player);
                        break;
                    default:
                        break;
                }
            }
        }

        //Состояние корабля
        private void ShipCondition(Player player)
        {
            Console.Write("    Health: {0}/{1}\n    Fuel: {2}/{3}.\n    Restore health - 1, restore fuel - 2 => ", 
                CurrentHealth, TotalHealth, CurrentFuel, TotalFuel);
            switch (Console.ReadLine())
            {
                case "1":
                    int healthDifference = TotalHealth - CurrentHealth;
                    if (healthDifference <= player.Money)
                    {
                        player.Money -= healthDifference;
                        CurrentHealth = TotalHealth;
                    }
                    else
                    {
                        CurrentHealth += player.Money;
                        player.Money = 0;
                    }
                    Console.WriteLine("      Health restored.");
                    break;
                case "2":
                    int fuelDifference = TotalFuel - CurrentFuel;
                    if (fuelDifference <= player.Money)
                    {
                        player.Money -= fuelDifference;
                        CurrentFuel = TotalFuel;
                    }
                    else
                    {
                        CurrentFuel += player.Money;
                        player.Money = 0;
                    }
                    Console.WriteLine("      Fuel restored.");
                    break;
                default:
                    return;
            }
        }

        //Информация о грузовом отсеке
        internal void InformationAboutCargoBay(Player player)
        {
            Console.WriteLine("    Goods in cargo bay:");
            Console.WriteLine("      Your cash - {0}", player.Money);
            foreach (var product in player.goods)
            {
                if (product.Value.Count > 0)
                {
                    Console.WriteLine("      {0} - {1}", product.Key, product.Value.Count);
                }
            }
        }

    }

    //Модель корабля "Корвет"
    internal class Corvette : Ship
    {
        internal Corvette() 
            : base(100, 50, 1, 80) { }
        internal Corvette(Ship oldShip) 
            : base(oldShip) 
        {

        }
    }

    //Модель корабля "Вояджер"
    internal class Voyager : Ship
    {
        internal Voyager() 
            : base(200, 70, 2, 50) { }
        internal Voyager(Ship oldShip) 
            : base(oldShip) 
        {

        }
    }

    //Модель корабля "Эсминец"
    internal class Destroyer : Ship
    {
        internal Destroyer() 
            : base(300, 100, 3, 30) { }
        internal Destroyer(Ship oldShip) 
            : base(oldShip) 
        {

        }
    }
}
