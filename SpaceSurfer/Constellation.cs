using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal class Constellation
    {
        //Звезды в созвездии
        internal readonly Dictionary<string, Wye> wyes = new Dictionary<string, Wye>();
        internal string Name { get; private set; }

        //Конструктор (инициализация звезд)
        internal Constellation()
        {
            Name = "Lion";
            Wye arya = new Wye("Arya", 0, 3);
            Wye belez = new Wye("Belez", 0, 2);
            Wye celena = new Wye("Celena", 2, 1);
            Wye devian = new Wye("Devian", 3, 3);
            arya.neighbourWyes.Add(belez, 5);
            arya.neighbourWyes.Add(celena, 7);
            belez.neighbourWyes.Add(arya, 5);
            belez.neighbourWyes.Add(devian, 7);
            celena.neighbourWyes.Add(arya, 7);
            celena.neighbourWyes.Add(devian, 5);
            devian.neighbourWyes.Add(belez, 7);
            devian.neighbourWyes.Add(celena, 5);

            wyes.Add("Arya", arya);
            wyes.Add("Belez", belez);
            wyes.Add("Celena", celena);
            wyes.Add("Devian", devian);
        }
        
        //Обновление созвездия
        internal void UpdateConstellation()
        {
            foreach (var wye in wyes)
            {
                wye.Value.UpdateWye();
            }
        }
    }
}
