﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaJanna.Config.ModesMenu.Harass;
using SettingsMana = VodkaJanna.Config.ManaManagerMenu;

namespace VodkaJanna.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
           
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.UseQ && QCastable() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Debug.WriteChat("Casting Q in Harass, Target: {0}, Distance: {1}, Prediction: {2}", target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                    Q.Cast(pred.CastPosition);
                    Core.DelayAction(() => { Q.Cast(pred.CastPosition); }, 10);
                }

            }
            if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target != null)
                {
                    W.Cast(target);
                }
            }
        }
    }
}
