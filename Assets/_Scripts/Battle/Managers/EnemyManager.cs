using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using EnemyAbilities;

public class EnemyManager : MonoBehaviour {

    public BattleManager battleManager;

    public float enemyWaitDuration;
    public bool enemiesActive;


	void Start () {

        enemiesActive = false;

        foreach (Enemy enemy in battleManager.enemyList) {
            enemy.currentEnemyState = Enemy.EnemyState.Inactive;
            PopulateAbilityLists(enemy);
        }

        StartCoroutine(EnemyWaitAtStart());

    } //end Start()
    	

	void Update () {

	    if (!enemiesActive) {
            return;
        }
        
        foreach(Enemy enemy in battleManager.enemyList) {
            CheckEnemyState(enemy);
        }
        
	} //end Update()
    

    public void CheckEnemyState (Enemy enemy) {

        if (enemy.currentEnemyState == Enemy.EnemyState.Barrage) {
            enemy.currentEnemyAbility.EnemyAbilityMap();
        }

        if (enemy.currentEnemyState == Enemy.EnemyState.Charge) {
            enemy.currentEnemyAbility.CheckEnemyCharge();
        }

        if (enemy.currentEnemyState == Enemy.EnemyState.Inactive) {

            int maxRange = 0;
            int marker = 0;

            foreach (EnemyAbility ability in enemy.enemyAbilityList) {
                maxRange += ability.enemyAbilityWeight;
                ability.enemyAbilityWeightMark = maxRange;
            }

            marker = Random.Range(0, maxRange);

            //could have list count here and iterate over it, that would be best/accommodate varying ability set sizes
            
            if (marker <= enemy.enemyAbilityList[0].enemyAbilityWeightMark) {
                enemy.currentEnemyAbility = enemy.enemyAbilityList[0];
            }
            else if (marker <= enemy.enemyAbilityList[1].enemyAbilityWeightMark) {
                enemy.currentEnemyAbility = enemy.enemyAbilityList[1];
            }
            else if (marker <= enemy.enemyAbilityList[2].enemyAbilityWeightMark) {
                enemy.currentEnemyAbility = enemy.enemyAbilityList[2];
            }

            enemy.currentEnemyAbility.InitEnemyAbility();
            Debug.Log(enemy + " using " + enemy.currentEnemyAbility.abilityName);

        } //end Inactive

        
    } //end CheckEnemyState(1)


    public void PopulateAbilityLists(Enemy enemy) {
        enemy.enemyAbilityList.Add(enemy.enemyAbilityOne);
        enemy.enemyAbilityList.Add(enemy.enemyAbilityTwo);
        enemy.enemyAbilityList.Add(enemy.enemyAbilityThree);
    }
    

    IEnumerator EnemyWaitAtStart() {
        while (Time.time < enemyWaitDuration) {
            yield return null;
        }

        enemiesActive = true;
        yield return null;
    } //end EnemyWaitAtStart()
    

} //end EnemyManager class
