using System.Collections.Concurrent;
using System.IO;

namespace TextRPG.Structures
{
    public static class Character
    {
        #region Private Var
        private static byte _level, _luc;
        private static ushort _hp, _mp, _str, _sta, _agi, _int, _ap;
        private static uint[] _levelSet;
        private static uint _exp;
        #endregion

        /// <summary>The inventory structure of the character.</summary>
        private struct Inventory
        {
            /// <summary>Slot represents an inventory slot which can contain an item and its quantity.</summary>
            struct Slot
            {
                /// <summary>The Item representing the slot.</summary>
                public Item Item;
                /// <summary>The quantity of the item in the inventory.</summary>
                public byte Quantity;
            }

            private static ConcurrentDictionary<Item, Slot> _slots = new ConcurrentDictionary<Item, Slot>();

            /// <summary>Method used to remove an item from the inventory.</summary>
            /// <param name="id">The ID of the item.</param>
            /// <returns>Returns true if the method was able to find and remove an entry for the item specified.</returns>
            public static bool RemoveItem(ushort id)
            {
                Item item;
                Slot slot;

                if (!Dictionary.Items.TryGetValue(id, out item))
                    return false;

                if (!_slots.TryGetValue(item, out slot))
                    return false;

                if (slot.Quantity > 1) slot.Quantity--;
                else { slot.Quantity = 0; return _slots.TryRemove(slot.Item, out slot); }

                return true;
            }

            /// <summary>Method used to add an item to the inventory.</summary>
            /// <param name="id">The ID of the item.</param>
            /// <returns>Returns true if the method was able to find an entry in the global dictionary and add to inventory.</returns>
            public static bool AddItem(ushort id)
            {
                Item item;
                Slot slot;

                if (!Dictionary.Items.TryGetValue(id, out item))
                    return false;

                if (_slots.TryGetValue(item, out slot))
                    slot.Quantity++;
                else { _slots.TryAdd(item, new Slot() { Item = item, Quantity = 1 }); }

                return true;
            }
        }

        /// <summary>Function to load character data from specified BinaryReader stream.</summary>
        /// <param name="rdr"></param>
        public static void Load(BinaryReader rdr)
        {
            _levelSet = new uint[rdr.ReadByte()];
            for (int lvl = 0; lvl < _levelSet.Length; lvl++ )
            {
                _levelSet[lvl] = rdr.ReadUInt32();
            }
            //todo: Load rest of character data
        }

        /// <summary>Function to modify character data (player rewards) upon level up.</summary>
        public static void LevelUp()
        {
            _level++;
            _ap += 5;
            _exp = 0;
        } // adds AP, resets EXP

        #region Public Var
        /// <summary>Representation of the character's maximum health.</summary>
        public static ushort MaxHealth { get { return (ushort)(_sta * 5); } } // Multiplier of stamina

        /// <summary>The current health of the character.</summary>
        public static ushort Health
        {
            get { return _hp; }
            set { _hp = value; }
        }

        /// <summary>Represenation of the character's maximum mana.</summary>
        public static ushort MaxMana { get { return (ushort)(_int * 3.5); } } // Multiplier of intellect

        /// <summary>The current mana of the character.</summary>
        public static ushort Mana
        {
            get { return _mp; }
            set { _mp = value; }
        }

        /// <summary>The declared strength stat of the character.</summary>
        public static ushort Strength
        {
            get { return _str; }
            set { _str = value; }
        } // determines dmg, defense

        /// <summary>The declared stamina stat of the character.</summary>
        public static ushort Stamina // determines health
        {
            get { return _sta; }
            set { _sta = value; }
        }
        
        /// <summary>The declared agility stat of the character.</summary>
        public static ushort Agility // determines hit chance/crit
        {
            get { return _agi; }
            set { _agi = value; }
        }

        /// <summary>The declared intellect stat of the character.</summary>
        public static ushort Intellect // determines mana/spell spower
        {
            get { return _int; }
            set { _int = value; }
        }

        /// <summary>The luck modifer for the character.</summary>
        public static byte Luck{ get { return _luc; } } // adds to hit chance/crit + determines item quality

        /// <summary>Unalloted attribute points.</summary>
        public static ushort AttributePoints { get { return _ap; } }

        /// <summary>The declared level of the character.</summary>
        public static byte Level { get { return _level; } }

        /// <summary>Amount of experience towards next character level.</summary>
        public static uint Exp { get { return _exp; } }
        #endregion
    }
}
