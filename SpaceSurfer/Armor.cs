using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс брони
    internal abstract class Armor : ShipEquipment
    {
        internal int DefenceRate { get; private set; }
        internal int Cost { get; private set; }
        
        //Список всей доступной брони
        internal static readonly Dictionary<string, Armor> ArmorList = new Dictionary<string,Armor>();
        //Заполнение списка брони
        static Armor() {
            ArmorList.Add("NoArmor", new NoArmor());
            ArmorList.Add("IronPlates", new IronPlates());
            ArmorList.Add("SteelPlates", new SteelPlates());
            ArmorList.Add("TitaniumPlates", new TitaniumPlates());
        }

        //Конструктор
        internal Armor(int defenceRate, int cost) {
            DefenceRate = defenceRate;
            Cost = cost;
        }

        //Покупка брони
        internal static void BuyArmor(Ship ship, Player player)
        {
            while (true)
            {
                Console.Write("      Current armor: {0}: {1}. Buy => ", ship.ShipArmor.GetType().ToString().Remove(0, 12), 
                    ship.ShipArmor);
                string input = Console.ReadLine();
                if (input == "0")
                {
                    return;
                }
                else if (!ArmorList.ContainsKey(input))
                {
                    continue;
                }
                Console.Write("        Defence rate - {0}, cost - {1}. Buy - 1 => ", ArmorList[input].DefenceRate,
                    ArmorList[input].Cost);
                if (Console.ReadLine() == "1")
                {
                    if (ArmorList[input].Cost > player.Money)
                    {
                        Console.WriteLine("        Not enough money!");
                        return;
                    }
                    player.Money -= ArmorList[input].Cost;
                    ship.ShipArmor = ArmorList[input];
                    Console.WriteLine("        Armor has been successfully installed!");
                    return;
                }
            }
        }

        //Переопределение метода ToString()
        public override string ToString()
        {
            return string.Format("defence rate - {0}", DefenceRate);
        }
    }

    //Класс "Без брони"
    internal class NoArmor : Armor
    {
        internal NoArmor()
            : base(0, 0) { }
    }

    //Класс брони типа "Железные пластины" 
    internal class IronPlates : Armor
    {
        internal IronPlates()
            : base(2, 100) { }
    }

    //Класс брони "Стальные пластины"
    internal class SteelPlates : Armor
    {
        internal SteelPlates()
            : base(4, 400) { }
    }

    //Класс брони "Титановые пластины"
    internal class TitaniumPlates : Armor
    {
        internal TitaniumPlates()
            : base(8, 1200) { }
    }
}
