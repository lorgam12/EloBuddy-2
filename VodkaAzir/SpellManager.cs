﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace VodkaAzir
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
        public static Spell.Active Recall { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear);
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular);
            E = new Spell.Active(SpellSlot.E, 1100);
            R = new Spell.Skillshot(SpellSlot.R, 250, SkillShotType.Linear);

            Recall = new Spell.Active(SpellSlot.Recall);

            if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Equals("summonerdot", StringComparison.CurrentCultureIgnoreCase))
            {
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            }
            else if ((Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Equals("summonerdot", StringComparison.CurrentCultureIgnoreCase)))
            {
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);
            }

            // Auto-learn W at level 1
            if (Player.Instance.Spellbook.GetSpell(SpellSlot.W).Level - 1 == 0 && Player.Instance.Spellbook.CanSpellBeUpgraded(SpellSlot.W))
                Player.Instance.Spellbook.LevelSpell(SpellSlot.W);
        }

        public static void Initialize()
        {

        }

        public static bool HasIgnite()
        {
            return Ignite != null;
        }
    }
}
