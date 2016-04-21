using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Abilities;
using Effects;

namespace Procs {

    public class Proc {

        public bool isDependent {
            get; set;
        }

        public bool hasDependent {
            get; set;
        }

        public Proc dependentProc;
        public Proc dependentUponProc;

    } //end AbilityProc class


    public class DamageProc : Proc {

        public enum DamageType {
            Physical,
            Magical,
            True
        }

        public DamageType damageType {
            get; set;
        }

        public bool isEvadeable {
            get; set;
        }

        public bool isBlockable {
            get; set;
        }

        public bool canCrit {
            get; set;
        }

        //stats

        public float critChance {
            get; set;
        }

        public float critMultiplier {
            get; set;
        }

        public float physicalPenetration {
            get; set;
        }

        public float magicalPenetration {
            get; set;
        }

        public float physicalAccuracy {
            get; set;
        }

        public float magicalAccuracy {
            get; set;
        }

        public float physicalFinesse {
            get; set;
        }

        public float magicalFinesse {
            get; set;
        }


        //nuts & bolts

        public float procStartDelay {
            get; set;
        }

        public float nextProcTimer {
            get; set;
        }

        public int procCounter {
            get; set;
        }

        public int procLimit {
            get; set;
        }

        public float procSpacing {
            get; set;
        }

        public float procDamage {
            get; set;
        }


        public DamageProc() {

            isDependent = false;
            hasDependent = false;
            isEvadeable = true;
            isBlockable = true;
            canCrit = true;
            procStartDelay = 0;

        } //end Constructor()


        //Application functions

        public virtual void ApplyDamageProc(BattleObject attacker, BattleObject defender) {

            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(attacker, defender, this, 1));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply, damageType);

        } //end ApplyDamageProc(2)


        public virtual void ApplyCritDamageProc(BattleObject attacker, BattleObject defender) {

            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(attacker, defender, this, critMultiplier));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply, damageType);
            //could call SpawnCritText, or not

        } //end ApplyCritDamageProc(2)


        public virtual void ApplyBlockDamageProc(BattleObject attacker, BattleObject defender) {

            float blockModifier = 0;

            if (damageType == DamageType.Physical) {
                blockModifier = defender.physicalBlockModifier;
            }
            else if (damageType == DamageType.Magical) {
                blockModifier = defender.magicalBlockModifier;
            }

            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(attacker, defender, this, (1-(blockModifier/100))));
            defender.currentHealth -= damageToApply;
            defender.SpawnBlockText(damageToApply, damageType);

        } //end ApplyBlockDamageProc(2)


        public virtual void ApplyInfDamageProc(BattleObject attacker, BattleObject defender, HeroAbility ability) {

            ability.isInfCharging = false;
            int damage = Mathf.RoundToInt(ability.infProcMultiplier * (Time.time - ability.infChargeStartTimer));
            defender.currentHealth -= damage;
            defender.SpawnDamageText(damage, damageType);

        } //end ApplyInfDamageProc(2)
        
    } //end DamageProc class


    public class HealProc : Proc {
        
        public bool isRevive {
            get; set;
        }


        //stats

        public float critChance {
            get; set;
        }

        public float critMultiplier {
            get; set;
        }


        //nuts & bolts

        public float procStartDelay {
            get; set;
        }

        public float nextProcTimer {
            get; set;
        }

        public int procCounter {
            get; set;
        }

        public int procLimit {
            get; set;
        }

        public float procSpacing {
            get; set;
        }

        public float procHeal {
            get; set;
        }


        public HealProc() {

            isDependent = false;
            isRevive = false;
            procStartDelay = 0;

        } //end Constructor()


        //Application functions

        public virtual void HealProcSingle(BattleObject healer, BattleObject healee) {

            int heal;
            if ((healee.currentHealth + procHeal) <= healee.maxHealth) {
                heal = Mathf.RoundToInt(procHeal);
            }
            else {
                heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
            }
            healee.currentHealth += heal;
            healee.SpawnHealText(heal);

        } //end HealProcSingle


        public virtual void HealProcMultiple(BattleObject healer, Ability ability) {

            foreach (BattleObject healee in ability.targetBattleObjectList) {
                HealProcSingle(healer, healee);
            }

        } //end HealProcMultiple


        public virtual void InfHealProcSingle(BattleObject healer, BattleObject healee, HeroAbility ability) {

            ability.isInfCharging = false;
            int heal;
            if (healee.currentHealth + (ability.infProcMultiplier * (Time.time - ability.infChargeStartTimer)) <= healee.maxHealth) {
                heal = Mathf.RoundToInt(ability.infProcMultiplier * (Time.time - ability.infChargeStartTimer));
            }
            else {
                heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
            }
            healee.currentHealth += heal;
            healee.SpawnHealText(heal);

        } //end InfHealProcSingle(3)

        public virtual void InfHealProcMultiple(BattleObject healer, HeroAbility ability) {

            ability.isInfCharging = false;
            foreach (BattleObject healee in ability.targetBattleObjectList) {
                InfHealProcSingle(healer, healee, ability);
            }

        } //end InfHealProcMultiple
        
    } //end HealProc class


    public class EffectProc : Proc {

        public Effect effectApplied;

        public float chanceToApply {
            get; set;
        }

        public float resolveScale {
            get; set;
        }

        public int stacksApplied {
            get; set;
        }


        public EffectProc() {

            isDependent = true;
            chanceToApply = 100;
            resolveScale = 1;
            stacksApplied = 1;

        } //end Constructor()


        //Application functions

        public virtual void ApplyEffectSingle(Effect effect, BattleObject target) {
            if (effect.effectType == Effect.EffectType.Stacking) {
                effect.stackCount += stacksApplied;
                effect.CreateStackingEffectSingle(target, stacksApplied);
            }
            else {
                effect.CreateEffectSingle(target);
            }
        } //end ApplyEffectSingle(2)


        public virtual void ApplyEffectMultiple(Effect effect, Ability ability) {
            if (effect.effectType == Effect.EffectType.Stacking) {
                effect.stackCount += stacksApplied;
                effect.CreateStackingEffectMultiple(ability.targetBattleObjectList, stacksApplied);
            }
            else {
                effect.CreateEffectMultiple(ability.targetBattleObjectList);
            }
        } //end ApplyEffectMultiple(2)
        
    } //end EffectProc class

    
} //end Procs namespace