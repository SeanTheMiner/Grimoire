using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using StringData;


public class BattleInit : MonoBehaviour {
    

    public PrefabIDCodex codex = new PrefabIDCodex();
    
    int heroCount, enemyCount;
    float heroHorizontalPosition, heroSpacing,
        enemyHorizontalPosition;

    Vector3 heroOrigin;

    // Eventually, this takes from GIK
    List<int> heroesToSpawnByID = new List<int>();
    List<int> enemiesToSpawnByID = new List<int>();


    void Start () {
        
        // Again, eventually fed from GIK
        heroesToSpawnByID.Add(1);
        heroesToSpawnByID.Add(2);
        heroesToSpawnByID.Add(3);
        heroesToSpawnByID.Add(4);

        enemiesToSpawnByID.Add(1);
        enemiesToSpawnByID.Add(2);
        enemiesToSpawnByID.Add(2);
        
        // From here should be fine

        heroCount = heroesToSpawnByID.Count;
        enemyCount = enemiesToSpawnByID.Count;
        
        heroHorizontalPosition = -8;
        heroSpacing = 2;
        heroOrigin = new Vector3(heroHorizontalPosition, 0, 0);
        
        SpawnBattleObjects(heroesToSpawnByID, enemiesToSpawnByID);

    } // end Start()
	
    
    public void SpawnBattleObjects(List<int> heroIDList, List<int> enemyIDList) {

        // Spawn heroes

        if (heroCount == 1) {
            SpawnOneHero(heroIDList[0]);
        }

        else if (heroCount == 2) {
            SpawnTwoHeroes(heroIDList);
        }

        else if (heroCount == 3) {
            SpawnThreeHeroes(heroIDList);
        }

        else if (heroCount == 4) {
            SpawnFourHeroes(heroIDList);
        }


        // Spawn enemies
        
        List<Vector3> enemyPositionList = GenerateBattlePositionList();
        
        foreach (int enemyID in enemyIDList) {
            
            LoadEnemyPrefab(codex.enemyDictionary[enemyIDList.IndexOf(enemyID) + 1], 
                enemyPositionList[enemyIDList.IndexOf(enemyID)]);
            
        } // end foreach
        
    } // end SpawnBattleObjects(2)


    void SpawnOneHero (int heroID) {
        LoadHeroPrefab(codex.heroDictionary[heroID], heroOrigin);
    } // end SpawnOneHero(1)


    void SpawnTwoHeroes (List<int> heroIDList) {

        Vector3 positionOne = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z + heroSpacing);
        Vector3 positionTwo = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z - heroSpacing);

        LoadHeroPrefab(codex.heroDictionary[heroIDList[0]], positionOne);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[1]], positionTwo);
        
    } // end SpawnTwoHeroes(1)


    void SpawnThreeHeroes (List<int> heroIDList) {

        Vector3 positionOne = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z + (2 * heroSpacing));
        Vector3 positionThree = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z - (2 * heroSpacing));

        LoadHeroPrefab(codex.heroDictionary[heroIDList[0]], positionOne);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[1]], heroOrigin);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[2]], positionThree);

    } // end SpawnThreeHeroes(1)


    void SpawnFourHeroes (List<int> heroIDList) {

        Vector3 positionOne = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z + (3 * heroSpacing));
        Vector3 positionTwo = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z + heroSpacing);
        Vector3 positionThree = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z - heroSpacing);
        Vector3 positionFour = new Vector3(heroOrigin.x, heroOrigin.y, heroOrigin.z - (3 * heroSpacing));

        LoadHeroPrefab(codex.heroDictionary[heroIDList[0]], positionOne);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[1]], positionTwo);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[2]], positionThree);
        LoadHeroPrefab(codex.heroDictionary[heroIDList[3]], positionFour);

    } // end SpawnFourHeroes(1)

   
    void LoadHeroPrefab(string prefabName, Vector3 position) {

        string prefabLocation = "BattleObjectPrefabs/Heroes/" + prefabName;
        GameObject BattleObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(prefabLocation),
            position,
            Quaternion.Euler(0, 90, 0)
            );

    } // end LoadBattleObjectPrefab(2)


    void LoadEnemyPrefab(string prefabName, Vector3 position) {

        Debug.Log("loading" + prefabName);

        string prefabLocation = "BattleObjectPrefabs/Enemies/" + prefabName;
        GameObject BattleObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load(prefabLocation),
            position,
            Quaternion.Euler(90, 0, 0)
            );

    } // end LoadBattleObjectPrefab(2)


    List<Vector3> GenerateBattlePositionList() {

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

        Debug.Log("position ring called");

        List<Vector3> positionList = new List<Vector3>();

        // Generate ring and first coordinate
        float ringRadius = Random.Range(minRadius, maxRadius);
        float horizontalCoordinate = Random.Range((7 - ringRadius), (7 + ringRadius));
        
        Vector3 firstPosition = new Vector3(horizontalCoordinate, 0, Mathf.Sqrt(Mathf.Pow(ringRadius, 2) - Mathf.Pow((horizontalCoordinate - 7), 2)));
        
        positionList.Add(firstPosition);

        float ringAngle = 360 / ringEnemyCount;
        int remainingCount = ringEnemyCount - 1;

        // Calculate remaining coordinates, evenly spaced around the ring
        while (remainingCount > 0) {

            Debug.Log("while loop");

            Vector3 lastPosition = positionList[positionList.Count - 1];

            float newHorizontalCoordinate = (lastPosition.x - 7) * (Mathf.Cos(ringAngle))
                - (lastPosition.y) * (Mathf.Sin(ringAngle));

            float newVerticalCoordinate = (lastPosition.y) * (Mathf.Cos(ringAngle))
                + (lastPosition.x - 7) * (Mathf.Sin(ringAngle));

            Vector3 positionToAdd = new Vector3((newHorizontalCoordinate + 7), 0, newVerticalCoordinate);

            positionList.Add(positionToAdd);
            remainingCount--;

        } // end while

        Debug.Log(positionList[0]);
        Debug.Log(positionList[1]);
        Debug.Log(positionList[2]);

        return positionList;

    } // end GeneratePositionRing(3)

    
} // end BattleInit class
