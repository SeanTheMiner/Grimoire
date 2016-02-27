using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Abilities;


public class TargetingManager : MonoBehaviour {

    public BattleManager battleManager;
    static System.Random randomer;

    public void CastSelecterRay(Hero hero) {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (hero.targetingAbility.targetScope == Ability.TargetScope.SingleEnemy)) {
                battleManager.TargetToCurrent(hero);
                hero.currentAbility.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
                battleManager.ExecuteAbility(hero);
            } //end if enemy & enemytargetable

            else if((objectHit.collider.tag == "Hero") && (hero.currentAbility.targetScope == Ability.TargetScope.SingleHero)) {
                battleManager.TargetToCurrent(hero);
                hero.currentAbility.targetHero = objectHit.collider.gameObject.GetComponent<Hero>();
                battleManager.ExecuteAbility(hero);
            } //end if hero & herotargetable

            battleManager.abilityTargeterActive = false;

        }  //end if objectHit

    } //end CastSelecterRay
    

    public void TargetAllEnemies(Hero hero) {
        battleManager.TargetToCurrent(hero);
        foreach(Enemy enemyTarget in battleManager.enemyList) {
            hero.currentAbility.targetEnemyList.Add(enemyTarget);
        }
        battleManager.ExecuteAbility(hero);
    }


    public void TargetAllHeroes(Hero hero) {
        battleManager.TargetToCurrent(hero);
        foreach(Hero heroTarget in battleManager.heroList) {
            hero.currentAbility.targetHeroList.Add(heroTarget);
        }
        battleManager. ExecuteAbility(hero);
    }


    public void TargetRandomEnemy(Hero hero) {
        battleManager.TargetToCurrent(hero);
        hero.currentAbility.targetEnemy = battleManager.enemyList[randomer.Next(battleManager.enemyList.Count)];
        battleManager.ExecuteAbility(hero);
    }


    public void ReTargetRandomEnemy(Hero hero) {
        hero.currentAbility.targetEnemy = battleManager.enemyList[randomer.Next(battleManager.enemyList.Count)];
        hero.currentAbility.SetBattleState();
    }


} //end TargetingManager
