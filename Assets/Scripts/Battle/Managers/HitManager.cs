using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;
using EnemyAbilities;

public class HitManager : MonoBehaviour {
    
    //Abilities call these, in order.
    //HERO SET FIRST - this feels so ugly, but, well, here we are.


    public static int DeterminEvasionAndBlock(BattleObject attacker, BattleObject defender, Ability ability) {

        float evasionChance = 0;
        float blockChance = 0;
        float accuracy = 0;
        float finesse = 0;
        float maxRange = 100;

        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            evasionChance = defender.physicalEvasionChance;
            accuracy = ability.physicalAccuracy;
            blockChance = defender.physicalBlockChance;
            finesse = ability.physicalFinesse;
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            evasionChance = defender.magicalEvasionChance;
            accuracy = ability.magicalAccuracy;
            blockChance = defender.magicalBlockChance;
            finesse = ability.magicalFinesse;
        }

        float evasionCeiling = evasionChance;
        float blockCeiling = evasionChance + blockChance;
        
        if ((blockChance + evasionChance) > 100) {
            maxRange = Mathf.CeilToInt(blockChance + evasionChance);
        }

        float checker = Random.Range(1, maxRange);

        if (checker <= evasionCeiling) {
            return 1;
        }
        if (checker <= blockCeiling) {
            return 2;
        }
        else {
            return 0;
        }

    } //end DetermineEvasionAndBlock (3)


    public static bool DetermineEvasion(BattleObject attacker, BattleObject defender, Ability ability) {

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

    
    public static bool DetermineBlock(BattleObject attacker, BattleObject defender, Ability ability) {

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





    //END HERO SET, BEGIN ENEMY SET





    public static bool EnemyDetermineEvasion(BattleObject attack, BattleObject defender, EnemyAbility ability) {

        float evasionChance = 0;
        float accuracy = 0;
        if (ability.primaryDamageType == EnemyAbility.DamageType.Physical) {
            evasionChance = defender.physicalEvasionChance;
            accuracy = ability.physicalAccuracy;
        }
        else if (ability.primaryDamageType == EnemyAbility.DamageType.Magical) {
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

    } //end EnemyDetermineEvasion(3)


    public static bool EnemyDetermineBlock(BattleObject attack, BattleObject defender, EnemyAbility ability) {

        float blockChance = 0;
        float finesse = 0;
        if (ability.primaryDamageType == EnemyAbility.DamageType.Physical) {
            blockChance = defender.physicalBlockChance;
            finesse = ability.physicalFinesse;
        }
        else if (ability.primaryDamageType == EnemyAbility.DamageType.Magical) {
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

    } //end EnemyDetermineBlock(3)


    public static bool EnemyDetermineCrit(BattleObject attacker, BattleObject defender, EnemyAbility ability) {

        float critChance = ability.critChance;
        int critCheck = Random.Range(1, 100);
        if (critCheck <= critChance) {
            return true;
        }
        else {
            return false;
        }

    } //end EnemyDetermineCrit(3)


    public static float EnemyApplyResist(BattleObject attacker, BattleObject defender, EnemyAbility ability, float rawDamage) {

        float resist = 0;
        float penetration = 0;
        float finalDamage;
        if (ability.primaryDamageType == EnemyAbility.DamageType.Physical) {
            resist = defender.armor;
            penetration = ability.physicalPenetration;
        }
        else if (ability.primaryDamageType == EnemyAbility.DamageType.Magical) {
            resist = defender.spirit;
            penetration = ability.magicalPenetration;
        }

        resist *= (1 - (penetration / 100));
        finalDamage = (rawDamage * (100 / (resist + 100)));
        return finalDamage;

    } //end EnemyApplyResist(4)



} //end HitManager class 
