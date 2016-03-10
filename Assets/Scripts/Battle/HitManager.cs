using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;

public class HitManager : MonoBehaviour {

    public enum HitOutcome {
        Evaded,
        Blocked,
        CriticalHit,
        StandardHit
    }


	public static HitOutcome DetermineHitOutcome (BattleObject attacker, BattleObject defender, Ability ability) {

        float evasionChance = 0;
        float blockChance = 0;
        float accuracy = 0;
        float finesse = 0;

        //Set the local variables to the appropriate set, per the ability's DamageType
        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            evasionChance = defender.physicalEvasionChance;
            blockChance = defender.physicalBlockChance;
            accuracy = ability.physicalAccuracy;
            finesse = ability.physicalFinesse;
        } else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            evasionChance = defender.magicalEvasionChance;
            blockChance = defender.magicalBlockChance;
            accuracy = ability.magicalAccuracy;
            finesse = ability.magicalFinesse;
        }

        //Determine evasion
        int evadeCheck = Random.Range(1, 100);
        if (evadeCheck <= (evasionChance * (1- (accuracy / 100)))) {
            return HitOutcome.Evaded;
        }
        
        //Determine block
        int blockCheck = Random.Range(1, 100);
        if (blockCheck <= (blockChance * (1 - (finesse / 100)))) {
            return HitOutcome.Blocked;
        }

        //Determine crit
        int critCheck = Random.Range(1, 100);
        if (critCheck <= ability.critChance) {
            return HitOutcome.CriticalHit;
        }

        else {
            return HitOutcome.StandardHit;
        }
        
    } //end DetermineHitOutcome(3)
	



} //end HitManager class 
