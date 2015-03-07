using System.IO;

namespace TextRPG.Structures
{
    public class Item
    {

        #region Private Var
        private string _name, _desc;
        private byte _effect, _target, _turnLength;
        private bool _battle;
        private ushort _id, _effectMag;
        #endregion

        /// <summary>Constructor for a game item.</summary>
        /// <param name="rdr">The binary reader to load Item attributes.</param>
        public Item(BinaryReader rdr)
        {
            _id = rdr.ReadUInt16();
            _name = rdr.ReadString();
            _desc = rdr.ReadString();
            _effect = rdr.ReadByte();
            _effectMag = rdr.ReadUInt16();
            _turnLength = rdr.ReadByte();
            _target = rdr.ReadByte();
            _battle = rdr.ReadBoolean();
        }

        #region Public Var
        /// <summary>ID of the item.</summary>
        public ushort ID { get { return _id; } }

        /// <summary>Name of the item. <i>(eg. "Health Potion")</i></summary>
        public string Name { get { return _name; } }

        /// <summary>Description of the item. <i>(eg. "Heals 20 health.")</i></summary>
        public string Description { get { return _desc; } }

        /// <summary>Effect of the item. <i>(eg. "Heal" or "StrengthDown"</i></summary>
        public EffectType Effect { get { return (EffectType)_effect; } }

        public ushort EffectMagnitude { get { return _effectMag; } }

        /// <summary>Lenth in turns that the effect lasts.</summary>
        public byte TurnLength { get { return _turnLength; } }

        /// <summary>Whether the item is usable in battle.</summary>
        public bool Battle { get { return _battle; } }

        /// <summary>Whether the item is used on the player or not.</summary>
        public TargetType Target { get { return (TargetType)_target; } }
        #endregion

        /// <summary>The target the item effects.</summary>
        public enum TargetType : byte
        {
            Self = 0,
            Enemy = 1,
            Pet = 2
        }

        /// <summary>Type of effect the item has.</summary>
        public enum EffectType : byte
        {
            Heal = 0,
            HealOther,
            ReviveOther,
            Poison,
            ManaUp,
            ManaDown,
            StrengthUp,
            StrengthDown,
            DefenseUp,
            DefenseDown,
            AgilityUp,
            AgilityDown,
            StaminaUp,
            StaminaDown,
            IntellectUp,
            IntellectDown,
            LuckUp,
            ExpUp,
            LevelUp
        }
    }
}

//todo: Add rarity for drop rates
