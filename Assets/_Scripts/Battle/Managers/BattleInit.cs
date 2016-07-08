using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Heroes;
using Enemies;

public class BattleInit : MonoBehaviour {

    //eventually this takes from GIK

    List<Hero> heroesToSpawn = new List<Hero>();
    List<Enemy> enemiesToSpawn = new List<Enemy>();

    HeroOne punchie = new HeroOne();
    HeroTwo flamie = new HeroTwo();
    Champion champion = new Champion();
    Myshka myshka = new Myshka();

    BlueEnemy enemyOne = new BlueEnemy();
    RedEnemy enemyTwo = new RedEnemy();
    RedEnemy enemyThree = new RedEnemy();

    static int heroCount, enemyCount;
    static float heroHorizontalPosition;

    
    void Start () {

        heroesToSpawn.Add(punchie);
        heroesToSpawn.Add(flamie);
        heroesToSpawn.Add(champion);
        heroesToSpawn.Add(myshka);

        enemiesToSpawn.Add(enemyOne);
        enemiesToSpawn.Add(enemyTwo);
        enemiesToSpawn.Add(enemyThree);

        heroHorizontalPosition = 3;

        enemyCount = 3;

        SpawnBattleObjects(heroesToSpawn, enemiesToSpawn);

    } // end Start()
	

    public static void SpawnBattleObjects(List<Hero> heroList, List<Enemy> enemyList) {

        foreach (Hero hero in heroList) {
            SpawnHero(hero, heroList.IndexOf(hero));
        }

        List<Vector3> enemyPositionList = GenerateBattlePositionList();

        foreach (Enemy enemy in enemyList) {
            
            LoadBattleObjectPrefab(enemy, enemyPositionList[enemyList.IndexOf(enemy)]);
            
        } // end foreach

    } // end SpawnBattleObjects(2)


    static void SpawnHero(Hero hero, int position) {

        Vector3 origin = new Vector3(heroHorizontalPosition, 0, 0.5f);

        if (heroCount == 1) {
            LoadBattleObjectPrefab(hero, origin);
        }
        
    } // end SpawnHero(2)


    static void LoadBattleObjectPrefab(BattleObject battleObject, Vector3 position) {

        string prefabLocation = "BattleObjectPrefabs/" + battleObject.prefabName;
        GameObject BattleObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(prefabLocation),
            position,
            Quaternion.identity
            );

    } // end LoadBattleObjectPrefab(2)


    static List<Vector3> GenerateBattlePositionList() {

        List<Vector3> battlePositionList = new List<Vector3>();

        if (enemyCount == 1) {
            battlePositionList.Add(new Vector3(Random.Range(3, 11), 0, Random.Range(-4, 4)));
            return battlePositionList;
        }
        
        if (enemyCount == 2) {
           return GeneratePositionRing(2, 2, 5);
        }
        
        if (enemyCount == 3) {
            return GeneratePositionRing(3, 3, 6);
        }

        if (enemyCount == 4) {
            return GeneratePositionRing(4, 3, 7);
        }

        else {
            Debug.Log("Enemy count 0");
            return battlePositionList;
        }

    } // end GenerateBattlePositionList()


    static List<Vector3> GeneratePositionRing(int ringEnemyCount, float minRadius, float maxRadius) {

        List<Vector3> positionList = new List<Vector3>();

        // Generate ring and first coordinate
        float ringRadius = Random.Range(minRadius, maxRadius);
        float horizontalCoordinate = Random.Range((7 - ringRadius), (7 + ringRadius));
        Vector3 firstPosition = new Vector3(horizontalCoordinate, 0, (Mathf.Sqrt((Mathf.Pow(ringRadius, 2) - (Mathf.Pow(horizontalCoordinate, 2))))));

        positionList.Add(firstPosition);

        float ringAngle = 360 / ringEnemyCount;
        int remainingCount = ringEnemyCount - 1;

        // Calculate remaining coordinates, evenly spaced around the ring
        while (remainingCount > 0) {

            Vector3 lastPosition = positionList[positionList.Count - 1];

            float newHorizontalCoordinate = (lastPosition.x - 7) * (Mathf.Cos(ringAngle))
                - (lastPosition.y) * (Mathf.Sin(ringAngle));

            float newVerticalCoordinate = (lastPosition.y) * (Mathf.Cos(ringAngle))
                + (lastPosition.x - 7) * (Mathf.Sin(ringAngle));

            Vector3 positionToAdd = new Vector3((newHorizontalCoordinate + 7), 0, newVerticalCoordinate);

            positionList.Add(positionToAdd);
            remainingCount--;

        } //end while

        return positionList;

    } //End GeneratePositionRing(3)





}
