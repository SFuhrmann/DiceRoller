using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace DiceRoller
{
    /// <summary>
    /// Class that manages a list of rolled dice and provides functionality to re-roll, add and remove dice conveniently.
    /// </summary>
    internal class DiceRoller
    {
        // TODO:
        // Convert these lists to private readonly collections so you cannot modify them from outside

        /// <summary>
        /// The list of rolled dice. Should not be modified from outside.
        /// </summary>
        internal List<int> RolledDice { get; private set; }


        /// <summary>
        /// The list of max values of each dice.
        /// </summary>
        internal List<int> MaxValues { get; private set; } = new List<int>();
        internal bool DifferentMaxValues { get; private set; }

        /// <summary>
        /// Fires if the amount of dice had changed or if any max value had changed.
        /// </summary>
        /// <param name="newDice">The updated list of dice.</param>
        public delegate void DiceChanged(int[] newDice);

        /// <summary>
        /// Fires when the dice were rolled.
        /// </summary>
        /// <param name="rolledDice"></param>
        public delegate void DiceRolled(int[] rolledDice);

        private Random rndGenerator = new Random();

        /// <summary>
        /// Creates a Dice Roller instance with <paramref name="diceCount"/> dice each of which are set to roll <paramref name="maxValue"/> at max.
        /// </summary>
        /// <param name="diceCount">Initial number of dice. The dice lists are allocated with that much memory.</param>
        /// <param name="maxValue">Initial maximum roll value of each dice created.</param>
        internal DiceRoller(int diceCount, int maxValue)
        {
            RolledDice = new List<int>(diceCount);
            MaxValues = new List<int>(diceCount);

            RolledDice.AddRange(Enumerable.Repeat(0, diceCount));
            MaxValues.AddRange(Enumerable.Repeat(maxValue, diceCount));
        }

        /// <summary>
        /// Rolls all dice.
        /// </summary>
        internal void Roll()
        {
            RolledDice.Clear();
            foreach (var item in MaxValues)
            {
                RolledDice.Add(rndGenerator.Next(item) + 1);
            }
        }

        /// <summary>
        /// Rolls the specified dice.
        /// </summary>
        /// <param name="ids">Specify the dice by ids. They do not need to be sorted or unique.</param>
        internal void Roll(int[] ids)
        {
            foreach (var item in ids)
            {
                RolledDice[item] = rndGenerator.Next(MaxValues[item]) + 1;
            }
        }

        /// <summary>
        /// Add one dice with the given <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="maxValue">Maximum roll value of the dice.</param>
        internal void AddDice(int maxValue)
        {
            MaxValues.Add(maxValue);
            RolledDice.Add(0);
        }

        /// <summary>
        /// Add multiple dice of the given <paramref name="maxValues"/>. The number of added dice equals the length of <paramref name="maxValues"/>.
        /// </summary>
        /// <param name="maxValues">A list of maximum roll values for the dice.</param>
        internal void AddDice(int[] maxValues)
        {
            MaxValues.AddRange(maxValues);
            RolledDice.AddRange(Enumerable.Repeat(0, maxValues.Length));
        }

        /// <summary>
        /// Adds new dice with the given <paramref name="maxValue"/>. Count is determined by <paramref name="diceCount"/>.
        /// </summary>
        /// <param name="maxValue">Maximum roll value for the newly added dice.</param>
        /// <param name="diceCount">Number of added dice.</param>
        internal void AddDice(int maxValue, int diceCount)
        {
            MaxValues.AddRange(Enumerable.Repeat(maxValue, diceCount));
            RolledDice.AddRange(Enumerable.Repeat(0, diceCount));
        }

        /// <summary>
        /// Removes the dice at the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Dice id to remove.</param>
        internal void RemoveDiceAt(int id)
        {
            MaxValues.RemoveAt(id);
            RolledDice.RemoveAt(id);
        }

        /// <summary>
        /// Removes the dice at the given <paramref name="ids"/>.
        /// </summary>
        /// <param name="ids">Dice id to remove.</param>
        internal void RemoveDiceAt(int[] ids) 
        {
            foreach (var item in ids)
            {
                RolledDice[item] = -1;
            }
            RolledDice.RemoveAll(t => t == -1);
        }

        /// <summary>
        /// Removes all dice starting at <paramref name="id"/>. Number of dice removed equals <paramref name="count"/>.
        /// </summary>
        /// <param name="id">Starting id for removal.</param>
        /// <param name="count">Number of removed dice.</param>
        internal void RemoveDiceRange(int id, int count)
        {
            MaxValues.RemoveRange(id, count);
            RolledDice.RemoveRange(id, count);
        }

        /// <summary>
        /// Removes the last dice of the list.
        /// </summary>
        internal void RemoveLastDice()
        {
            MaxValues.RemoveAt(MaxValues.Count - 1);
            RolledDice.Remove(RolledDice.Count - 1);
        }

        /// <summary>
        /// Removes all dice.
        /// </summary>
        internal void ClearDice()
        {
            RolledDice.Clear();
            foreach (var item in MaxValues)
            {
                RolledDice.Add(0);
            }
        }

        /// <summary>
        /// Changes the number of dice by <paramref name="diceCount"/>. If it is increasing the dice count, it will "copy" the last dice of the list.
        /// </summary>
        /// <param name="diceCount">New number of dice count.</param>
        internal void SetNumberOfDice(int diceCount)
        {
            if(MaxValues.Count > diceCount)
            {
                MaxValues.RemoveRange(MaxValues.Count - diceCount - 1, diceCount);
                RolledDice.RemoveRange(RolledDice.Count - diceCount - 1, diceCount);
            }
            else if (MaxValues.Count < diceCount) 
            {
                MaxValues.AddRange(Enumerable.Repeat(MaxValues.Last(), MaxValues.Count - diceCount));
                RolledDice.AddRange(Enumerable.Repeat(0, RolledDice.Count - diceCount));
            }
        }
    }
}
