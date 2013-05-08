using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс товаров
    internal abstract class Goods
    {
        internal int CostForPiece { get; set; }
        internal int Count { get; set; }

        //Конструктор без параметров
        internal Goods()
        {
            CostForPiece = GetDefaultCost() * GameManager.random.Next(75, 125) / 100;
            Count = GameManager.random.Next(30);
        }

        //Конструктор с указанием цены
        internal Goods(int cost)
        {
            CostForPiece = cost;
            Count = 0;
        }

        //Конструктор с указанием цены и количества
        internal Goods(int cost, int count)
            : this(cost)
        {
            Count = count;
        }

        internal void UpdateGoods()
        {
            CostForPiece = GetDefaultCost() * GameManager.random.Next(75, 125) / 100;
            Count = GameManager.random.Next(30);
        }

        //Переопределение метода ToString()
        override public string ToString()
        {
            return string.Format("Cost - {0}, count - {1}", CostForPiece, Count);
        }

        internal abstract int GetIllegalityLevel();
        internal abstract int GetDefaultCost();

        //Покупка
        internal void Buy(Player player, string input)
        {
            Console.Write("      Ship capacity - {0}/{1}\n      Items count => ", player.PlayersShip.CurrentCapacity,
                player.PlayersShip.TotalCapacity);
            int count;
            if (!Int32.TryParse(Console.ReadLine(), out count))
            {
                return;
            }
            if (count > this.Count)
            {
                Console.WriteLine("      Not enough items on  wye!");
            }
            else if (player.PlayersShip.TotalCapacity - player.PlayersShip.CurrentCapacity < count)
            {
                Console.WriteLine("      Not enough place on ship!");
            }
            else if (player.Money < count * CostForPiece)
            {
                Console.WriteLine("      Not enough money!");
            }
            else
            {
                player.PlayersShip.CurrentCapacity += count;
                Count -= count;
                player.Money -= count * CostForPiece;
                player.goods[input].Count += count;
                Console.WriteLine("      Successfully bought!");
            }
        }

        //Продажа
        internal void Sell(Player player, string input)
        {
            Console.Write("      Items in cargo bay - {0}. Items count => ", player.goods[input].Count);
            int count;
            if (!Int32.TryParse(Console.ReadLine(), out count))
            {
                return;
            }
            if (count > player.goods[input].Count)
            {
                Console.WriteLine("    Not enough items!");
            }
            else
            {
                player.PlayersShip.CurrentCapacity -= count;
                player.goods[input].Count -= count;
                Count += count;
                player.Money += CostForPiece * count;
                Console.WriteLine("      Successfully sold!");
            }
        }
    }

    //Класс товаров типа "Оружие"
    internal class Weaponry : Goods
    {
        private static int defaultCost = 30;
        internal override int GetDefaultCost() { return defaultCost; }
        private static int illegalityLevel = 2;
        internal override int GetIllegalityLevel() { return illegalityLevel; }
        
        //Конструкторы
        internal Weaponry() 
            : base() { }
        internal Weaponry(int cost) 
            : base(cost) { }
        internal Weaponry(int cost, int count) 
            : base(cost, count) { }
    }

    //Класс товаров типа "Продовольствие"
    internal class Foodstaffs : Goods
    {
        private static int defaultCost = 10;
        internal override int GetDefaultCost() { return defaultCost; }
        private static int illegalityLevel = 1;
        internal override int GetIllegalityLevel() { return illegalityLevel; }

        //Конструкторы
        internal Foodstaffs() 
            : base() { }
        internal Foodstaffs(int cost) 
            : base(cost) { }
        internal Foodstaffs(int cost, int count) 
            : base(cost, count) { }
    }

    //Класс товаров типа "Наркотики"
    internal class Drugs : Goods
    {
        private static int defaultCost = 50;
        internal override int GetDefaultCost() { return defaultCost; }
        private static int illegalityLevel = 3;
        internal override int GetIllegalityLevel() { return illegalityLevel; }

        //Конструкторы
        internal Drugs() 
            : base() { }
        internal Drugs(int cost) 
            : base(cost) { }
        internal Drugs(int cost, int count) 
            : base(cost, count) { }
    }
}
