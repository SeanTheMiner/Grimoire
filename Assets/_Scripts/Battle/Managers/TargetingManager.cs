using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Abilities;
using EnemyAbilities;
using BattleObjects;


public class TargetingManager {

    public void SortTargetingType(HeroAbility ability) {

        if ((ability.targetScope == HeroAbility.TargetScope.SingleEnemy) | (ability.targetScope == HeroAbility.TargetScope.SingleHero) | (ability.targetScope == HeroAbility.TargetScope.SingleHeroOrEnemy)) {
            ability.abilityOwner.currentBattleState = Hero.BattleState.Target;
        }
        else if (ability.targetScope == HeroAbility.TargetScope.AllEnemies) {
            ability.targetBattleObjectList = TargetAllEnemies();
        }
        else if (ability.targetScope == HeroAbility.TargetScope.AllHeroes) {
            ability.targetBattleObjectList = TargetAllHeroes();
        }
        
    } //end SortTargetingType


    public void CastSelecterRay(HeroAbility ability) {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (ability.targetScope == HeroAbility.TargetScope.SingleEnemy)) {
                ability.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
            } 

            else if((objectHit.collider.tag == "Hero") && (ability.targetScope == HeroAbility.TargetScope.SingleHero)) {
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


    public Enemy TargetRandomEnemy() {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        int index = Random.Range(0, allEnemies.Length);
        return allEnemies[index].GetComponent<Enemy>();
    } //end TargetRandomEnemy(1)


    public List<BattleObject> TargetAllEnemies() {
        List<BattleObject> listToReturn = new List<BattleObject>();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in allEnemies) {
            if (enemyObject.activeInHierarchy) {
                listToReturn.Add(enemyObject.GetComponent<Enemy>());
            }
        }
        return listToReturn;
    } //end TargetAllEnemies(1)


    public Hero TargetRandomHero() {
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        int index = Random.Range(0, allHeroes.Length);
        return allHeroes[index].GetComponent<Hero>();
    } //end TargetRandomHero(1)


    public List<BattleObject> TargetAllHeroes() {
        List<BattleObject> listToReturn = new List<BattleObject>();
        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach (GameObject heroObject in allHeroes) {
            if (heroObject.activeInHierarchy) {
                listToReturn.Add(heroObject.GetComponent<Hero>());
            }
        }
        return listToReturn;
    } //end TargetAllHeroes(1)


    //Enemy targeting sorting - it might make sense to just target abilities before procs, but this works for now
    //and adds another layer of depth anyway. Barely. Let's throw around the word "depth."

    public void EnemySortTargetingType(EnemyAbility ability) {

        if (ability.targetScope == EnemyAbility.TargetScope.SingleHero) {
            ability.targetHero = TargetRandomHero();
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.SingleEnemy) {
            ability.targetEnemy = TargetRandomEnemy();
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.AllHeroes) {
            ability.targetBattleObjectList = TargetAllHeroes();
            return;
        }
        else if (ability.targetScope == EnemyAbility.TargetScope.AllEnemies) {
            ability.targetBattleObjectList = TargetAllEnemies();
            return;
        }

    } //end SortTargetingType


} //end TargetingManager
