using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Effects;
using Procs;
using ProcTriggers;

namespace BattleObjects {

    //BattleObjects encompass Heroes and Enemies.
    //This way, if something treats both the same, you only have to write code once.

    //Note "AddMod" and "MultMod" - additive modifiers and multiplicative modifiers. 
    //These are initialized at 1 and 0, and buffs modify these rather than the base stats, 
    //Which should only be modified by level things? God maybe.

    public class BattleObject : MonoBehaviour {

        public List<Effect> effectList = new List<Effect>();
        public List<ProcTrigger> procTriggerList = new List<ProcTrigger>();

        public EffectDisplayController effectDisplayController;

        public Effect coreEffect {
            get; set;
        }


        //Health & modifiers

        public float maxHealth {
            get; set;
        }

        public float currentHealth {
            get; set;
        }


        public float healthRegen {
            get; set;
        }

        public float healthRegenMultMod {
            get; set;
        }

        public float healthRegenAddMod {
            get; set;
        }


        //Offensive stats & modifiers
        
        public float physicalPenetration {
            get; set;
        }

        public float physicalPenetrationMultMod {
            get; set;
        }

        public float physicalPenetrationAddMod {
            get; set;
        }


        public float physicalAccuracy {
            get; set;
        }

        public float physicalAccuracyMultMod {
            get; set;
        }

        public float physicalAccuracyAddMod {
            get; set;
        }


        public float physicalFinesse {
            get; set;
        }

        public float physicalFinesseMultMod {
            get; set;
        }

        public float physicalFinesseAddMod {
            get; set;
        }


        public float physicalCritChance {
            get; set;
        }

        public float physicalCritChanceAddMod {
            get; set;
        }

        public float physicalCritChanceMultMod {
            get; set;
        }


        public float magicalPenetration {
            get; set;
        }

        public float magicalPenetrationMultMod {
            get; set;
        }

        public float magicalPenetrationAddMod {
            get; set;
        }


        public float magicalAccuracy {
            get; set;
        }

        public float magicalAccuracyMultMod {
            get; set;
        }

        public float magicalAccuracyAddMod {
            get; set;
        }


        public float magicalFinesse {
            get; set;
        }

        public float magicalFinesseMultMod {
            get; set;
        }

        public float magicalFinesseAddMod {
            get; set;
        }


        public float magicalCritChance {
            get; set;
        }

        public float magicalCritChanceAddMod {
            get; set;
        }

        public float magicalCritChanceMultMod {
            get; set;
        }


        public float physicalAttackSpeedMultMod {
            get; set;
        }

        public float magicalAttackSpeedMultMod {
            get; set;
        }
        

        public float physicalLifeSteal {
            get; set;
        }

        public float physicalLifeStealMultMod {
            get; set;
        }

        public float physicalLifeStealAddMod {
            get; set;
        }


        public float magicalLifeSteal {
            get; set;
        }

        public float magicalLifeStealMultMod {
            get; set;
        }

        public float magicalLifeStealAddMod {
            get; set;
        }


        //Defensive stats & modifiers

        public float armor {
            get; set;
        }

        public float armorMultMod {
            get; set;
        }

        public float armorAddMod {
            get; set;
        }


        public float spirit {
            get; set;
        }

        public float spiritMultMod {
            get; set;
        }

        public float spiritAddMod {
            get; set;
        }


        public float physicalEvasionChance {
            get; set;
        }

        public float physicalEvasionChanceMultMod {
            get; set;
        }

        public float physicalEvasionChanceAddMod {
            get; set;
        }


        public float magicalEvasionChance {
            get; set;
        }

        public float magicalEvasionChanceMultMod {
            get; set;
        }
        
        public float magicalEvasionChanceAddMod {
            get; set;
        }


        public float physicalBlockChance {
            get; set;
        }

        public float physicalBlockChanceMultMod {
            get; set;
        }
        
        public float physicalBlockChanceAddMod {
            get; set;
        }


        public float physicalBlockModifier {
            get; set;
        }

        public float physicalBlockModifierMultMod {
            get; set;
        }

        public float physicalBlockModifierAddMod {
            get; set;
        }


        public float magicalBlockChance {
            get; set;
        }

        public float magicalBlockChanceMultMod {
            get; set;
        }

        public float magicalBlockChanceAddMod {
            get; set;
        }

        
        public float magicalBlockModifier {
            get; set;
        }

        public float magicalBlockModifierMultMod {
            get; set;
        }

        public float magicalBlockModifierAddMod {
            get; set;
        }


        public float tenacity {
            get; set;
        }

        public float tenacityAddMod {
            get; set;
        }

        public float tenacityMultMod {
            get; set;
        }


        public float resolve {
            get; set;
        }

        public float resolveAddMod {
            get; set;
        }

        public float resolveMultMod {
            get; set;
        }
        

        public float incomingHealMultMod {
            get; set;
        }

        public float outgoingHealMultMod {
            get; set;
        }

        public float healSpeedMultMod {
            get; set;
        }



        //Constructor

