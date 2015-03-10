using System.Collections.Concurrent;
using TextRPG.Structures;

namespace TextRPG
{
    /// <summary>
    /// Global collection reference.
    /// </summary>
    public static class Dictionary
    {
        /// <summary>Global collection of game items.</summary>
        public static ConcurrentDictionary<ushort, Item> Items = new ConcurrentDictionary<ushort, Item>();

        /// <summary>Gloabl collection of game monsters.</summary>
        public static ConcurrentDictionary<ushort, Monster> Monsters = new ConcurrentDictionary<ushort, Monster>();

        /// <summary>Gloabl collection of game quests.</summary>
        public static ConcurrentDictionary<ushort, Quest> Quests = new ConcurrentDictionary<ushort, Quest>();

        /// <summary>Global collection of character abilities.</summary>
        public static ConcurrentDictionary<ushort, Ability> Abilities = new ConcurrentDictionary<ushort, Ability>();

        /// <summary>Global collection of game gears for the character.</summary>
        public static ConcurrentDictionary<ushort, Gear> Gears = new ConcurrentDictionary<ushort, Gear>();
    }
}
