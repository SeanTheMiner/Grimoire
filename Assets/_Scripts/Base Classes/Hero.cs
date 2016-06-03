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

        public bool isDead {
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

        public float reviveChannelDuration {
            get; set;
        }

        public float reviveChannelEndTimer {
            get; set;
        }

        public float reviveCostScale {
            get; set;
        }

        public float stunStartTimer {
            get; set;
        }


        //Under the hood

        [HideInInspector]
        public HeroAbility currentAbility, queuedAbility, selectedAbility, targetingAbility, defaultAbility;
        public HeroAbility abilityOne, abilityTwo, abilityThree, abilityFour, abilityFive, abilitySix;
        public Artifact artifactOne, artifactTwo, artifactThree;
        public Hero revivalTarget;
        public TargetingManager targetingManager = new TargetingManager();

        public BattleState currentBattleState {
            get; set;
        }

        public BattleState priorBattleState {
            get; set;
        }

        public enum BattleState {
            Wait,
            Stunned,
            Target,
            Charge,
            InfCharge,
            InfBarrage,
            Ability,
            Uncharge,
            Dead,
            Reviving,
            RevTarget
        }


        public Hero() {

            canTakeCommands = true;
            healthIsLocked = false;

            reviveChannelDuration = 5;
            reviveCostScale = 1;
            
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

        
        public void SetHeroToDead () {

            currentHealth = 0;
            currentMana = 0;
            currentBattleState = BattleState.Dead;
            gameObject.tag = "DeadHero";

            effectDisplayController.KillAllEffects(this);

            isDead = true;
            healthIsLocked = true;
            canTakeCommands = false;

            transform.position -= Vector3.forward * 0.2f;
            transform.position -= Vector3.right * 0.7f;
            transform.Rotate(0, 90, 0);

            selectedAbility = null;
            currentAbility = null;
            defaultAbility = null;

        } //end SetHeroToDead()


        private void ReviveHeroGeneral () {

            currentMana = maxMana;
            currentBattleState = BattleState.Wait;
            gameObject.tag = "Hero";

            isDead = false;
            healthIsLocked = false;
            canTakeCommands = true;

            transform.position += Vector3.forward * .2f;
            transform.position += Vector3.right * 0.7f;
            transform.Rotate(0, -90, 0);

        } //end ReviveHeroGeneral()


        public void ReviveHeroPercentage (float percent) {

            ReviveHeroGeneral();
            currentHealth = maxHealth * (percent / 100);

        } //end ReviveHeroPercentage(1)


        public void ReviveHeroFlat (float flatHealth) {

            ReviveHeroGeneral();
            currentHealth = flatHealth;

        } //end ReviveHeroFlat(1)


        void LateUpdate() {

            if ((currentHealth <= 0) && (!isDead)) { 
                SetHeroToDead();
            }

        } //end LateUpdate()


        public void InitRevival () {

            currentBattleState = BattleState.Reviving;

            currentAbility = null;
            selectedAbility = null;

            reviveChannelEndTimer = Time.time + reviveChannelDuration;

        } //end InitRevival()


        public void CheckRevivalCharge () {

            if (reviveChannelEndTimer <= Time.time) {
                revivalTarget.ReviveHeroPercentage(30);
                currentHealth -= 150;
                //will need to refer to whereve the actual dynamic cost is,
                //This calling a virtual ReviveHero function will be the way to go.
                currentBattleState = BattleState.Wait;
            }

        } //end CheckRevivalCharge()


        public void InitStun () {

            priorBattleState = currentBattleState;
            currentBattleState = BattleState.Stunned;
            canTakeCommands = false;

            stunStartTimer = Time.time;
            
        } //End PrepForStun()


        public void ExitStun () {

            currentBattleState = priorBattleState;
            priorBattleState = BattleState.Stunned;
            canTakeCommands = true;

            if (currentBattleState == BattleState.Charge) {
                currentAbility.chargeEndTimer += (Time.time - stunStartTimer);
            }
            if (currentBattleState == BattleState.Reviving) {
                reviveChannelEndTimer += (Time.time - stunStartTimer);
            } 

            //things using stunStartTimer

        }



    } //end Hero class

} //end Heroes namespace
