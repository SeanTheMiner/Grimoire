using UnityEngine;
using System.Collections.Generic;

using BattleObjects;
using Abilities;
using Effects;
using Artifacts;

namespace Heroes {

    public class Hero : BattleObject {
        
        public string heroName {
            get; set;
        }

        public bool healthIsLocked {
            get; set;
        }

        public bool canTakeCommands {
            get; set;
        }

        //any stats that aren't inherited from BattleObject go here. 
        //I don't know what they'd be other than mana.

        public float maxMana {
            get; set;
        }

        public float currentMana {
            get; set;
        }

        public float manaRegen {
            get; set;
        }


        //Under the hood

        [HideInInspector]
        public HeroAbility currentAbility, queuedAbility, selectedAbility, targetingAbility, defaultAbility;
        public HeroAbility abilityOne, abilityTwo, abilityThree, abilityFour, abilityFive, abilitySix;
        public Artifact artifactOne, artifactTwo, artifactThree;
        
        public BattleState currentBattleState {
            get; set;
        }

        public enum BattleState {
            Wait,
            Target,
            Charge,
            InfCharge,
            InfBarrage,
            Ability,
            Uncharge,
            Dead
        }


        public Hero() {

            canTakeCommands = true;
            healthIsLocked = false;
            
        } //end constructor


        public virtual void BattleStart () {}


        public void SetAbilityOwner () {

            abilityOne.abilityOwner = this;
            abilityTwo.abilityOwner = this;
            abilityThree.abilityOwner = this;
            abilityFour.abilityOwner = this;
            abilityFive.abilityOwner = this;
            abilitySix.abilityOwner = this;

        } //end SetAbilityOwner(1)
        
        
        public void SetCoreEffects() {
            
            CheckAbilityForCoreEffect(abilityOne);
            CheckAbilityForCoreEffect(abilityTwo);
            CheckAbilityForCoreEffect(abilityThree);
            CheckAbilityForCoreEffect(abilityFour);
            CheckAbilityForCoreEffect(abilityFive);
            CheckAbilityForCoreEffect(abilitySix);
            
        } //end SetCoreEffects()


        public void CheckAbilityForCoreEffect(HeroAbility ability) {
            if (ability.appliesCoreEffect) {
                ability.coreEffectApplied = coreEffect;
            }
        } //end CheckAbilityForCoreEffect(1)

        
    } //end Hero class

} //end Heroes namespace
