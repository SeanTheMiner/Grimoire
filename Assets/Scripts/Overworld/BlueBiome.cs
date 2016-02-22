using UnityEngine;
using System.Collections.Generic;

using Enemies;
using Biomes;




public class BlueBiome : Biome {

    public Enemy blueEnemyOne;
    public Enemy blueEnemyTwo;
    public Enemy blueEnemyThree;

    public BlueBiome () {

        enemyPool.Add(blueEnemyOne);
        enemyPool.Add(blueEnemyTwo);
        enemyPool.Add(blueEnemyThree);

        enemySpawnCap = 3;

        //metaListToUse = battlePositionSet.threeEnemiesMetaList;
        //positionListToUse = metaListToUse[Random.Range(0, (metaListToUse.Count - 1))];

    }
}
