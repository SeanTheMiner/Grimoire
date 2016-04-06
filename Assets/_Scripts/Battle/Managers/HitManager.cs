using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;
using EnemyAbilities;

public class HitManager : MonoBehaviour {
    
    //Abilities call these, in order.
    //HERO SET FIRST - this feels so ugly, but, well, here we are.

    public enum HitOutcome {
        Evade,
        Block,
        Hit,
        Crit
    }


    public static HitOutcome DetermineEvasionAndBlock(BattleObject attacker, BattleObject defender, Ability ability) {

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

        float evasionCeiling = (evasionChance * (1 - (accuracy / 100)));
        float blockCeiling = evasionCeiling + (blockChance * (1 - (finesse / 100)));
        
        if ((blockChance + evasionChance) > 100) {
            maxRange = Mathf.CeilToInt(blockChance + evasionChance);
        }

        float checker = Random.Range(1, maxRange);

        if (checker <= evasionCeiling) {
            return HitOutcome.Evade;
        }
        if (checker <= blockCeiling) {
            return HitOutcome.Block;
        }
        else {
            return HitOutcome.Hit;
        }

    } //end DetermineEvasionAndBlock (3)


    public static HitOutcome DetermineCrit(BattleObject attacker, BattleObject defender, Ability ability) {

        float critChance = ability.critChance;
        int critCheck = Random.Range(1, 100); 
        if (critCheck <= critChance) {
            return HitOutcome.Crit;
        }
        else {
            return HitOutcome.Hit;
        }

    } //end DetermineCrit(3)


    public static float ApplyResist(BattleObject attacker, BattleObject defender, Ability ability, float rawDamage) {

        float resist = 0;
        float penetration = 0;
        float finalDamage;
        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            resist = defender.ApplyStatModifications(defender.armor, defender.armorMultMod, defender.armorAddMod);
            penetration = ability.physicalPenetration;
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            resist = defender.ApplyStatModifications(defender.spirit, defender.spiritMultMod, defender.spiritAddMod);
            penetration = ability.magicalPenetration;
        }

        resist *= (1 - (penetration / 100));
        finalDamage = (rawDamage * (100/(resist+100)));
        return finalDamage;

    } //end ApplyResist(4)

    


    //END HERO SET, BEGIN ENEMY SET



    public static HitOutcome EnemyDetermineEvasionAndBlock(BattleObject attacker, BattleObject defender, EnemyAbility ability) {

        float evasionChance = 0;
        float blockChance = 0;
        float accuracy = 0;
        float finesse = 0;
        float maxRange = 100;

        if (ability.primaryDamageType == EnemyAbility.DamageType.Physical) {
            evasionChance = defender.physicalEvasionChance;
            accuracy = ability.physicalAccuracy;
            blockChance = defender.physicalBlockChance;
            finesse = ability.physicalFinesse;
        }
        else if (ability.primaryDamageType == EnemyAbility.DamageType.Magical) {
            evasionChance = defender.magicalEvasionChance;
            accuracy = ability.magicalAccuracy;
            blockChance = defender.magicalBlockChance;
            finesse = ability.magicalFinesse;
        }

        float evasionCeiling = (evasionChance * (1 - (accuracy / 100)));
        float blockCeiling = evasionCeiling + (blockChance * (1 - (finesse / 100)));

        if ((blockChance + evasionChance) > 100) {
            maxRange = Mathf.CeilToInt(blockChance + evasionChance);
        }

        float checker = Random.Range(1, maxRange);

        if (checker <= evasionCeiling) {
            return HitOutcome.Evade;
        }
        if (checker <= blockCeiling) {
            return HitOutcome.Block;
        }
        else {
            return HitOutcome.Hit;
        }

    } //end DetermineEvasionAndBlock (3)


    public static HitOutcome EnemyDetermineCrit(BattleObject attacker, BattleObject defender, EnemyAbility ability) {

        float critChance = ability.critChance;
        int critCheck = Random.Range(1, 100);
        if (critCheck <= critChance) {
            return HitOutcome.Crit;
        }
        else {
            return HitOutcome.Hit;
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
