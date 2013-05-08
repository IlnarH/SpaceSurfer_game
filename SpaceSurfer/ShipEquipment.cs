using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal abstract class ShipEquipment
    {
        internal static void BuyEquipment(Ship ship, Player player)
        {
            while (true)
            {
                Console.Write("    Your cash: {0}. Buy: armor - 1, weapons - 2, engine - 3, new ship - 4 => ", player.Money);
                switch (Console.ReadLine())
                {
                    case "0":
                        return;
                    case "1":
                        Armor.BuyArmor(ship, player);
                        break;
                    case "2":
                        Weapons.BuyWeapon(ship, player);
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