        public BattleObject () {

            maxHealth = 0;
            currentHealth = 0;

            healthRegen = 0;
            healthRegenMultMod = 1;
            healthRegenAddMod = 0;

            physicalPenetration = 0;
            physicalPenetrationMultMod = 1;
            physicalPenetrationAddMod = 0;

            physicalAccuracy = 0;
            physicalAccuracyMultMod = 1;
            physicalAccuracyAddMod = 0;

            physicalFinesse = 0;
            physicalFinesseMultMod = 1;
            physicalFinesseAddMod = 0;

            physicalCritChance = 0;
            physicalCritChanceMultMod = 1;
            physicalCritChanceAddMod = 0;

            magicalPenetration = 0;
            magicalPenetrationMultMod = 1;
            magicalPenetrationAddMod = 0;

            magicalAccuracy = 0;
            magicalAccuracyMultMod = 1;
            magicalAccuracyAddMod = 0;

            magicalFinesse = 0;
            magicalFinesseMultMod = 1;
            magicalFinesseAddMod = 0;

            physicalAttackSpeedMultMod = 1;
            magicalAttackSpeedMultMod = 1;

            physicalLifeSteal = 0;
            physicalLifeStealMultMod = 1;
            physicalLifeStealAddMod = 0;

            magicalLifeSteal = 0;
            magicalLifeStealMultMod = 1;
            magicalLifeStealAddMod = 0;


            armor = 0;
            armorMultMod = 1;
            armorAddMod = 0;

            spirit = 0;
            spiritMultMod = 1;
            spiritAddMod = 0;
            
            physicalEvasionChance = 0;
            physicalEvasionChanceMultMod = 1;
            physicalEvasionChanceAddMod = 0;

            magicalEvasionChance = 0;
            magicalEvasionChanceMultMod = 1;
            magicalEvasionChanceAddMod = 0;

            physicalBlockChance = 0;
            physicalBlockChanceMultMod = 1;
            physicalBlockChanceAddMod = 0;

            physicalBlockModifier = 0;
            physicalBlockModifierMultMod = 1;
            physicalBlockModifierAddMod = 0;

            magicalBlockChance = 0;
            magicalBlockChanceMultMod = 1;
            magicalBlockChanceAddMod = 0;

            magicalBlockModifier = 0;
            magicalBlockModifierMultMod = 1;
            magicalBlockModifierAddMod = 0;

            tenacity = 0;
            tenacityMultMod = 1;
            tenacityAddMod = 0;

            resolve = 0;
            resolveMultMod = 1;
            resolveAddMod = 0;
            
            incomingHealMultMod = 1;
            outgoingHealMultMod = 1;
            healSpeedMultMod = 1;
            
        } //end Constructor


        //Native functions
       
        public float ApplyStatModifications(float baseStat, float multMod, float addMod) {
            return ((baseStat * multMod) + addMod);
        }
        

        //Spawning functions
        public virtual void SpawnDamageText(int damage, DamageProc.DamageType damageType) {

            GameObject damageTextPrefab = (GameObject)Instantiate(Resources.Load("DamageTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh damageTextMesh = damageTextPrefab.GetComponentInChildren<TextMesh>();
            damageTextMesh.text = damage.ToString();
            
            if (damageType == Procs.DamageProc.DamageType.Physical) {
                damageTextMesh.color = Color.red;
                damageTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.PhysicalHit;
            }
            else if (damageType == Procs.DamageProc.DamageType.Magical) {
                damageTextMesh.color = Color.blue;
                damageTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.MagicalHit;
            }
            
        } //end SpawnDamageText()


        public virtual void SpawnHealText(int heal) {

            GameObject healTextPrefab = (GameObject)Instantiate(Resources.Load("HealTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh healTextMesh = healTextPrefab.GetComponentInChildren<TextMesh>();
            healTextMesh.text = heal.ToString();
            
            healTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.Heal;

        } //end SpawnHealText()


        public virtual void SpawnMissText(Procs.DamageProc.DamageType damageType) {

            GameObject missTextPrefab = (GameObject)Instantiate(Resources.Load("MissTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh missTextMesh = missTextPrefab.GetComponentInChildren<TextMesh>();
            missTextMesh.text = "Miss!";
            
            if (damageType == Procs.DamageProc.DamageType.Physical) {
                missTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.PhysicalMiss;
            }
            else {
                missTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.MagicalMiss;
            }

        } //end SpawnDamageText()


        public virtual void SpawnBlockText(int damage, Procs.DamageProc.DamageType damageType) {

            GameObject blockTextPrefab = (GameObject)Instantiate(Resources.Load("BlockTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh blockTextMesh = blockTextPrefab.GetComponentInChildren<TextMesh>();
            blockTextMesh.text = damage.ToString();

            if (damageType == Procs.DamageProc.DamageType.Physical) {
                blockTextMesh.color = new Color(1, 0.5f, 0.5f, 1);
                blockTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.PhysicalBlock;
            }
            else {
                blockTextMesh.color = new Color(0.5f, 0.5f, 1, 1);
                blockTextPrefab.GetComponentInChildren<DamageTextObjects.DamageTextController>().hitType = DamageTextObjects.DamageTextController.HitType.MagicalBlock;
            }

        } //end SpawnDamageText() 


    } //end BattleObject class

} //end namespace
