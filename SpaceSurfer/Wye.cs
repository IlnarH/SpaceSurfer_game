using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal class Wye
    {
        internal string Name { get; set; }
        internal int SeverityLevel { get; set; }
        internal int ToleranceLevel { get; set; }
        internal bool playerHere = false;

        //Соседние звезды
        internal Dictionary<Wye, int> neighbourWyes = new Dictionary<Wye, int>();

        //Товары звезды
        internal Dictionary<string, Goods> goods = new Dictionary<string, Goods>();

        //Конструктор (имя, жестокость, толерантность, товары)
        internal Wye(string name, int severity, int tolerance)
        {
            Name = name;
            SeverityLevel = severity;
            ToleranceLevel = tolerance;
            goods.Add("Weaponry", new Weaponry());
            goods.Add("Foodstaffs", new Foodstaffs());
            goods.Add("Drugs", new Drugs());
        }

        internal void UpdateWye()
        {
            foreach (var product in goods)
            {
                product.Value.UpdateGoods();
            }
        }

        //Вывод соседних звезд
        internal void PrintNeighbourWyes()
        {
            Console.Write("  Neighbour wyes:");
            foreach (var neighbour in neighbourWyes)
            {
                Console.Write(" " + neighbour.Key.Name);
            }
            Console.WriteLine();
        }

        //Переопределение метода ToString()
        public override string ToString()
        {
            return string.Format("Wye - {0}, severity - {1}, tolerance - {2}", Name, SeverityLevel, ToleranceLevel);
        }

        //Действия на звезде
        internal void WyeActions(Player player)
        {
            while (true) 
            {
                Console.Write("  Wye {0}: 1 - wye status, 2 - trade => ", Name);
                switch (Console.ReadLine())
                {
                    case "0":
                        return;
                    case "1":
                        Console.WriteLine("    " + this);
                        break;
                    case "2":
                        Trade(player);
                        break;
                    default:
                        break;
                }
            }
        }

        //Торговля
        internal void Trade(Player player) {
            while (true) 
            {
                Console.Write("    Trade with => ");
                string input = Console.ReadLine();
                if (input == "0") 
                {
                    return;
                }
                if (!(goods.ContainsKey(input)))
                {
                    continue;
                }
                Goods product = goods[input];
                if (product.GetIllegalityLevel() > ToleranceLevel) 
                {
                    Console.WriteLine("    Denied here!");
                }
                else 
                {
                    Console.Write("    {0}. Your cash: {1}. Buy - 1, sell - 2 => ", product,
                        player.Money);
                    switch (Console.ReadLine()) 
                    {
                        case "0":
                            return;
                        case "1":
                            product.Buy(player, input);
                            break;
                        case "2":
                            product.Sell(player, input);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
