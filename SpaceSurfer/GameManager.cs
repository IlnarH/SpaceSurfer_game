using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal static class GameManager
    {
        internal static int Intensity { get; private set; }
        internal static int Accuracy { get; private set; }
        internal static int Maneuver { get; private set; }

        internal static Random random = new Random();

        //Решает, появляются ли враги
        internal static void FightProbability(Player player)
        {
            if (random.Next(10) < player.Location.SeverityLevel)
            {
                Console.WriteLine("    You have been attacked!\n");
                Fight(player);
                if (player.Level != (int)(Math.Sqrt(player.Experience) / 2 + 1))
                {
                    player.Level = (int)(Math.Sqrt(player.Experience) / 2 + 1);
                    Console.WriteLine("    You got {0} level.\n", player.Level);
                }
            }
        }

        //Бой
        internal static void Fight(Player player)
        {
            //Генерирование врагов
            Dictionary<int, Enemy> enemies = Enemy.GenerateEnemies(player.Level);
            int experience = 0;
            int money = 0;
            foreach (var enemy in enemies)
            {
                experience += enemy.Value.Experience;
                money += (enemy.Value.Level + 1) * random.Next(7, 13);
                Console.WriteLine("      " + enemy.Key + " enemy: " + enemy.Value);
            }
            //Начало боя
            Console.WriteLine("\n    -------------------------------Battle-------------------------------");
            //Боевой цикл (пошаговое выполнение при живых обеих сторонах)
            while (player.Alive && enemies.Count != 0)
            {
                try
                {
                    FightTurn(enemies, player);
                }
                //Окончание игры при поражении
                catch (DeadPlayerException)
                {
                    Console.WriteLine("    ------------------------------You lose-------------------------------");
                    throw new DeadPlayerException();
                }
            }
            //Победа
            player.Money += money;
            player.Experience += experience;
            Console.WriteLine("    ------------------------------You win!-------------------------------");
            Console.WriteLine("    {0} money, {1} experience received.\n", money, experience);
        }

        //Один ход боя
        internal static void FightTurn(Dictionary<int, Enemy> enemies, Player player)
        {
            Dictionary<int, Enemy> enemiesCopy = new Dictionary<int,Enemy>(enemies);
            Console.WriteLine("\nYour turn:");
            foreach (var enemy in enemiesCopy)
            {
                Intensity = Accuracy = Maneuver = 0;
                FightTactic(enemy, player);
                if (PlayerAttack(enemy, player))
                {
                    Console.WriteLine("  Enemy destroyed.");
                    enemies.Remove(enemy.Key);
                }
                else
                {
                    EnemyAttack(enemy, player);
                }
            }
        }

        //Выбор тактики против врага (увеличение силы или точности орудий или маневренности корабля)
        internal static void FightTactic(KeyValuePair<int, Enemy> enemy, Player player)
        {
            while (true)
            {
                Console.Write("Your excertion on {0} enemy: 1 - intensity, 2 - accuracy, 3 - maneuver => ", enemy.Key);
                switch (Console.ReadLine())
                {
                    case "1":
                        Intensity = player.Level;
                        return;
                    case "2":
                        Accuracy = player.Level*5;
                        return;
                    case "3":
                        Maneuver = player.Level*5;
                        return;
                    default:
                        break;
                }
            }
        }

        //Атака игрока
        internal static bool PlayerAttack(KeyValuePair<int, Enemy> enemy, Player player)
        {
            foreach (var weapon in player.PlayersShip.shipWeapons)
            {
                if (weapon.GetType().ToString() == "NoWeapon")
                {
                    continue;
                }

                int damage;

                if (random.Next(enemy.Value.Maneuverability) > random.Next(weapon.Accuracy + Accuracy))
                {
                    damage = -1;
                }
                else
                {
                    damage = weapon.CurrentDamage * random.Next(75, 125) / 100 + Intensity;
                    enemy.Value.CurrentHealth = Math.Max(0, enemy.Value.CurrentHealth - (damage - enemy.Value.Armor));
                }

                Console.WriteLine("Enemy: {0}, weapon: {1}(lvl {2}) - {3}. Health - {4}/{5}.", enemy.Key, 
                    weapon.GetType().ToString().Remove(0, 12), weapon.WeaponLevel, 
                    (damage == -1) ? "missed" : damage.ToString(), enemy.Value.CurrentHealth, enemy.Value.TotalHealth);

                if (enemy.Value.CurrentHealth == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Атака врага
        internal static void EnemyAttack(KeyValuePair<int, Enemy> enemy, Player player)
        {
            int damage;

            if (random.Next(enemy.Value.Accuracy) < random.Next(player.PlayersShip.Maneuverability + Maneuver))
            {
                damage = -1;
            }
            else
            {
                damage = enemy.Value.Damage * random.Next(75, 125) / 100;
                player.PlayersShip.CurrentHealth = Math.Max(0, player.PlayersShip.CurrentHealth - (damage -
                    player.PlayersShip.ShipArmor.DefenceRate));
            }

            Console.WriteLine("Enemy: {0} - {1}. Your health - {2}/{3}.", enemy.Key,
                (damage == -1) ? "missed" : damage.ToString(), player.PlayersShip.CurrentHealth,
                player.PlayersShip.TotalHealth);

            if (player.PlayersShip.CurrentHealth == 0)
            {
                player.Alive = false;
                throw new DeadPlayerException();
            }
        }
    }
}
