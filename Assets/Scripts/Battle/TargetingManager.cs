using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Abilities;
using EnemyAbilities;
using BattleObjects;


public class TargetingManager {

    public void SortTargetingType(Ability ability) {

        if ((ability.targetScope == Ability.TargetScope.SingleEnemy) | (ability.targetScope == Ability.TargetScope.SingleHero) | (ability.targetScope == Ability.TargetScope.SingleHeroOrEnemy)) {
            ability.abilityOwner.currentBattleState = Hero.BattleState.Target;
        }
        else if (ability.targetScope == Ability.TargetScope.AllEnemies) {
            TargetAllEnemies(ability);
        }
        else if (ability.targetScope == Ability.TargetScope.AllHeroes) {
            TargetAllHeroes(ability);
        }
        
    } //end SortTargetingType


    public void CastSelecterRay(Ability ability) {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (ability.targetScope == Ability.TargetScope.SingleEnemy)) {
                ability.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
            } 

            else if((objectHit.collider.tag == "Hero") && (ability.targetScope == Ability.TargetScope.SingleHero)) {
                ability.targetHero = objectHit.collider.gameObject.GetComponent<Hero>();
            } 

            else {
                return;
            }

            if (ability.requiresCharge) {
                ability.InitCharge();
            }
            else {
                ability.AbilityMap();
            }

        }  //end if objectHit

    } //end CastSelecterRay(1)


    public void TargetRandomEnemy(Ability ability) {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        int index = Random.Range(0, allEnemies.Length);
        ability.targetEnemy = allEnemies[index].GetComponent<Enemy>();
    } //end TargetRandomEnemy(1)


    public void TargetAllEnemies(Ability ability) {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in allEnemies) {
            if (enemyObject.activeInHierarchy) {
                ability.targetBattleObjectList.Add(enemyObject.GetComponent<Enemy>());
            }
        }
    } //end TargetAllEnemies(1)


    public void TargetRandomHero(Ability ability) {
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        int index = Random.Range(0, allHeroes.Length);
        ability.targetHero = allHeroes[index].GetComponent<Hero>();
    } //end TargetRandomHero(1)


    public void TargetAllHeroes(Ability ability) {
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach (GameObject heroObject in allHeroes) {
            if (heroObject.activeInHierarchy) {
                ability.targetBattleObjectList.Add(heroObject.GetComponent<Hero>());
            }
        }
    } //end TargetAllHeroes(1)




    // END HERO SET, BEGIN ENEMY SET
    //YEAH I KNOW IT'S EMBARRASSING, MAYBE ABILITYOWNERS SHOULD BE BATTLEOBJECTS HUH


    public void EnemySortTargetingType(EnemyAbility ability) {

        if (ability.targetScope == EnemyAbility.TargetScope.SingleHero) {
            EnemyTargetRandomHero(ability);
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.SingleEnemy) {
            EnemyTargetRandomEnemy(ability);
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.AllHeroes) {
            EnemyTargetAllHeroes(ability); ;
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.AllEnemies) {
            EnemyTargetAllEnemies(ability);
            return;
        }

    } //end SortTargetingType

    
    public void EnemyTargetRandomHero(EnemyAbility ability) {
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        int index = Random.Range(0, allHeroes.Length);
        ability.targetHero = allHeroes[index].GetComponent<Hero>();
    } //end TargetRandomHero(1)
    

    public void EnemyTargetAllHeroes(EnemyAbility ability) {
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach (GameObject heroObject in allHeroes) {
            if (heroObject.activeInHierarchy) {
                ability.targetBattleObjectList.Add(heroObject.GetComponent<Hero>());
            }
        }
    } //end TargetAllHeroes(1)


    public void EnemyTargetRandomEnemy(EnemyAbility ability) {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        int index = Random.Range(0, allEnemies.Length);
        ability.targetEnemy = allEnemies[index].GetComponent<Enemy>();
    } //end TargetRandomEnemy(1)


    public void EnemyTargetAllEnemies(EnemyAbility ability) {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in allEnemies) {
            if (enemyObject.activeInHierarchy) {
                ability.targetBattleObjectList.Add(enemyObject.GetComponent<Enemy>());
            }
        }
    } //end TargetAllEnemies(1)




} //end TargetingManager
