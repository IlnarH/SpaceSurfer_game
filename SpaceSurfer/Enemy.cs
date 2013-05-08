using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    //Класс врагов
    internal class Enemy
    {
        internal int Damage { get; private set; }
        internal int Accuracy { get; set; }
        internal int Armor { get; private set; }
        internal int CurrentHealth { get; set; }
        internal int TotalHealth { get; private set; }
        internal int Maneuverability { get; private set; }
        internal int Level { get; private set; }
        internal int Experience { get; private set; }

        //Конструктор
        internal Enemy(int level)
        {
            Level = level;
            Experience = Level + 1;
            TotalHealth = 50 + Level * GameManager.random.Next(7, 13);
            CurrentHealth = TotalHealth;
            int coef = GameManager.random.Next(7, 13);
            Damage = 10 + Level * coef / 10;
            Accuracy = (19 - coef) * 7;
            Armor = Level * GameManager.random.Next(7, 13) / 20;
            Maneuverability = GameManager.random.Next(20, 80) + Level * GameManager.random.Next(7, 13) / 2;
        }

        // Переопределение метода ToString() (характеристики врага)
        public override string ToString()
        {
            return string.Format("{0} lvl, H - {1}, D - {2}, Acc - {3}, A - {4}, M - {5}",
                Level, TotalHealth, Damage, Accuracy, Armor, Maneuverability);
        }

        //Генерирование врагов в зависимости от уровня игрока (количество, уровень)
        internal static Dictionary<int, Enemy> GenerateEnemies(int level)
        {
            Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();
            int amount = GameManager.random.Next(1, level / 3 + 2);
            for (int i = 1; i <= amount; i++)
            {
                enemies.Add(i, new Enemy(GameManager.random.Next(level + 1)));
            }
            return enemies;
        }
    }
}
