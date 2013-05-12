using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceSurfer
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            Console.WindowHeight = 30;

            //Создание мира
            World world = new World();

            //Дополнительный поток для обновления мира в фоновом режиме
            Thread worldUpdate = new Thread(world.UpdateWorld);
            worldUpdate.Start();

            //Создание игрока
            Player player = new Player(world.constellations["Lion"]);

            //Отслеживание гибели игрока
            player.Killed += new Player.PlayersDeath(Player.EndGame);

            //Переход к действиям
            try
            {
                player.MainActions(world.constellations["Lion"]);
            }
            catch (DeadPlayerException)
            {
            }
            finally
            {
                worldUpdate.Abort();
            }

            Console.WriteLine(player.stepCounter);

            Console.ReadLine();
        }
    }
}
