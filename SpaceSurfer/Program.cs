using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            Console.WindowHeight = 30;

            //Создание созвездия
            Constellation constellation = new Constellation();

            //Создание игрока
            Player player = new Player(constellation);

            //Отслеживание гибели игрока
            player.Killed += new Player.PlayersDeath(Player.EndGame);

            //Переход к действиям
            try
            {
                player.MainActions(constellation);
            }
            catch (DeadPlayerException)
            {
            }

            Console.WriteLine(player.stepCounter);

            Console.ReadLine();
        }
    }
}
