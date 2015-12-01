﻿using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaWarwick.Config.ModesMenu.Combo;
using SettingsMana = VodkaWarwick.Config.ManaManagerMenu;

namespace VodkaWarwick.Modes
{
    public sealed class Combo : ModeBase
    {
        static Item Cutlass;
        static Item BOTRK;

        static Combo()
        {
            Cutlass = new Item(ItemId.Bilgewater_Cutlass, 450);
            BOTRK = new Item(ItemId.Blade_of_the_Ruined_King, 450);
        }

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            // Items
            if (Settings.UseItems)
            {
                var enemy = TargetSelector.GetTarget(BOTRK.Range, DamageType.Physical);
                if (enemy != null)
                {
                    if (CanUseItem(ItemId.Bilgewater_Cutlass))
                    {
                        Cutlass.Cast(enemy);
                        Debug.WriteChat("Using Bilgewater Cutlass on {0}", enemy.ChampionName);
                    }
                    else if (CanUseItem(ItemId.Blade_of_the_Ruined_King) &&
                             enemy.HealthPercent <= Settings.MaxBOTRKHPEnemy &&
                             PlayerHealth <= Settings.MaxBOTRKHPPlayer)
                    {
                        BOTRK.Cast(enemy);
                        Debug.WriteChat("Using BOTRK on {0}", enemy.ChampionName);
                    }
                }
            }
            // Skills
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                AIHeroClient target = null;
                if (TargetSelector.SeletedEnabled && TargetSelector.SelectedTarget != null)
                {
                    target = TargetSelector.SelectedTarget;
                }
                else
                {
                    target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                }
                if (target != null && target.IsValidTarget() && !target.HasBuffOfType(BuffType.SpellImmunity) && !target.HasBuffOfType(BuffType.SpellShield))
                {
                    R.Cast(target);
                }
            }
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null && target.IsValidTarget() && !target.HasBuffOfType(BuffType.SpellImmunity) &&
                    !target.HasBuffOfType(BuffType.SpellShield))
                {
                    Q.Cast(target);
                }
            }
        }

        private bool CanUseItem(ItemId id)
        {
            return Item.HasItem(id) && Item.CanUseItem(id);
        }
    }
}
