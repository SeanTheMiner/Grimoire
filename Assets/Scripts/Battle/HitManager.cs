using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;

public class HitManager : MonoBehaviour {
    
    //Abilities call these, in order.

    public static bool DetermineEvasion(BattleObject attack, BattleObject defender, Ability ability) {

        float evasionChance = 0;
        float accuracy = 0;
        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            evasionChance = defender.physicalEvasionChance;
            accuracy = ability.physicalAccuracy;
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            evasionChance = defender.magicalEvasionChance;
            accuracy = ability.magicalAccuracy;
        }

        int evadeCheck = Random.Range(1, 100);
        if (evadeCheck <= (evasionChance * (1 - (accuracy / 100)))) {
            return true;
        }
        else {
            return false;
        }

    } //end DetermineEvasion(3)

    
    public static bool DetermineBlock(BattleObject attack, BattleObject defender, Ability ability) {

        float blockChance = 0;
        float finesse = 0;
        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            blockChance = defender.physicalBlockChance;
            finesse = ability.physicalFinesse;
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            blockChance = defender.magicalBlockChance;
            finesse = ability.magicalFinesse;
        }

        int blockCheck = Random.Range(1, 100);
        if (blockCheck <= (blockChance * (1 - (finesse / 100)))) {
            return true;
        }
        else {
            return false;
        }

    } //end DetermineBlock(3)


    public static bool DetermineCrit(BattleObject attacker, BattleObject defender, Ability ability) {

        float critChance = ability.critChance;
        int critCheck = Random.Range(1, 100); 
        if (critCheck <= critChance) {
            return true;
        }
        else {
            return false;
        }

    } //end DetermineCrit(3)


    public static float ApplyResist(BattleObject attacker, BattleObject defender, Ability ability, float rawDamage) {

        float resist = 0;
        float penetration = 0;
        float finalDamage;
        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            resist = defender.armor;
            penetration = ability.physicalPenetration;
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            resist = defender.spirit;
            penetration = ability.magicalPenetration;
        }

        resist *= (1 - (penetration / 100));
        finalDamage = (rawDamage * (100/(resist+100)));
        return finalDamage;

    } //end ApplyResist(4)

    
} //end HitManager class 
