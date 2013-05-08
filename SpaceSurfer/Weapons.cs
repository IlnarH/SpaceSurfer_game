using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс оружий
    internal abstract class Weapons : ShipEquipment
    {
        internal int BaseDamage { get; private set; }
        internal int CurrentDamage { get; private set; }
        internal int Accuracy { get; private set; }
        internal int WeaponLevel { get; private set; }
        internal int Cost { get; private set; }

        //Список всех доступных оружий
        internal static readonly Dictionary<string, Weapons> WeaponsList = new Dictionary<string, Weapons>();
        //Заполнение списка оружий
        static Weapons()
        {
            WeaponsList.Add("NoWeapon", new NoWeapon());
            WeaponsList.Add("Laser", new Laser());
            WeaponsList.Add("Rocket", new Rocket());
            WeaponsList.Add("Plasma", new Plasma());
        }

        //Конструктор
        internal Weapons(int damage, int accuracy, int cost) {
            BaseDamage = damage;
            CurrentDamage = damage;
            Accuracy = accuracy;
            WeaponLevel = 1;
            Cost = cost;
        }

        //Покупка оружия
        internal static void BuyWeapon(Ship ship, Player player)
        {
            Console.WriteLine("      Current weapons:");
            foreach (var weapon in ship.shipWeapons)
            {
                Console.WriteLine("        " + weapon.GetType().ToString().Remove(0, 12) + ": " + weapon);
            }
            while (true)
            {
                Console.Write("      Buy => ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    return;
                }
                else if (!WeaponsList.ContainsKey(input))
                {
                    continue;
                }
                Console.Write("        Weapon's damage - {0}, accuracy - {1}, cost - {2}. Buy - 1 => ", 
                    WeaponsList[input].BaseDamage, WeaponsList[input].Accuracy, WeaponsList[input].Cost);
                if (Console.ReadLine() == "1")
                {
                    if (WeaponsList[input].Cost > player.Money)
                    {
                        Console.WriteLine("        Not enough money!");
                        return;
                    }
                    while (true)
                    {
                        Console.Write("        In slot => ");
                        string stringSlot = Console.ReadLine();
                        int slot = 0;
                        if (!Int32.TryParse(stringSlot, out slot))
                        {
                            continue;
                        }
                        if (slot == 0)
                        {
                            return;
                        }
                        if (slot > ship.shipWeapons.Length)
                        {
                            Console.WriteLine("          Not enough slots!");
                            continue;
                        }
                        switch (input)
                        {
                            case "NoWeapon":
                                ship.shipWeapons[slot-1] = new NoWeapon();
                                break;
                            case "Laser":
                                ship.shipWeapons[slot-1] = new Laser();
                                break;
                            default:
                                break;
                        }
                        player.Money -= ship.shipWeapons[slot - 1].Cost;
                        Console.WriteLine("        Weapon has been successfully installed!");
                        return;
                    }
                }
            }
        }

        //Переопределение метода ToString()
        public override string ToString()
        {
            return string.Format("damage - {0}, accuracy - {1}, level - {2}", CurrentDamage, Accuracy, WeaponLevel);
        }
    }

    //Класс "Без оружия"
    internal class NoWeapon : Weapons
    {
        internal NoWeapon()
            : base(0, 0, 0) { }
    }
    //Класс оружия типа "Лазер"
    internal class Laser : Weapons
    {
        internal Laser()
            : base(10, 80, 100) { }
    }
    //Класс оружия "Ракетное оружие"
    internal class Rocket : Weapons
    {
        internal Rocket()
            : base(20, 40, 300) { }
    }
    //Класс оружия "Плазменное оружие"
    internal class Plasma : Weapons
    {
        internal Plasma()
            : base(20, 75, 800) { }
    }
}