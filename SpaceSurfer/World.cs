using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceSurfer
{
    internal class World
    {
        internal Dictionary<string, Constellation> constellations = new Dictionary<string, Constellation>();

        internal World()
        {
            constellations.Add("Lion", new Constellation());
        }

        internal void UpdateWorld()
        {
            while (true)
            {
                foreach (var constellation in constellations)
                {
                    if (GameManager.random.Next(1000000000) == 0)
                    {
                        constellation.Value.UpdateConstellation();
                    }
                }
            }
        }
    }
}
