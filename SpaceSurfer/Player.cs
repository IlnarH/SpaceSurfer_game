using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс игрока
    internal class Player
    {
        internal Ship PlayersShip { get; set; }
        internal Wye Location { get; set; }
        internal int Money { get; set; }
        internal int Experience { get; set; }
        internal int stepCounter = 0;
        internal bool Alive { get; set; }
        internal int Level { get; set; }
        internal delegate void PlayersDeath();
        internal event PlayersDeath Killed;

        //Товары игрока
        internal Dictionary<string, Goods> goods = new Dictionary<string, Goods>();

        //Конструктор
        internal Player(Constellation constellation)
        {
            PlayersShip = new Corvette();
            Location = constellation.wyes["Arya"];
            Money = 100;
            Alive = true;
            Level = 1;
            goods.Add("Weaponry", new Weaponry(0, 0));
            goods.Add("Foodstaffs", new Foodstaffs(0, 0));
            goods.Add("Drugs", new Drugs(0, 0));
        }

        //Основные действия
        internal void MainActions(Constellation constellation)
        {
            while (true)
            {
                Console.Write("\nChoose action: 1 - move, 2 - ship, 3 - wye => ");
                switch (Console.ReadLine())
                {
                    case "exit":
                        return;
                    case "1":
                        Move(constellation);
                        break;
                    case "2":
                        PlayersShip.ShipActions(this);
                        break;
                    case "3":
                        Location.WyeActions(this);
                        break;
                    default:
                        break;
                }
            }
        }

        //Передвижение от одной звезды к другой
        internal void Move(Constellation constellation)
        {
            Location.PrintNeighbourWyes();
            Console.Write("  Choose destination wye => ");
            string destination = Console.ReadLine();
            if (!(constellation.wyes.ContainsKey(destination) 
                && Location.neighbourWyes.ContainsKey(constellation.wyes[destination])))
            {
                return;
            }
            if (PlayersShip.CurrentFuel < Location.neighbourWyes[constellation.wyes[destination]])
            {
                Console.WriteLine("    Not enough fuel!");
                return;
            }
            PlayersShip.CurrentFuel -= Location.neighbourWyes[constellation.wyes[destination]];
            Location = constellation.wyes[destination];

            //Вероятность атаки
            try
            {
                GameManager.FightProbability(this);
            }
            catch (DeadPlayerException)
            {
                if (Killed != null)
                {
                    Killed();
                }
            }

            Console.WriteLine("    Moved succesfully. Fuel: " + PlayersShip.CurrentFuel + "/" + PlayersShip.TotalFuel + 
                ". Move - " + ++stepCounter + "\n    --------------------------------------------------------------------");

            //Обновление мира при перелете
            constellation.UpdateConstellation();
        }

        internal static void EndGame()
        {
            Console.WriteLine("\n                                You are dead!");
            throw new DeadPlayerException();
        }
    }
}
