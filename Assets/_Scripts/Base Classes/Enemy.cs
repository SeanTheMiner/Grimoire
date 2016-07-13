using UnityEngine;
using System.Collections.Generic;
using EnemyAbilities;
using BattleObjects;

namespace Enemies {

    public class Enemy : BattleObject {

        public string enemyName {
            get; set;
        }
        
        public Vector3 battlePosition {
            get; set;
        }
        
        public Biome homeBiome {
            get; set;
        }

        public enum Biome {
            RedBiome,
            BlueBiome
        }

        public EnemyState currentEnemyState {
            get; set;
        }

        public enum EnemyState {
            Inactive,
            Stunned,
            Charge,
            Barrage,
        }

        public EnemyAbility currentEnemyAbility;
        public EnemyAbility enemyAbilityOne, enemyAbilityTwo, enemyAbilityThree;

        public List<EnemyAbility> enemyAbilityList = new List<EnemyAbility>();

        
        public void SetAbilityOwner() {

            enemyAbilityOne.abilityOwner = this;
            enemyAbilityTwo.abilityOwner = this;
            enemyAbilityThree.abilityOwner = this;
            
        } //end SetAbilityOwner(1)


        void LateUpdate() {

            if (currentHealth <= 0) {

                effectDisplayController.KillAllEffects(this);
                Destroy(gameObject);
            }

        } //end LateUpdate()


        public override void BattleStart() {
            currentHealth = maxHealth;
        }


    } //end Enemy class

} //end Enemies namespace