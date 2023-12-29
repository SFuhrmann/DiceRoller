using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRoller
{
    internal class DiceRoller
    {
        internal List<int> RolledDice { get; private set; } = new List<int>();
        internal List<int> MaxValues { get; private set; } = new List<int>();
        internal bool DifferentMaxValues { get; private set; }
        private Random rndGenerator = new Random();

        internal DiceRoller(int numberOfDice, int maxValue)
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                RolledDice.Add(0);
                MaxValues.Add(maxValue);
            }
        }

        internal void Roll()
        {
            RolledDice.Clear();
            foreach (var item in MaxValues)
            {
                RolledDice.Add(rndGenerator.Next(item) + 1);
            }
        }
    }
}
