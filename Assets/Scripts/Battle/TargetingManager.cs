using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Abilities;


public class TargetingManager : MonoBehaviour {

    public BattleManager battleManager;
    

    public void SortUntargetedType(Hero hero) {

        if (hero.targetingAbility.targetScope == Ability.TargetScope.AllEnemies) {
            TargetAllEnemies(hero);
        }

        else if(hero.targetingAbility.targetScope == Ability.TargetScope.AllHeroes) {
            TargetAllHeroes(hero);
        }
        

    } //end SortUntargetedType()


    public void CastSelecterRay(Hero hero) {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (hero.targetingAbility.targetScope == Ability.TargetScope.SingleEnemy)) {
                battleManager.TargetingToCurrent(hero);
                hero.currentAbility.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
                battleManager.ExecuteAbility(hero);
            } //end if enemy & enemytargetable

            else if((objectHit.collider.tag == "Hero") && (hero.currentAbility.targetScope == Ability.TargetScope.SingleHero)) {
                battleManager.TargetingToCurrent(hero);
                hero.currentAbility.targetHero = objectHit.collider.gameObject.GetComponent<Hero>();
                battleManager.ExecuteAbility(hero);
            } //end if hero & herotargetable
            
        }  //end if objectHit

    } //end CastSelecterRay
    
    //Initial unchosen targeting functions

    public void TargetAllEnemies(Hero hero) {
        battleManager.TargetingToCurrent(hero);
        foreach(Enemy enemyTarget in battleManager.enemyList) {
            hero.currentAbility.targetBattleObjectList.Add(enemyTarget);
        }
        battleManager.ExecuteAbility(hero);
    }

    public void RefreshTargetAllEnemies(Hero hero) {
        hero.currentAbility.targetBattleObjectList.Clear();
        foreach(Enemy enemyTarget in battleManager.enemyList) {
            hero.currentAbility.targetBattleObjectList.Add(enemyTarget);
        }
    }


    public void TargetAllHeroes(Hero hero) {
        battleManager.TargetingToCurrent(hero);
        foreach(Hero heroTarget in battleManager.heroList) {
            hero.currentAbility.targetBattleObjectList.Add(heroTarget);
        }
        battleManager.ExecuteAbility(hero);
    }

    //Not used right now - not sure if we'll need a random enemy picker for an ABILITY - proc, sure.
    public void TargetRandomEnemy(Hero hero) {
        battleManager.TargetingToCurrent(hero);
        hero.currentAbility.targetEnemy = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];
        battleManager.ExecuteAbility(hero);
    }


    //Other functions

    public void ReTargetRandomEnemy(Hero hero) {
        hero.currentAbility.targetEnemy = battleManager.enemyList[Random.Range(0, battleManager.enemyList.Count)];
        hero.currentAbility.SetBattleState();
    }


} //end TargetingManager
