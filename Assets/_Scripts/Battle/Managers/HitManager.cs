using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;
using Procs;
using EnemyAbilities;

public class HitManager : MonoBehaviour {
    

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

            evasionChance = defender.ApplyStatModifications(defender.physicalEvasionChance, defender.physicalEvasionChanceMultMod, defender.physicalEvasionChanceAddMod);
            accuracy = attacker.ApplyStatModifications((ability.physicalAccuracy + attacker.physicalAccuracy), attacker.physicalAccuracyMultMod, attacker.physicalAccuracyAddMod);

            blockChance = defender.ApplyStatModifications(defender.physicalBlockChance, defender.physicalBlockChanceMultMod, defender.physicalBlockChanceAddMod);
            finesse = attacker.ApplyStatModifications((ability.physicalFinesse + attacker.physicalFinesse), attacker.physicalFinesseMultMod, attacker.physicalFinesseAddMod);
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {

            evasionChance = defender.ApplyStatModifications(defender.magicalEvasionChance, defender.magicalEvasionChanceMultMod, defender.magicalEvasionChanceAddMod);
            accuracy = attacker.ApplyStatModifications((ability.magicalAccuracy + attacker.magicalAccuracy), attacker.magicalAccuracyMultMod, attacker.magicalAccuracyAddMod);

            blockChance = defender.ApplyStatModifications(defender.magicalBlockChance, defender.magicalBlockChanceMultMod, defender.magicalBlockChanceAddMod);
            finesse = attacker.ApplyStatModifications((ability.magicalFinesse + attacker.magicalFinesse), attacker.magicalFinesseMultMod, attacker.magicalFinesseAddMod);
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


    public static HitOutcome DetermineCrit(BattleObject attacker, BattleObject defender, DamageProc damageProc) {

        float critChance = 0;

        if (damageProc.damageType == DamageProc.DamageType.Physical) {
            critChance = attacker.ApplyStatModifications((damageProc.critChance + attacker.physicalCritChance), attacker.physicalCritChanceMultMod, attacker.physicalCritChanceAddMod);
        }
        else if (damageProc.damageType == DamageProc.DamageType.Magical) {
            critChance = attacker.ApplyStatModifications((damageProc.critChance + attacker.magicalCritChance), attacker.magicalCritChanceMultMod, attacker.magicalCritChanceAddMod);
        }

        int critCheck = Random.Range(1, 100); 
        if (critCheck <= critChance) {
            return HitOutcome.Crit;
        }
        else {
            return HitOutcome.Hit;
        }

    } //end DetermineCrit(3)


    public static float ApplyResist(BattleObject attacker, BattleObject defender, DamageProc damageProc, float modifier) {

        float resist = 0;
        float penetration = 0;
        float finalDamage;

        if (damageProc.damageType == DamageProc.DamageType.Physical) {
            resist = defender.ApplyStatModifications(defender.armor, defender.armorMultMod, defender.armorAddMod);
            penetration = damageProc.physicalPenetration
                + attacker.ApplyStatModifications(attacker.physicalPenetration, attacker.physicalPenetrationMultMod, attacker.physicalPenetrationAddMod);
        }
        else if (damageProc.damageType == DamageProc.DamageType.Magical) {
            resist = defender.ApplyStatModifications(defender.spirit, defender.spiritMultMod, defender.spiritAddMod);
            penetration = damageProc.magicalPenetration
                + attacker.ApplyStatModifications(attacker.magicalPenetration, attacker.magicalPenetrationMultMod, attacker.magicalPenetrationAddMod);
        }
        

        resist *= (1 - (penetration / 100));
        finalDamage = ((damageProc.procDamage * modifier) * (100 / (resist + 100)));
        return finalDamage;
        
    } //end ApplyResistNew(3)


    //OLD OLD OLD NOT SURE WHY IT'S HERE

    public static float ApplyResistOLD(BattleObject attacker, BattleObject defender, Ability ability, float rawDamage) {

        float resist = 0;
        float penetration = 0;
        float finalDamage;

        if (ability.primaryDamageType == Ability.DamageType.Physical) {
            resist = defender.ApplyStatModifications(defender.armor, defender.armorMultMod, defender.armorAddMod);
            penetration = ability.physicalPenetration 
                + attacker.ApplyStatModifications(attacker.physicalPenetration, attacker.physicalPenetrationMultMod, attacker.physicalPenetrationAddMod);
        }
        else if (ability.primaryDamageType == Ability.DamageType.Magical) {
            resist = defender.ApplyStatModifications(defender.spirit, defender.spiritMultMod, defender.spiritAddMod);
            penetration = ability.magicalPenetration 
                + attacker.ApplyStatModifications(attacker.magicalPenetration, attacker.magicalPenetrationMultMod, attacker.magicalPenetrationAddMod);
        }

        resist *= (1 - (penetration / 100));
        finalDamage = (rawDamage * (100/(resist+100)));
        return finalDamage;

    } //end ApplyResist(4)


} //end HitManager class 
