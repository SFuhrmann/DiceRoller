using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace DiceRoller
{
    internal class DiceRoller
    {
        // TODO:
        // Convert these lists to private readonly collections so you cannot modify them from outside
        internal List<int> RolledDice { get; private set; }
        internal List<int> MaxValues { get; private set; } = new List<int>();
        internal bool DifferentMaxValues { get; private set; }
        private Random rndGenerator = new Random();

        internal DiceRoller(int numberOfDice, int maxValue)
        {
            RolledDice = new List<int>(numberOfDice);
            MaxValues = new List<int>(numberOfDice);

            RolledDice.AddRange(Enumerable.Repeat(0, numberOfDice));
            MaxValues.AddRange(Enumerable.Repeat(maxValue, numberOfDice));
        }

        internal void Roll()
        {
            RolledDice.Clear();
            foreach (var item in MaxValues)
            {
                RolledDice.Add(rndGenerator.Next(item) + 1);
            }
        }

        internal void Roll(int[] ids)
        {
            foreach (var item in ids)
            {
                RolledDice[item] = rndGenerator.Next(MaxValues[item]) + 1;
            }
        }

        internal void AddDice(int maxValue)
        {
            MaxValues.Add(maxValue);
            RolledDice.Add(0);
        }

        internal void AddDice(int[] maxValues)
        {
            MaxValues.AddRange(maxValues);
            RolledDice.AddRange(Enumerable.Repeat(0, maxValues.Length));
        }

        internal void RemoveDiceAt(int id)
        {
            MaxValues.RemoveAt(id);
            RolledDice.RemoveAt(id);
        }

        internal void RemoveDiceAt(int[] ids) 
        {
            foreach (var item in ids)
            {
                RolledDice[item] = -1;
            }
            RolledDice.RemoveAll(t => t == -1);
        }

        internal void RemoveDiceRange(int id, int count)
        {
            MaxValues.RemoveRange(id, count);
            RolledDice.RemoveRange(id, count);
        }

        internal void RemoveLastDice()
        {
            MaxValues.RemoveAt(MaxValues.Count - 1);
            RolledDice.Remove(RolledDice.Count - 1);
        }

        internal void ClearDice()
        {
            RolledDice.Clear();
            foreach (var item in MaxValues)
            {
                RolledDice.Add(0);
            }
        }

        internal void SetNumberOfDice(int numberOfDice)
        {
            if(MaxValues.Count > numberOfDice)
            {
                MaxValues.RemoveRange(MaxValues.Count - numberOfDice - 1, numberOfDice);
                RolledDice.RemoveRange(RolledDice.Count - numberOfDice - 1, numberOfDice);
            }
            else if (MaxValues.Count < numberOfDice) 
            {
                MaxValues.AddRange(Enumerable.Repeat(MaxValues.Last(), MaxValues.Count - numberOfDice));
                RolledDice.AddRange(Enumerable.Repeat(0, RolledDice.Count - numberOfDice));
            }
        }
    }
}
